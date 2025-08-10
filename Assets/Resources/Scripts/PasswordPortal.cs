using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PasswordPortal : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public CanvasGroup screenFade;

    public ItemsPanelManager itemsPanel;
    public ItemsPanelManager cluesPanel;
    public InteractableObject usb;

    private int level = 1;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ObjectDetector>() != null)
        {
            CanvasGroup cg = screenFade.GetComponent<CanvasGroup>();
            switch (level)
            {
                case 1:
                    level = 2;
                    cg.blocksRaycasts = true;
                    cg.interactable = true;
                    cg.DOFade(1, 1).onComplete += () => {
                        level1.SetActive(false);
                        level2.SetActive(true);
                        cg.DOFade(0, 1).onComplete += () => {
                            cg.blocksRaycasts = false;
                            cg.interactable = false;
                        };
                    };
                    break;
                case 2:
                    level = 3;
                    cg.blocksRaycasts = true;
                    cg.interactable = true;
                    cg.DOFade(1, 1).onComplete += () => {
                        level2.SetActive(false);
                        level3.SetActive(true);
                        cg.DOFade(0, 1).onComplete += () => {
                            cg.blocksRaycasts = false;
                            cg.interactable = false;
                        };
                    };
                    break;
                case 3:
                    UI_Manager.getInstance().showMessage("you have reached the end of the experience so far");
                    break;
            }
            this.gameObject.SetActive(false);

            itemsPanel.clearItems();
            itemsPanel.addItem(usb);
            cluesPanel.clearItems();
        }
    }
}