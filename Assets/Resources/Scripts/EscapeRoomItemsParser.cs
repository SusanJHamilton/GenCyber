using UnityEngine;
using System.Collections.Generic;
using System.IO; 

[System.Serializable]
public class EscapeRoomItem
{
    public string id;
    public string useCase;
    public string type;
    public string title;
    public string content;
    public string img;
}

[System.Serializable]
public class EscapeRoomItems
{
    public List<EscapeRoomItem> items;
}

public class EscapeRoomItemsParser 
{ 
    private Dictionary<string, EscapeRoomItem> itemsDictionary; 

    public EscapeRoomItemsParser(string json)
    {
        //load the json file under Resources folder. the name is data.json
        // string path = Application.dataPath + "/Resources/" + json_path;
        // string escapeRoomItemsJsonString = File.ReadAllText(path); 
        EscapeRoomItems escapeRoomItems = JsonUtility.FromJson<EscapeRoomItems>(json);
        itemsDictionary = new Dictionary<string, EscapeRoomItem>();

        foreach (var item in escapeRoomItems.items)
        {
            itemsDictionary.Add(item.id, item);
        }

        Debug.Log("EscapeRoomItemsParser: " + itemsDictionary.Count + " items loaded.");
    }  

    public EscapeRoomItem GetItemById(string id)
    {
        if (itemsDictionary.ContainsKey(id))
        {
            return itemsDictionary[id];
        }
        else
        {
            Debug.LogWarning("Item with ID " + id + " not found.");
            return null;
        }
    } 
}