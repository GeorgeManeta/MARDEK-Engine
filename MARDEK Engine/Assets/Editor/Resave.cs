using UnityEditor;
using UnityEngine;

public class ResaveAll : Editor
{
    [MenuItem("Custom/Re-save All Assets")]
    private static void Resave()
    {
        string[] assets = AssetDatabase.FindAssets("");

        Debug.Log($"Re-saving {assets.Length} assets");
        foreach (string guid in assets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            EditorUtility.SetDirty(asset);
        }

        AssetDatabase.SaveAssets();
    }
}