using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//this object is something that contains an EscapeRoomItem, which could be clue
public class InteractableObject : MonoBehaviour
{
    private Outline outline = null;
    public bool senseMode = false;
    public EscapeRoomItem EscapeRoom_item;
    public bool isArchived = false;

    public void setEscapeRoomItem(EscapeRoomItem item)
    {
        this.EscapeRoom_item = item;
    }

    public void Update()
    {
        if (this.outline != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                senseMode = true;
                this.outline.OutlineColor = new Color(0f, 0.8f, 0.8f, 1f);
                this.outline.enabled = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                senseMode = false;
                this.outline.OutlineColor = new Color(1, 1, 1, 1f);
                this.outline.enabled = false;
            }
        }
    }

    public void Start()
    {
        this.outline = this.gameObject.GetComponent<Outline>();
    } 

    public void showInfo()
    {
        if (this.EscapeRoom_item == null) return;
        if (this.isArchived) return;
        
        Debug.Log("Showing info for: " + this.EscapeRoom_item.type + " " + this.EscapeRoom_item.title);

        if (EscapeRoom_item.useCase == "info")
            UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content);
        else if (EscapeRoom_item.useCase == "item")
        {
            UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content, Resources.Load<Sprite>(EscapeRoom_item.img), () => {
                Debug.Log("item clicked");
                this.isArchived = true;
                //get gameobject with the name "inventory_items_panel"
                GameObject inventory_items_panel = GameObject.Find("inventory_items_panel");
                ItemsPanelManager itemsPanelManager = inventory_items_panel.GetComponent<ItemsPanelManager>();
                itemsPanelManager.addItem(this);

                UI_Manager.getInstance().showMessage("The " + EscapeRoom_item.title + " is now added to your inventory showing on the top of the screen");

                MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
                MeshCollider collider = this.gameObject.GetComponent<MeshCollider>();
                BoxCollider bcollider = this.gameObject.GetComponent<BoxCollider>();

                if (renderer != null) { renderer.enabled = false; }
                if (collider != null) { collider.enabled = false; }
                if (bcollider != null) { bcollider.enabled = false; }

                MeshRenderer childRenderer;
                MeshCollider childCollider;
                BoxCollider childbCollider;

                Transform transform = this.gameObject.GetComponent<Transform>();
                for (int i = 0; i < transform.childCount; i++)
                {
                    childRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
                    childCollider = transform.GetChild(i).GetComponent<MeshCollider>();
                    childbCollider = transform.GetChild(i).GetComponent<BoxCollider>();

                    if (childRenderer != null) { childRenderer.enabled = false; }
                    if (childCollider != null) { childCollider.enabled = false; }
                    if (childbCollider != null) { childbCollider.enabled = false; }
                }
            });
        }
        else if (EscapeRoom_item.useCase == "clue")
        {
            if (EscapeRoom_item.type == "audio")
            {
                UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content, Resources.Load<Sprite>(EscapeRoom_item.img), () => {
                    Debug.Log("clue clicked");
                    this.isArchived = true;
                    //get gameobject with the name "inventory_items_panel"
                    GameObject clues_items_panel = GameObject.Find("clues_items_panel");
                    ItemsPanelManager itemsPanelManager = clues_items_panel.GetComponent<ItemsPanelManager>();
                    itemsPanelManager.addItem(this);

                    UI_Manager.getInstance().showMessage("This clue is now added to your clue list showing on the right of the screen");
                });
                if (gameObject.GetComponent<AudioSource>() != null)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
            else if (EscapeRoom_item.type == "text")
            {
                UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content, Resources.Load<Sprite>(EscapeRoom_item.img), () => {
                    Debug.Log("clue clicked");
                    this.isArchived = true;
                    //get gameobject with the name "inventory_items_panel"
                    GameObject clues_items_panel = GameObject.Find("clues_items_panel");
                    ItemsPanelManager itemsPanelManager = clues_items_panel.GetComponent<ItemsPanelManager>();
                    itemsPanelManager.addItem(this);

                    UI_Manager.getInstance().showMessage("This clue is now added to your clue list showing on the right of the screen");
                });
            }
            else
            {
                UI_Manager.getInstance().showInfo(EscapeRoom_item.title, EscapeRoom_item.content, Resources.Load<Sprite>(EscapeRoom_item.img), () => {
                    Debug.Log("clue clicked");
                    this.isArchived = true;
                    //get gameobject with the name "inventory_items_panel"
                    GameObject clues_items_panel = GameObject.Find("clues_items_panel");
                    ItemsPanelManager itemsPanelManager = clues_items_panel.GetComponent<ItemsPanelManager>();
                    itemsPanelManager.addItem(this);

                    UI_Manager.getInstance().showMessage("This clue is now added to your clue list showing on the right of the screen");
                });
            }
        }
    }
}
