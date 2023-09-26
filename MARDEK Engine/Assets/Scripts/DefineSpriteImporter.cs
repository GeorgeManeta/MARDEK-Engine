using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FullSerializer;
using SkewMatrixDecompositor;

public class DefineSpriteImporter : MonoBehaviour
{
    public int spriteID = 0;
    [SerializeField] List<Frame> frames = new List<Frame>();
    [SerializeField] List<DefineSpriteImporter> placedObjects = new List<DefineSpriteImporter>();
    public int depth = 0;

    private void OnValidate()
    {
        ListMatrices();
    }

    [ContextMenu("Import")]
    public void Import()
    {
        if (spriteID == 0)
        {
            Debug.LogError("Trying to import sprite with id 0, aborting.");
            return;
        }

        var jsonGUIDs = AssetDatabase.FindAssets($"t:TextAsset {spriteID}");
        if (jsonGUIDs.Length == 1)
        {
            LoadAsDefineSprite(jsonGUIDs[0]);
            return;
        }

        var svgGUIDs = AssetDatabase.FindAssets($"t:Sprite {spriteID}");
        if (svgGUIDs.Length == 1)
        {
            LoadAsShape(svgGUIDs[0]);
            return;
        }

        Debug.LogWarning($"ID {spriteID} didn't match json or sprite");
    }

    void LoadAsDefineSprite(string guid)
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var defineSpriteJSON = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        fsJsonParser.Parse(defineSpriteJSON.text, out fsData data);
        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize(data, ref frames);

        InstantiateFirstFrame();
    }
    void LoadAsShape(string guid)
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void InstantiateFirstFrame()
    {
        for (int i = gameObject.transform.childCount-1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        placedObjects = new List<DefineSpriteImporter>();

        foreach (var placeObject in frames[0].placeObjects)
        {
            var gameObject = new GameObject($"{placeObject.depth} : {placeObject.id}");
            gameObject.transform.parent = transform;

            var position = new Vector3(placeObject.translateX, -placeObject.translateY, 0);
            gameObject.transform.localPosition = position;

            var scale = new Vector3(placeObject.scaleX, placeObject.scaleY, 1);
            gameObject.transform.localScale = scale;

            var defineSprite = gameObject.AddComponent<DefineSpriteImporter>();
            defineSprite.spriteID = placeObject.id;
            defineSprite.depth = placeObject.depth;
            placedObjects.Add(defineSprite);
            defineSprite.Import();
        }
    }

    [SerializeField] int frame = 0;
    [ContextMenu("List Matrices")]
    public void ListMatrices()
    {
        foreach(var po in frames[frame].placeObjects)
        {
            var matrix = new System.Numerics.Matrix3x2(po.scaleX, po.rotateSkew0, po.rotateSkew1, po.scaleY, po.translateX, -po.translateY);
            var result = MatrixDecompositor.Decompose(matrix);

            foreach(var i in placedObjects)
                if (i.depth == po.depth)
                {
                    if (result.Scale2 != null)
                        i.transform.localScale = result.Scale2.Value;
                    else
                        i.transform.localScale = Vector3.one;

                    if (result.Rotate2 != null)
                        i.transform.localRotation = Quaternion.Euler(0f, 0f, -result.Rotate2.Value * Mathf.Rad2Deg);
                    else
                        i.transform.localRotation = Quaternion.Euler(0f, 0f, 0);

                    if (result.Translate != null)
                        i.transform.position = result.Translate.Value;
                    else
                        i.transform.position = Vector3.zero;

                    if (i.transform.childCount == 0)
                        continue;
                    var child = i.transform.GetChild(0);
                    if (child)
                    {
                        child.transform.localPosition = Vector3.zero;

                        if (result.Scale1 != null)
                            child.transform.localScale = result.Scale1.Value;
                        else
                            child.transform.localScale = Vector3.one;

                        if (result.Rotate1 != null)
                            child.transform.localRotation = Quaternion.Euler(0f, 0f, -result.Rotate1.Value * Mathf.Rad2Deg);
                        else
                            child.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

                    }
                }
        }
    }

    [ContextMenu("Apply")]
    public void ApplyResult()
    {

    }

    [System.Serializable]
    class Frame
    {
        public List<PlaceObject> placeObjects;
    }

    [System.Serializable]
    class PlaceObject
    {
        public int depth;
        public int id;
        public float translateX = 0;
        public float translateY = 0;
        public float scaleX = 1;
        public float scaleY = 1;
        public float rotateSkew0 = 0;
        public float rotateSkew1 = 0;
    }
}