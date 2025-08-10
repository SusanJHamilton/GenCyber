using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Locker : MonoBehaviour
{
    public string title;
    public string content;
    public string answer; 
    private GameObject parent;  // The parent object of the locker. this is a door typically

    //canvas part
    public CanvasGroup questionPanel;
    public TextMeshProUGUI questionTitle;
    public TextMeshProUGUI questionContent;
    public TextMeshProUGUI logText;
    public TMP_InputField answerInput;
    public Button submitButton;
     
    
    public void showQuestionUIPanel()
    {
        parent = transform.parent.gameObject;

        // show the question panel
        questionPanel.gameObject.SetActive(true);
        questionTitle.text = title;
        questionContent.text = content;
        
        questionPanel.DOFade(1, 0.2f).onComplete += () => {
            Debug.Log("Info shown");
            questionPanel.interactable = true;
            questionPanel.blocksRaycasts = true;
        };

        submitButton.onClick.AddListener(() => {
            if (answerInput.text.ToLower() == answer.ToLower())
            {
                Debug.Log("Correct answer");
                logText.text = "Correct answer!";
                UI_Manager.getInstance().showMessage("Correct answer! The locker is now unlocked...");
                questionPanel.DOFade(0, 0.2f).onComplete += () => {
                    questionPanel.gameObject.SetActive(false);
                    questionPanel.interactable = false;
                    questionPanel.blocksRaycasts = false;
                    //change the tag of this gameobject
                    gameObject.tag = "Untagged";
                    //if the parent object has a DoorOpener component, unlock it and open the door
                    if (parent.GetComponent<DoorOpener>() != null)
                    {
                        parent.GetComponent<DoorOpener>().locked = false;
                        parent.GetComponent<DoorOpener>().OpenDoor();
                    }else if(parent.GetComponent<DrawerOpener>() != null)
                    {
                        parent.GetComponent<DrawerOpener>().locked = false;
                        parent.GetComponent<DrawerOpener>().interact();
                    }
                };
            }
            else
            {
                logText.text = "Wrong answer. Try again! If you do not know the answer, you can find it in the book shelf...";
            }
        });
    }
}
