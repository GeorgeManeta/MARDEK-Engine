using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FullSerializer;
using UnityEngine.UIElements;

public class DefineSpriteImporter : MonoBehaviour
{
    public int spriteID = 0;
    [SerializeField] List<Frame> frames = new List<Frame>();

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

        foreach (var placeObject in frames[0].placeObjects)
        {
            var gameObject = new GameObject(placeObject.id.ToString());
            gameObject.transform.parent = transform;
            var position = new Vector3(placeObject.translateX, -placeObject.translateY, 0);
            gameObject.transform.localPosition = position;
            var defineSprite = gameObject.AddComponent<DefineSpriteImporter>();
            defineSprite.spriteID = placeObject.id;
            defineSprite.Import();
        }
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