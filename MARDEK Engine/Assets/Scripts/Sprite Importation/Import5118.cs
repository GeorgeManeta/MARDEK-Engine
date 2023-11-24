using FullSerializer;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Import5118 : MonoBehaviour
{
    [SerializeField] TextAsset json5118;
    [SerializeField] List<SWFFrame> frames = new List<SWFFrame>();
    [ContextMenu("Import")] void Import()
    {
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        fsJsonParser.Parse(json5118.text, out fsData data);
        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize(data, ref frames);

        foreach(var frame in frames)
        {
            if (string.IsNullOrEmpty(frame.label))
                continue;
            if (frame.placeObjects.Count == 0)
                continue;

            var go = new GameObject(frame.label);
            go.transform.parent = transform;
            var bm = go.AddComponent<BattleModel>();
            var placedObject = frame.placeObjects[0];
            bm.Create(frame.label, placedObject.id, placedObject.skin);
            var prefabPath = $"Assets/Prefabs/Battle Models/{frame.label}.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
        }
    }
}