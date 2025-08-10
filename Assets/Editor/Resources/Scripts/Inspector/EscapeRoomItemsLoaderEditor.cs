using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(EscapeRoomItemsLoader))]
public class EscapeRoomItemsLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EscapeRoomItemsLoader script = (EscapeRoomItemsLoader)target;

        if (script.escapeRoomItems != null)
        {
            EditorGUILayout.LabelField("Escape Room Items", EditorStyles.boldLabel);

            foreach (var kvp in script.escapeRoomItems.dictionary.ToList())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Key", GUILayout.Width(100));
                string newKey = EditorGUILayout.TextField(kvp.Key);
                if (newKey != kvp.Key)
                {
                    script.escapeRoomItems.dictionary.Remove(kvp.Key);
                    script.escapeRoomItems.dictionary[newKey] = kvp.Value;
                }
                EditorGUILayout.LabelField("Value", GUILayout.Width(100));
                script.escapeRoomItems.dictionary[newKey] = (InteractableObject)EditorGUILayout.ObjectField(kvp.Value, typeof(InteractableObject), true);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Item"))
            {
                string uniqueKey = GetUniqueKey(script.escapeRoomItems.dictionary, "New Key");
                script.escapeRoomItems.dictionary.Add(uniqueKey, null);
            }

            if (GUILayout.Button("Delete Last Item"))
            {
                if (script.escapeRoomItems.dictionary.Count > 0)
                {
                    var lastKey = script.escapeRoomItems.dictionary.Keys.Last();
                    script.escapeRoomItems.dictionary.Remove(lastKey);
                }
            }
        }
    }

    private string GetUniqueKey(Dictionary<string, InteractableObject> dictionary, string baseKey)
    {
        string uniqueKey = baseKey;
        int counter = 1;
        while (dictionary.ContainsKey(uniqueKey))
        {
            uniqueKey = $"{baseKey} {counter}";
            counter++;
        }
        return uniqueKey;
    }
}