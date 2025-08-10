using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorOpener : MonoBehaviour
{ 
    private Vector3 alternateRotation;
    public float rotateYvalueToChange = 90;
    public bool open = false;
    public bool locked = false;

    void Start()
    {
        alternateRotation = transform.localEulerAngles;
    }

    public void OpenDoor()
    {
        if (DOTween.IsTweening(transform)) return; 

        if (locked)
        {
            UI_Manager.getInstance().showMessage("The door is locked... Need to unlock it first...");
            return;
        }

        if (!open)
        {
            Vector3 targetRotation = alternateRotation + new Vector3(0, rotateYvalueToChange, 0);

            transform.DOLocalRotate( targetRotation, 1).onComplete += () =>
            {
                // Debug.Log($"Door rotated to {targetRotation}");
                open = true;
            };
        }
        else
        {
            
            transform.DOLocalRotate(alternateRotation, 1).onComplete += () =>
            {
                Debug.Log($"D/oor rotated back to {alternateRotation}");
                open = false;
            };
        }
    } 
}
