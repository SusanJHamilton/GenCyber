using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the abstract class to be used for each scene gamemanger 
public abstract class Abstract_GameManager : MonoBehaviour
{
    public ObjectDetector objectInteractionManager; 
    public GameObject objectDetected_current;
    public GameObject avatar;
    private GameObject lastObject; // used because of some odd issues 



    // Start is called before the first frame update
    public virtual void Start(){
        if (objectInteractionManager != null)
        {
            objectInteractionManager.OnObjectDetectedChanged += HandleObjectDetectedChanged;
            objectInteractionManager.OnObjectClicked += HandleObjectClicked;
            objectInteractionManager.OnObjectCollided += HandleObjectCollided;
            Debug.Log("Listener registered for object detection "+ objectInteractionManager.name); 
        }



    }

    public abstract void HandleObjectCollided(GameObject collidedObject);
    public abstract void HandleObjectClicked(GameObject clickedObject);
    public virtual void HandleObjectDetectedChanged(GameObject detectedObject){
        if (detectedObject != null)
        {
            Debug.Log("Listener detected new object: " + detectedObject?.name);
            objectDetected_current = detectedObject;

            //ensures the previous object's outline is disabled
            if (lastObject != null && lastObject.GetComponent<Outline>() != null)
            {
                if (lastObject.GetComponent<InteractableObject>() != null)
                {
                    if (lastObject.GetComponent<InteractableObject>().senseMode)
                    {
                        lastObject.GetComponent<Outline>().OutlineColor = new Color(0f, 0.8f, 0.8f, 1f);
                    }
                    else
                    {
                        lastObject.GetComponent<Outline>().enabled = false;
                    }
                }
                else
                {
                    lastObject.GetComponent<Outline>().enabled = false;
                }
                
            }
            lastObject = detectedObject;

            //in this case, enable Outline component
            if (objectDetected_current.GetComponent<Outline>() != null)
            {
                objectDetected_current.GetComponent<Outline>().OutlineColor = new Color(1, 1, 1, 1f);
                if (objectDetected_current.GetComponent<InteractableObject>() != null)
                { 
                    //when this go has ParticleSystem on its children, then disable the particle system and enable the outline
                    if (objectDetected_current.GetComponentInChildren<ParticleSystem>() != null)
                    {
                        objectDetected_current.GetComponentInChildren<ParticleSystem>().Stop();
                        objectDetected_current.GetComponentInChildren<ParticleSystem>().Clear();
                    }
                    objectDetected_current.GetComponent<Outline>().enabled = true; 
                } 
                else
                {
                    //when this go has ParticleSystem on its children, then disable the particle system and enable the outline
                    if (objectDetected_current.GetComponentInChildren<ParticleSystem>() != null)
                    {
                        objectDetected_current.GetComponentInChildren<ParticleSystem>().Stop();
                        objectDetected_current.GetComponentInChildren<ParticleSystem>().Clear();
                    }
                    objectDetected_current.GetComponent<Outline>().enabled = true;
                }  
            }
        }
        else
        {
            Debug.Log("Listener detected no object");
            if (objectDetected_current != null)
            {
                //in this case, disable Outline component
                if (objectDetected_current.GetComponent<Outline>() != null)
                {
                    if (objectDetected_current.GetComponent<InteractableObject>() != null)
                    {
                        if(objectDetected_current.GetComponent<InteractableObject>().senseMode)
                        {
                            objectDetected_current.GetComponent<Outline>().OutlineColor = new Color(0f, 0.8f, 0.8f, 1f);
                        }
                        else
                        {
                            objectDetected_current.GetComponent<Outline>().enabled = false;
                        }
                    }
                    else
                    {
                        objectDetected_current.GetComponent<Outline>().enabled = false;
                    }
                    //when this go has ParticleSystem on its children, then enable the particle system and disable the outline
                    if (objectDetected_current.GetComponentInChildren<ParticleSystem>() != null)
                    {
                        objectDetected_current.GetComponentInChildren<ParticleSystem>().Play();
                    }
                }

                objectDetected_current = null;
            }
        }
    }

    public virtual void OnDestroy()
    {
        if (objectInteractionManager != null)
        {
            objectInteractionManager.OnObjectDetectedChanged -= HandleObjectDetectedChanged;
            objectInteractionManager.OnObjectClicked -= HandleObjectClicked;
            objectInteractionManager.OnObjectCollided -= HandleObjectCollided;
        }
    }
}
