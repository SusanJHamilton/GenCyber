using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager_IntroRoom : Abstract_GameManager
{ 
    public GameObject portal;

    public override void HandleObjectCollided(GameObject collidedObject)
    {
        if (collidedObject == null) return;
        Debug.Log("Listener detected collision with object: " + collidedObject?.name);
    }
    
    public override void HandleObjectClicked(GameObject clickedObject)
    {
        if (clickedObject == null) return;
        Debug.Log("Listener detected click on object: " + clickedObject?.name);
        float distance = Vector3.Distance(avatar.transform.position, clickedObject.transform.position);
        
        if (clickedObject.name == "cabinet_door"){
            if (distance >= 2)
            {
                UI_Manager.getInstance().showMessage("The cabinet is too far away...");
                return;
            }
            //when tweeing, return
            if (DOTween.IsTweening(clickedObject.transform)) return;
             Debug.Log("Door opened"+ clickedObject.transform.localEulerAngles);

            if ( (int)(clickedObject.transform.localEulerAngles.y) == 270){
                clickedObject.transform.DOLocalRotate(new Vector3(-90, 0, 0), 1);
                Debug.Log("Door !opened"+ clickedObject.transform.localEulerAngles);
            } else {
                clickedObject.transform.DOLocalRotate(new Vector3(-90, -90, 0), 1).onComplete += () => { 
                    Debug.Log("Door opened"+ clickedObject.transform.localEulerAngles);
                };
            }
        }

        if (clickedObject.name == "computer"){ 
            //check the distance between the avatar and the computer
            if (distance >= 1.2f)
            {
                UI_Manager.getInstance().showMessage("The computer is too far away...");
                return;
            }
            Debug.Log("Distance between avatar and computer: " + distance);
            //if the distance is less than 2, move the avatar to the computer
            if (distance < 2){ 
                Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = false; //disable camera movement

                avatar.transform.DOMove( new Vector3(-1.44885f, 1, 5.450035f), 1).onComplete += () => {
                    Debug.Log("Avatar moved to computer");
                };
                avatar.transform.DORotate( new Vector3(0, -179f, 0), 1).onComplete += () => {
                    Debug.Log("Avatar rotated to computer");
                };

                //camera rotation 4.03, 0 ,0
                Camera.main.transform.DORotate( new Vector3(4.03f, 180, 0), 1).onComplete += () => {
                    Debug.Log("Camera rotated to computer");
                };

                Camera.main.DOFieldOfView(24.5f, 1).onComplete += () => {
                    Debug.Log("Camera zoomed to computer");
                };
            } else {
                Debug.Log("Avatar is too far from computer");
            } 
        }

        if (clickedObject.name == "first_paper"){
            if (distance >= 2f)
            {
                UI_Manager.getInstance().showMessage("That book is too far away...");
                return;
            }
            
            UI_Manager.getInstance().showInfo("Paper", "The paper reads: 'The portal is the key to the future'");
        }

        #region this part is for UI components
        if (clickedObject.name == "computer_exit"){
            Camera.main.DOFieldOfView(60f, 1).onComplete += () => {
                Debug.Log("Camera zoomed to computer");
                Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = true; //enable camera movement
            }; 
        }

        if (clickedObject.name == "acceptBtn_introRoom"){
            //show info message
            UI_Manager.getInstance().showMessage("It feels like the portal is calling me...");
            Camera.main.DOFieldOfView(60f, 1).onComplete += () => {
                Debug.Log("Camera zoomed to computer");
                Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = true; //enable camera movement
                portal.SetActive(true);
            }; 
        }

        #endregion
        
    }
 
}
