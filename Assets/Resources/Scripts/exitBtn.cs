using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class exitBtn : MonoBehaviour
{
    private Button exitButton;
    public CanvasGroup cg;
    // Start is called before the first frame update
    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(() => {
            if (cg == null) return;
            cg.DOFade(0, 0.1f).onComplete += () => {
                cg.interactable = false;
                cg.blocksRaycasts = false;
                cg.gameObject.SetActive(false);
            };
        });
    } 
}
