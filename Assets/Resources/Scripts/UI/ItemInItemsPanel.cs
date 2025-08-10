using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInItemsPanel : MonoBehaviour
{
    public ItemsPanelManager itemsPanelManager;
    public EscapeRoomItem EscapeRoom_item;
    
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            if (EscapeRoom_item == null) return;
            UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content, Resources.Load<Sprite>(EscapeRoom_item.img));
        });
    }

    public void setItem(EscapeRoomItem item)
    {
        EscapeRoom_item = item;
        Sprite sprite = Resources.Load<Sprite>(item.img);
        transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }
    
}
