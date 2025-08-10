using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public bool collected = false;
    public bool childDisable = true;

    public void collect() // this function disables collisions and rendering for the object (and any children), making it effectively disappear, without disabling the script
    {
        collected = true;

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        MeshCollider collider = this.gameObject.GetComponent<MeshCollider>();
        BoxCollider bcollider = this.gameObject.GetComponent<BoxCollider>();

        if (renderer != null) { renderer.enabled = false; }
        if (collider != null) { collider.enabled = false; }
        if (bcollider != null) { bcollider.enabled = false; }

        if (childDisable)
        {
            MeshRenderer childRenderer;
            MeshCollider childCollider;
            BoxCollider childbCollider;

            Transform transform = this.gameObject.GetComponent<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                childRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
                if (childRenderer != null) { childRenderer.enabled = false; }

                childCollider = transform.GetChild(i).GetComponent<MeshCollider>();
                if (childCollider != null) { childCollider.enabled = false; }

                childbCollider = transform.GetChild(i).GetComponent<BoxCollider>();
                if (childbCollider != null) { childbCollider.enabled = false; }
            }
        }
    }
}
