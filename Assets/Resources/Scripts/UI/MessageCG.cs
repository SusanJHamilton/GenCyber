using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MessageCG : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public CanvasGroup messageCG;
    public Button closeButton;
    public float messageDuration = 5;   //how long the message will be displayed
    public bool isMessageAutomaticallyDismissed = true; //if the message will be automatically dismissed after the designated duration
    private float messageStartTime; //the time the message was shown


    void Start()
    {
        messageCG.alpha = 0;
        messageCG.interactable = false;
        messageCG.blocksRaycasts = false;
        closeButton.onClick.AddListener(() => {
            messageCG.DOFade(0, 0.1f).onComplete += () => {
                messageCG.interactable = false;
                messageCG.blocksRaycasts = false;
            };
        });
    }


    //when the message is already shown, change the text and update the time
    public void ShowMessage(string message)
    {
        if (messageCG.alpha == 0){  //not shown yet
            messageText.text = message; 
            messageCG.DOFade(1, 0.1f).onComplete += () => {
                messageCG.interactable = true;
                messageCG.blocksRaycasts = true;
                messageStartTime = Time.time;
            }; 
        }else{
            messageText.text = message;
            messageStartTime = Time.time;
        }

        if (isMessageAutomaticallyDismissed)
            StartCoroutine(DismissMessage());
    }

    //dismiss the message after the designated duration
    IEnumerator DismissMessage()
    {
        yield return new WaitForSeconds(messageDuration);
        messageCG.DOFade(0, 0.1f).onComplete += () => {
            messageCG.interactable = false;
            messageCG.blocksRaycasts = false;
        };
    }
}
