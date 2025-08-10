using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager_PasswordCracking : Abstract_GameManager
{
    public override void HandleObjectCollided(GameObject collidedObject)
    {
        if (collidedObject == null) return;
        Debug.Log("Listener detected collision with object: " + collidedObject?.name);
    }


    public override void HandleObjectClicked(GameObject clickedObject)
    {
        // if (clickedObject == null) return;
        // Debug.Log("Listener detected click on object: " + clickedObject?.name);
        // float distance = Vector3.Distance(avatar.transform.position, clickedObject.transform.position);

        // #region Doors
        // if (clickedObject.name.Contains("door"))
        // {
        //     if (distance >= 2)
        //     {
        //         UI_Manager.getInstance().showMessage("The door is too far away...");
        //         return;
        //     }
        //     //get DoorOpener component
        //     DoorOpener doorOpener = clickedObject.GetComponent<DoorOpener>();
        //     if (doorOpener != null)
        //         doorOpener.OpenDoor();
        // }
        // #endregion

        // #region Drawers
        // if (clickedObject.name.Contains("drawer"))
        // {
        //     if (distance >= 2)
        //     {
        //         UI_Manager.getInstance().showMessage("The drawer is too far away...");
        //         return;
        //     }

        //     DrawerOpener drawer = clickedObject.GetComponent<DrawerOpener>();
        //     if (drawer != null)
        //     {
        //         if (drawer.locked)
        //         {
        //             //need to outline code here for how it is unlocked
        //             if (avatar.GetComponent<PlayerInventory>().hasItem(drawer.keyName))
        //             {
        //                 drawer.interact();
        //             }
        //             else
        //             {
        //                 UI_Manager.getInstance().showMessage("The drawer seems to be locked...");
        //                 return;
        //             }
        //         }
        //         else
        //         {
        //             drawer.interact();
        //         }
        //     }
        // }
        // #endregion

        // #region Clues
        // if (clickedObject.name.Contains("clue"))
        // {
        //     if (distance >= 2)
        //     {
        //         UI_Manager.getInstance().showMessage("The item is too far away...");
        //         return;
        //     }

        //     ClueTypeAudio Aclue = clickedObject.GetComponent<ClueTypeAudio>();
        //     ClueOnObject clue = clickedObject.GetComponent<ClueOnObject>();

        //     if (Aclue != null)
        //     {
        //         if (!Aclue.isArchived)
        //         {
        //             Aclue.playAudio();
        //             UI_Manager.getInstance().showInfo(Aclue.name, Aclue.text, true, Aclue, clickedObject.GetComponent<InteractableObject>());
        //             clue = null;
        //         }
        //     }

        //     if (clue != null)
        //     {
        //         if (!clue.isArchived)
        //         {
        //             UI_Manager.getInstance().showInfo(clue.name, clue.text, true, clue, clickedObject.GetComponent<InteractableObject>());
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("no clue found");
        //     }
        // }
        // #endregion

        // #region Inventory Items
        // if (clickedObject.name.Contains("item"))
        // {
        //     if (distance >= 2)
        //     {
        //         UI_Manager.getInstance().showMessage("The item is too far away...");
        //         return;
        //     }
        //     //get Clue component
        //     InventoryItem item = clickedObject.GetComponent<InventoryItem>();
        //     if (item != null)
        //     {
        //         if (!item.collected)
        //         {
        //             UI_Manager.getInstance().showMessage(item.itemName + " collected. Press tab to open your inventory and view it");
        //             avatar.GetComponent<PlayerInventory>().addItem(item);
        //             item.collect();
        //             clickedObject.GetComponent<InteractableObject>().active = false;
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("Manager failedto find item");
        //     }
        // }
        // #endregion

        // #region Password Cracking PC
        // if (clickedObject.name.Contains("crack"))
        // {
        //     //check the distance between the avatar and the computer
        //     if (distance >= 1.5f)
        //     {
        //         UI_Manager.getInstance().showMessage("The computer is too far away...");
        //         return;
        //     }
        //     Debug.Log("Distance between avatar and crack_pc: " + distance);
        //     if (distance < 2)
        //     {
        //         Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = false; //disable camera movement

        //         avatar.transform.DOMove(clickedObject.GetComponent<PassWordCracking>().CamLocation, 1).onComplete += () => {
        //             Debug.Log("Avatar moved to crack_pc");
        //         };
        //         avatar.transform.DORotate(new Vector3(0, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1).onComplete += () => {
        //             Debug.Log("Avatar rotated to crack_pc");
        //         };

        //         //rotate camera to the screen
        //         Camera.main.transform.DORotate(new Vector3(clickedObject.GetComponent<PassWordCracking>().camAngle.x, clickedObject.GetComponent<PassWordCracking>().camAngle.y, 0), 1).onComplete += () => {
        //             Debug.Log("Camera rotated to crack_pc");
        //         };

        //         Camera.main.DOFieldOfView(clickedObject.GetComponent<PassWordCracking>().camFOV, 1).onComplete += () => {
        //             Debug.Log("Camera zoomed to crack_pc");
        //         };
        //     }
        //     else
        //     {
        //         Debug.Log("Avatar is too far from crack_pc");
        //     }
        // }
        // #endregion
    }
}
