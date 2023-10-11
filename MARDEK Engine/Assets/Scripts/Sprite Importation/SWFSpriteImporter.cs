using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SWFSpriteImporter : MonoBehaviour
{
    [SerializeField] int ID = 0;

    [ContextMenu("Import One")]
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
        if (prefabGUIs.Length == 1)
        { 
                
        }

        var jsonGUIDs = AssetDatabase.FindAssets($"t:TextAsset {id}");
        if (jsonGUIDs.Length == 1)
        {
            var spriteObject = new GameObject();
            spriteObject.transform.parent = parent;
            spriteObject.transform.localPosition = Vector3.zero;
            spriteObject.transform.localRotation = Quaternion.identity;
            spriteObject.transform.localScale = Vector3.one;
            var spriteComponent = spriteObject.AddComponent<SWFSprite>();

            var path = AssetDatabase.GUIDToAssetPath(jsonGUIDs[0]);
            var json = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            spriteComponent.Create(json);
            return spriteObject;
        }

        Debug.LogError($"Failed to create or instantiate swf object of id: {id}");
        return null;
    }
}