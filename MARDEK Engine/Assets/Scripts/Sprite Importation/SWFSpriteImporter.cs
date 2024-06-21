using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class SWFSpriteImporter : MonoBehaviour
{
    [SerializeField] int ID = 0;

    [ContextMenu("Import This ID")]
    void ImportOne()
    {
        CreateOrInstantiateByID(ID, transform);
    }

    public static GameObject CreateOrInstantiateByID(int id, Transform parent)
    {
        var svgGUIDs = AssetDatabase.FindAssets($"t:Sprite {id}");
        if (svgGUIDs.Length == 1)
        {
            var shapeObject = new GameObject();
            shapeObject.transform.parent = parent;
            shapeObject.transform.localPosition = Vector3.zero;
            shapeObject.transform.localRotation = Quaternion.identity;
            shapeObject.transform.localScale = Vector3.one;
            var shapeComponent = shapeObject.AddComponent<SWFShape>();
            
            var path = AssetDatabase.GUIDToAssetPath(svgGUIDs[0]);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            shapeComponent.Create(sprite);
            return shapeObject;
        }

        var prefabGUIs = AssetDatabase.FindAssets($"t:Prefab {id}");
        if (prefabGUIs.Length == 0)
        {
            var jsonGUIDs = AssetDatabase.FindAssets($"t:TextAsset {id}");
            if (jsonGUIDs.Length == 1)
            {
                print($"Creating prefab: {id}");

                var spriteObject = new GameObject();
                spriteObject.transform.parent = parent;
                spriteObject.transform.localPosition = Vector3.zero;
                spriteObject.transform.localRotation = Quaternion.identity;
                spriteObject.transform.localScale = Vector3.one;
                var spriteComponent = spriteObject.AddComponent<SWFSprite>();

                var path = AssetDatabase.GUIDToAssetPath(jsonGUIDs[0]);
                var json = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                spriteComponent.Create(json);

                var prefabPath = $"Assets/Prefabs/Battle Models/Inner Parts/{id}.prefab";
                PrefabUtility.SaveAsPrefabAsset(spriteObject, prefabPath);
                DestroyImmediate(spriteObject);
            }
        }
        prefabGUIs = AssetDatabase.FindAssets($"t:Prefab {id}");
        if (prefabGUIs.Length == 1)
        {
            print($"Instantiating prefab: {id}");

            var path = AssetDatabase.GUIDToAssetPath(prefabGUIs[0]);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var obj = PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            foreach(var shape in obj.GetComponentsInChildren<SWFShape>(true))
                shape.CalculateSortingOrderAndColor();

            return obj;
        }
        if (prefabGUIs.Length > 1)
            Debug.LogError($"Found more than 1 prefab with id: {id}");

        var failed = new GameObject($"{id}");
        failed.transform.parent = parent;
        return failed;
    }
}
#endif