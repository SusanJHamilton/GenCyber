using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrawerOpener : MonoBehaviour
{
    public float ZpositionToOpen = 0.6f;
    private Vector3 initialPosition;
    public bool locked = false;
    public string keyName;
    public bool open = false;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void interact()
    {
        if (DOTween.IsTweening(transform)) return;

        if (locked)
        {
            UI_Manager.getInstance().showMessage("This drawer is locked... Need to unlock it first...");
            return;
        }

        if (open)
        {
            transform.DOLocalMove(initialPosition, 1).onComplete += () =>
            {
                open = false;
            };
        }
        else
        {
            transform.DOLocalMove(initialPosition + new Vector3(0, 0, ZpositionToOpen), 1).onComplete += () =>
            {
                open = true;
            };
        }
    }
}
