using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    // [HideInInspector]
    private List<TKey> keys = new List<TKey>();
    [SerializeField]
    // [HideInInspector]
    private List<TValue> values = new List<TValue>();

    public Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    // Save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    // Load dictionary from lists
    public void OnAfterDeserialize()
    {
        dictionary.Clear();

        if (keys.Count != values.Count)
            throw new Exception("There are different numbers of keys and values");

        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
    }

    //add a new item to the dictionary
    public void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
    }

    public void Remove(TKey key)
    {
        dictionary.Remove(key);
    }
}


public class EscapeRoomItemsLoader : MonoBehaviour
{
    public string EscapeRoomItemsJson = "data.json"; 
    [SerializeField]
    public SerializableDictionary<string, InteractableObject> escapeRoomItems = new SerializableDictionary<string, InteractableObject>();
    [SerializeField]
    public List<string> escapeRoomItemsKeys_zone1 = new List<string>();
    public List<string> escapeRoomItemsKeys_zone2 = new List<string>();

    [HideInInspector]
    EscapeRoomItemsParser escapeRoomItemsParser;
    public GameObject clue_zone1_parent;
    public GameObject clue_zone2_parent;

    public GameObject magazine_1;
    public GameObject usb;
    public GameObject trophy;
    public GameObject picture_1;
    public GameObject stickynote;
    public GameObject foodbowl1;
    public GameObject foodbowl2;
    public GameObject mail;
    public GameObject voicemail;
    public GameObject bluekey;
    public GameObject magazine_2;

    private static EscapeRoomItemsLoader instance;

    public static EscapeRoomItemsLoader getInstance(){
        return instance;
    }

    void Awake(){
        if (instance == null){
            instance = this;
        } 
    }



    void Start()
    {  
        //parse the json file
        escapeRoomItemsParser = new EscapeRoomItemsParser(EscapeRoomItemsJson); 

        //randomly choose 3 gameobejcts for zone1
        List<GameObject> zone1_items = new List<GameObject>();
        List<GameObject> zone2_items = new List<GameObject>();
        int[] randomNumbers_zone1 = Utils.getRandomIntegers(escapeRoomItemsKeys_zone1.Count, clue_zone1_parent.transform.childCount);
        int[] randomNumbers_zone2 = Utils.getRandomIntegers(escapeRoomItemsKeys_zone2.Count, clue_zone2_parent.transform.childCount);

        for (int i = 0; i < randomNumbers_zone1.Length; i++)
        {
            GameObject item = clue_zone1_parent.transform.GetChild(randomNumbers_zone1[i]).gameObject;
            //add * on the name of the item
            item.name = "***"+item.name + "***"+
            //dynamically add components: InteractableObject 
            item.AddComponent<InteractableObject>(); 
            item.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById(escapeRoomItemsKeys_zone1[i]));
            zone1_items.Add(item);
        }

        for (int i = 0; i < randomNumbers_zone2.Length; i++)
        {
            GameObject item = clue_zone2_parent.transform.GetChild(randomNumbers_zone2[i]).gameObject;
            item.name = "***"+item.name + "***"+
            //dynamically add components: InteractableObject  
            item.AddComponent<InteractableObject>(); 
            item.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById(escapeRoomItemsKeys_zone2[i]));
            zone2_items.Add(item);
        } 

        //change tag name of the object in zone1 and zone2 if the object name does not contain ***
        foreach (Transform tr in clue_zone1_parent.transform)
        {
            if (!tr.name.Contains("***"))
            {
                tr.gameObject.tag = "Untagged";
            }
        }
        foreach (Transform tr in clue_zone2_parent.transform)
        {
            if (!tr.name.Contains("***"))
            {
                tr.gameObject.tag = "Untagged";
            }
        }

        magazine_1.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("magazine_1"));
        usb.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("usb"));
        trophy.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("trophy"));
        picture_1.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("picture_1"));
        stickynote.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("stickynote"));
        foodbowl1.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("foodbowl"));
        foodbowl2.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("foodbowl"));
        mail.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("mail"));
        voicemail.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("voicemail"));
        bluekey.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("blueKey"));
        magazine_2.GetComponent<InteractableObject>().setEscapeRoomItem(escapeRoomItemsParser.GetItemById("magazine_2"));

    }

    
    //get the item by id
    public EscapeRoomItem GetItemById(string id)
    {
        return escapeRoomItemsParser.GetItemById(id);
    }
}
