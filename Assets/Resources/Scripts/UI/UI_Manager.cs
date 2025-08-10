using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class UI_Manager : MonoBehaviour
{
    public CanvasGroup info_cg;
    public TextMeshProUGUI titleText_in_info;
    public TextMeshProUGUI contentText_in_info;
    public Image image_in_info;
    public CanvasGroup message_cg; 
    public Button archive_btn;
    public Button how_to_play_btn;
    public PlayerNotebook notebook;


    private static UI_Manager instance;

    public static UI_Manager getInstance(){
        return instance;
    }

    void Awake(){
        if (instance == null){
            instance = this;
        }
    }

    void Start(){
        how_to_play_btn.onClick.AddListener(() => {
            EscapeRoomItem welcomeItem = EscapeRoomItemsLoader.getInstance().GetItemById("welcome_message");
            Debug.Log("Welcome item: " + welcomeItem.title);
            showInfo(welcomeItem.title, welcomeItem.content, null);
        });
    }

    public void showMessage(string text){
        message_cg.GetComponent<MessageCG>().ShowMessage(text);
    }

    public void showInfo(string title, string content, Sprite image = null, UnityAction achiveCallback = null, bool additionalFlagToControlAchieveButton = false){
        Debug.Log("Showing info");
        info_cg.gameObject.SetActive(true); 

        //get ScrollRect component
        ScrollRect scrollRect = info_cg.GetComponentInChildren<ScrollRect>();
        //reset the scroll position
        scrollRect.verticalNormalizedPosition = 1;


        titleText_in_info.text = title;
        contentText_in_info.text = content;
        if (image != null)
        {
            image_in_info.sprite = image;
            image_in_info.gameObject.SetActive(true);
        }else{
            image_in_info.gameObject.SetActive(false);
        }

        info_cg.DOFade(1, 0.2f).onComplete += () => { 
            info_cg.interactable = true;
            info_cg.blocksRaycasts = true;
        };

        if (achiveCallback == null)
        {
            archive_btn.gameObject.SetActive(false);
            return;
        }else{
            archive_btn.gameObject.SetActive(true);
            if (additionalFlagToControlAchieveButton)
            {
                TextMeshProUGUI btn_text = archive_btn.GetComponentInChildren<TextMeshProUGUI>();
                btn_text.text = "Close";
            }else{
                TextMeshProUGUI btn_text = archive_btn.GetComponentInChildren<TextMeshProUGUI>();
                btn_text.text = "Archive this";}
        }

        archive_btn.onClick.RemoveAllListeners();
        
        archive_btn.onClick.AddListener(() => {
            // Debug.Log("Add to notebook clicked");
            // clue.isArchived = true;
            // //change tag name of the object
            // clue.gameObject.tag = "Untagged";

            info_cg.DOFade(0, 0.5f).onComplete += () => {
                info_cg.interactable = false;
                info_cg.blocksRaycasts = false;
                info_cg.gameObject.SetActive(false);
                if (achiveCallback != null)
                    achiveCallback.Invoke();

                // notebook.findClue(clue);
                // showMessage("The clue has been added to your notebook. You can view your notebook on the right side of your screen.");
            };
            
        });
    } 
}
