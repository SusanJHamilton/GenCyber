using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PanelOrientation
{
    Horizontal,
    Vertical
}

public class ItemsPanelManager : MonoBehaviour
{
    public PanelOrientation orientation = PanelOrientation.Horizontal;
    public GameObject items_parent;
    public GameObject item_template;
    

    // Start is called before the first frame update
    void Start()
    {
        if (orientation == PanelOrientation.Horizontal)
        {
            items_parent.AddComponent<HorizontalLayoutGroup>();
            items_parent.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            items_parent.GetComponent<HorizontalLayoutGroup>().childControlHeight = true;
            ContentSizeFitter csf = items_parent.GetComponent<ContentSizeFitter>();
            csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
        else
        { 
            items_parent.AddComponent<VerticalLayoutGroup>();
            items_parent.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
            items_parent.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
            ContentSizeFitter csf = items_parent.GetComponent<ContentSizeFitter>();
            csf.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    public void addItem(InteractableObject item)
    {
        item_template.SetActive(true);
        GameObject newItem = Instantiate(item_template, items_parent.transform);
        item_template.SetActive(false);

        newItem.GetComponent<ItemInItemsPanel>().setItem(item.EscapeRoom_item);
    }

    public bool findItem(string item_id){
        foreach (Transform child in items_parent.transform)
        {
            ItemInItemsPanel item = child.GetComponent<ItemInItemsPanel>();
            if (item == null) return false;
            EscapeRoomItem es = item.EscapeRoom_item;
            if (es == null) return false;

            if (es.id == item_id) return true;
        }
        return false;
    }

    public void clearItems()
    {
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(0).GetChild(i).gameObject.activeSelf) 
            {
                Destroy(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
            }
        }
    }
}
