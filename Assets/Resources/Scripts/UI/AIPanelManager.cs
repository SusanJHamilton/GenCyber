using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using OpenAI;
// using OpenAI.Chat;
using OpenAI.Models;
using OpenAI.Assistants;
using OpenAI.Threads; 
using UnityEngine.Events;
using System;



public class AIPanelManager : MonoBehaviour 
{
    public float buttonClickDelay = 1.5f;
    public float buttonClickDelayCounter = 0f;
    // public RectTransform draggablePanel; 
    // public RectTransform heightAdjustHolder;    //when this image is dragged vertically, the height of the panel will be adjusted
    // public Canvas parent_canvas; 
    public VerticalLayoutGroup content_area;
    public GameObject basic_response_text_template; //this need to be duplicated and setEnabled to true and attached to content_area 
    public GameObject basic_input_text_template; //this need to be duplicated and setEnabled to true and attached to content_area
    public TMP_InputField inputField;
    public Button sendButton;
    // public string openAIKey;

    // private ThreadResponse thread;
    private OpenAIClient api;
    // private AssistantResponse assistant;

    void Update()
    {
        if (buttonClickDelayCounter > 0)
            buttonClickDelayCounter -= Time.deltaTime;
        if (buttonClickDelayCounter <= 0)
            buttonClickDelayCounter = 0;
    }

    // public AssistantResponse getAssistant(){
    //     return assistant;
    // }

    public async void Start(){
        
        
        
        string test1 = "proj-nYOLuc6pYtbn7n_G3Xf59Y27bXV0JSZmD0Q5x3DfzUTPHgfA";
        string test2 = "pl9z2bmIVT6MIWLpDKg0AxEu-iT3BlbkFJcw";
        string test3 = "9tfYuBzIeEVD7KKbttx0ifUw5W4";
        string test4 = "ZCIFi7fELUA4nyHYdL0FF5S4gienzCA4He5YTX6_bD6sA"; 
        api = new OpenAIClient("sk-"+test1+test2+test3+test4);
        init();
    }

    public GameObject instanitateTemplate(GameObject template, Transform parent, string content, ScrollRect scrollView, bool isTypeWriter = true)
    {
        template.gameObject.SetActive(true);
        GameObject new_response_text_template = Instantiate(template.gameObject, parent);
        template.gameObject.SetActive(false);
        TextMeshProUGUI new_response_text = new_response_text_template.GetComponentInChildren<TextMeshProUGUI>();
        new_response_text.text = content;
        new_response_text.GetComponent<AutoResizeTextUI>().forceUpdate();
        new_response_text_template.gameObject.SetActive(true);
        new_response_text_template.GetComponent<CanvasGroup>().DOFade(1, 1.5f).SetEase(Ease.OutCubic).OnComplete(() => { 
            scrollView.verticalNormalizedPosition = 0f;
        });

        if (isTypeWriter)
            Utils.setTypeWriterEffect(new_response_text, content, scrollView);

        return new_response_text_template;
    }

    void init()
    { 
        sendButton.onClick.AddListener(() => {
            if (buttonClickDelayCounter == 0)
            {
                buttonClickDelayCounter = buttonClickDelay;
                string message = inputField.text;
                if (message != "")
                {
                    ScrollRect scrollView = content_area.transform.parent.parent.GetComponent<ScrollRect>(); 
                    instanitateTemplate(basic_input_text_template, content_area.transform, message, scrollView, false);
                    //after this message, ChatGPT will respond
                    GameObject new_response_text_template = instanitateTemplate(basic_response_text_template, content_area.transform, "Thinking...", scrollView);
                    ChatGPT_instance chatGPT_Instance = new_response_text_template.GetComponent<ChatGPT_instance>();
                    chatGPT_Instance.sendMessageRequest(message, api, scrollView);
                    inputField.text = "";
                }
            }
        });
    }
  
 
}
