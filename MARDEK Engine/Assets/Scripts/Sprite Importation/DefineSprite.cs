using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FullSerializer;

public class DefineSprite : MonoBehaviour
{
    public int spriteID = 0;
    [SerializeField] List<FrameJSON> frames = new List<FrameJSON>();
    [SerializeField] List<PlaceObject> placedObjects = new List<PlaceObject>();
    [SerializeField] int selectedFrame = 0;

    private void OnValidate()
    {
        SetFrame(selectedFrame);
    }

    private void Update()
    {
        if(frames.Count > 0)
        {
            var index = (int)(Time.time * 30) % frames.Count;
            SetFrame(index);
        }
    }

    [ContextMenu("Import")]
    public void Import()
    {
        if (spriteID == 0)
        {
            Debug.LogError("Trying to import sprite with id 0, aborting.");
            return;
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

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
    void LoadAsShape(string guid)
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        int order = 0;
        var upperObject = GetComponentInParent<PlaceObject>(includeInactive: true);
        while (upperObject != null)
        {
            order += upperObject.Depth;
            var parent = upperObject.transform.parent;
            if (parent == null)
                upperObject = null;
            else
                upperObject = parent.GetComponentInParent<PlaceObject>(includeInactive: true);
        }
        spriteRenderer.sortingOrder = order;
    }
    void LoadAsDefineSprite(string guid)
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var defineSpriteJSON = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        fsJsonParser.Parse(defineSpriteJSON.text, out fsData data);
        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize(data, ref frames);

        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        placedObjects = new List<PlaceObject>();

        foreach (var frame in frames)
        {
            foreach(var obj in frame.placeObjects)
            {
                if (GetObjectByDepth(obj.depth) == null)
                {
                    var newGameObject = new GameObject($"{obj.depth}");
                    newGameObject.transform.parent = transform;
                    var newPlaceObject = newGameObject.AddComponent<PlaceObject>();
                    newPlaceObject.Create(obj.depth);
                    placedObjects.Add(newPlaceObject);
                }
            }
        }
        SetFrame(frames.Count);
        SetFrame(0);
    }
    PlaceObject GetObjectByDepth(int depth)
    {
        foreach (var obj in placedObjects)
            if (obj.Depth == depth)
                return obj;
        return null;
    }
    void SetFrame(int frameNumber)
    {
        if (frames.Count == 0)
            return;
        foreach (var obj in placedObjects)
            obj.SetObjectByID(-1);

        for (int f = 0; f <= frameNumber; f++)
        {
            if (frames.Count <= f)
                continue;

            foreach (var obj in frames[f].removeObjects)
            {
                GetObjectByDepth(obj.depth).SetObjectByID(-1);
            }
            foreach (var obj in frames[f].placeObjects)
            {
                var placedObject = GetObjectByDepth(obj.depth);
                placedObject.SetObjectByID(obj.id);
                placedObject.SetMatrix(obj.scaleX, obj.rotateSkew0, obj.rotateSkew1, obj.scaleY, obj.translateX, -obj.translateY);
            }
        }
    }

    [System.Serializable]
    class FrameJSON
    {
        public List<PlaceObjectJSON> placeObjects = new List<PlaceObjectJSON>();
        public List<RemoveObjectJSON> removeObjects = new List<RemoveObjectJSON>();
    }
    [System.Serializable]
    class PlaceObjectJSON
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
    [System.Serializable]
    class RemoveObjectJSON
    {
        public int depth;
    }
}