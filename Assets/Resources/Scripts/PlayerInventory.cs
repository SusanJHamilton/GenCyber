using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using System;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> inventory = new List<InventoryItem>();

    #region UI stuff
    public GameObject item_template;
    public GridLayoutGroup item_panel;
    public GameObject inventoryUI;
    #endregion

    private bool UIopen = false;

    public bool hasItem(string itemName)
    {
        bool returner = false;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == itemName)
            {
                returner = true;
            }
        }

        return returner;
    }

    public void addItem(InventoryItem item)
    {
        inventory.Add(item);

        item_template.SetActive(true);
        GameObject newItem = Instantiate(item_template, item_panel.transform);
        item_template.SetActive(false);
        newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        // hint_item.GetComponentInChildren<TextMeshProUGUI>().text = clue.text;
        newItem.GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Clue clicked");
            UI_Manager.getInstance().showInfo(item.itemName, item.description);
        });
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIopen)
            {
                closeUI();
                UIopen = false;
            } 
            else
            {
                openUI();
                UIopen = true;
            }
        }
    }

    public void openUI()
    {
        inventoryUI.SetActive(true);
        inventoryUI.GetComponent<RectTransform>().DOMove(new Vector3(50, 25, 0), 1);
    }

    public void closeUI()
    {
        inventoryUI.GetComponent<RectTransform>().DOMove(new Vector3(50, -600, 0), 1).onComplete += () =>
        {
            inventoryUI.SetActive(false);
        };
    }
}
