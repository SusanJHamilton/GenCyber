using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager_Room1 : Abstract_GameManager
{


  //start inherited from Abstract_GameManager
    public override void Start()
    {
        base.Start();

        //show welcome message
        Utils.startDelayedAction( ()=> {
            EscapeRoomItem welcomeItem = EscapeRoomItemsLoader.getInstance().GetItemById("welcome_message");
            Debug.Log("Welcome item: " + welcomeItem.title);
            UI_Manager.getInstance().showInfo(welcomeItem.title, welcomeItem.content, null, ()=>{
                UI_Manager.getInstance().showMessage("I need to look around and find clues to escape...");
            }, true);
        }, 1f);
    }

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

        bool isRobot_with_ChatPanel = clickedObject.name.Contains("Robot_with_chat_panel");
        if (isRobot_with_ChatPanel)
        {
            if (distance >= 1.5f)
            {
                UI_Manager.getInstance().showMessage("The robot is too far away...");
                return;
            }
            
            //rotate 180 degrees on y axis
            if (DG.Tweening.DOTween.IsTweening(clickedObject.transform)) 
                return;

            //check if the canvas alpha is 1
            CanvasGroup chatPanelCanvasGroup = clickedObject.GetComponentInChildren<CanvasGroup>();
            if (chatPanelCanvasGroup.alpha == 0) //means the chat panel is hidden
            {
                chatPanelCanvasGroup.alpha = 1;
                chatPanelCanvasGroup.blocksRaycasts = true;
                chatPanelCanvasGroup.interactable = true;
                clickedObject.transform.DOLocalRotate(new Vector3(0, clickedObject.transform.rotation.eulerAngles.y + 180, 0), 0.5f);
            }else{
                chatPanelCanvasGroup.alpha = 0;
                chatPanelCanvasGroup.blocksRaycasts = false;
                chatPanelCanvasGroup.interactable = false;
                clickedObject.transform.DOLocalRotate(new Vector3(0, clickedObject.transform.rotation.eulerAngles.y - 180, 0), 0.5f);
            }
            return;
        }


        //check if the clickedObject contains DoorOpener component
        bool isDoor = clickedObject.GetComponent<DoorOpener>() != null;
        if (isDoor)
        {
            if (distance >= 2)
            {
                UI_Manager.getInstance().showMessage("The door is too far away...");
                return;
            }
            //get DoorOpener component
            DoorOpener doorOpener = clickedObject.GetComponent<DoorOpener>();
            if (doorOpener != null)
                doorOpener.OpenDoor();

            return;
        }

        //check if the clickedObject contains InteractableObject component
        bool isEscapeRoomItem = clickedObject.GetComponent<InteractableObject>() != null;
        if (isEscapeRoomItem)
        {
            if (distance >= 2)
            {
                UI_Manager.getInstance().showMessage("That object is too far away...");
                return;
            }
            InteractableObject interactableObject = clickedObject.GetComponent<InteractableObject>();
            interactableObject.showInfo();  //note that all callbacks are handled in interactableObject

            return;
        }

        //check if the clickedObject contains Locker component
        bool isLocker = clickedObject.GetComponent<Locker>() != null;
        if (isLocker)
        {
            if (distance >= 1.5f)
            {
                UI_Manager.getInstance().showMessage("The locker is too far away...");
                return;
            }
            Locker locker = clickedObject.GetComponent<Locker>();
            locker.showQuestionUIPanel();
            return;
        }

        //check if the clickedObject contains DrawerOpener component
        bool isDrawer = clickedObject.GetComponent<DrawerOpener>() != null;
        if (isDrawer)
        {
            if (distance >= 1.5f)
            {
                UI_Manager.getInstance().showMessage("The drawer is too far away...");
                return;
            }
            DrawerOpener drawer = clickedObject.GetComponent<DrawerOpener>();
            drawer.interact();
            return;
        } 

        bool isComputer = clickedObject.GetComponent<PassWordCracking>() != null;
        if (isComputer)
        {
            if (distance >= 1.2f)
            {
                UI_Manager.getInstance().showMessage("The computer is too far away...");
                return;
            }

            GameObject inventory_items_panel = GameObject.Find("inventory_items_panel");
            ItemsPanelManager itemsPanelManager = inventory_items_panel.GetComponent<ItemsPanelManager>();
            bool hasUSB = itemsPanelManager.findItem("usb");
            if (!hasUSB)
            {
                UI_Manager.getInstance().showMessage("You need a USB to crack the password. Perhaps there is one around here somewhere... I wonder why that drawer is locked?  ...");
                return;
            }

            PassWordCracking passWordCracking = clickedObject.GetComponent<PassWordCracking>();
            passWordCracking.openPasswordCrackingUI();


            // avatar.transform.DOMove(clickedObject.GetComponent<PassWordCracking>().CamLocation, 1);
            // avatar.transform.DORotate(new Vector3(0, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1);
            // //camera rotation 4.03, 0 ,0
            // Camera.main.transform.DORotate(new Vector3(clickedObject.GetComponent<PassWordCracking>().camAngle.x, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1);
            // Camera.main.DOFieldOfView(clickedObject.GetComponent<PassWordCracking>().camFOV, 1);
            
        }

        // #region password cracking PC. use late if needed on otherscenes.
        // if (clickedObject.name.Contains("crack"))
        // {
        //     //check the distance between the avatar and the computer
        //     if (distance >= 1.2f)
        //     {
        //         UI_Manager.getInstance().showMessage("The computer is too far away...");
        //         return;
        //     }
        //     Debug.Log("Distance between avatar and crack_pc: " + distance);
        //     //if the distance is less than 2, move the avatar to the computer
        //     if (distance < 2)
        //     {
        //         Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = false; //disable camera movement

        //         avatar.transform.DOMove(clickedObject.GetComponent<PassWordCracking>().CamLocation, 1);
        //         avatar.transform.DORotate(new Vector3(0, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1);
        //         //camera rotation 4.03, 0 ,0
        //         Camera.main.transform.DORotate(new Vector3(clickedObject.GetComponent<PassWordCracking>().camAngle.x, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1);
        //         Camera.main.DOFieldOfView(clickedObject.GetComponent<PassWordCracking>().camFOV, 1);
        //     }
        // }
        // #endregion

    }
}
