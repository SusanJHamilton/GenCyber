using UnityEngine;
using UnityEditor;

public class EnableRead
{
    [MenuItem("Tools/Enable Read/Write On All Meshes")]
    static void EnableReadWrite()
    {
        string[] allModelPaths = AssetDatabase.FindAssets("t:Model");

        foreach (string guid in allModelPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null && !importer.isReadable)
            {
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                Debug.Log($"Enabled Read/Write on: {path}");
            }
        }

        Debug.Log("Finished enabling Read/Write on all models.");
    }
}
