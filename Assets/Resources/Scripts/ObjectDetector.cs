using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{ 
    public float detectFrequency = 20; //how many times per second to detect objects (this is to reduce the number of raycasts per second)
    public float distanceToInteract = 20;
    private float nextDetectTime;  
    public event Action<GameObject> OnObjectDetectedChanged;
    public event Action<GameObject> OnObjectClicked;
    public event Action<GameObject> OnObjectCollided;
    private GameObject objectDetected; 
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Interactable"))
        {
            OnObjectCollided?.Invoke(collision.collider.gameObject);
            Debug.Log($"Collided with {collision.collider.gameObject.name}");
        }
    }

    private void setObjectDetected(GameObject value)
    {  
        if (objectDetected == value) return;
        objectDetected = value;
        OnObjectDetectedChanged?.Invoke(objectDetected); 
    }
    void Update()
    { 
        //block when the cursor is on UI
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (nextDetectTime <= Time.time)
        {
            DetectObject();
            nextDetectTime = Time.time + 1f / detectFrequency; 
        }

        if (Input.GetMouseButtonDown(0))    //when clicked
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, distanceToInteract))
            {
                if(hit.collider.tag == "Interactable")
                {   
                    OnObjectClicked?.Invoke(hit.collider.gameObject);
                }
            }
        }
    }

    void DetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceToInteract))
        {
            if(hit.collider.tag == "Interactable")
            {  
                setObjectDetected(hit.collider.gameObject); 
            }
            else
            {
                setObjectDetected(null);
            }
        }
        else
        {
            setObjectDetected(null);
        }
    }
}
