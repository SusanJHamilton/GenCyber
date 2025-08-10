using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject mainUI;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;
    public Button option1Button;
    public Button option2Button;

    public Lobby lobbyScript; // Reference to Lobby
    public Transform robotMoveTarget; // Position robot walks to after talking
    public GameObject levelSelectUI;

    private int dialogueStage = 0;
    private Coroutine typingCoroutine;
    public float typingSpeed = 0.03f; // Adjust typing speed here

    void Start()
    {
        StartConversation();
    }

    public void StartConversation()
    {
        dialoguePanel.SetActive(true);
        mainUI.SetActive(false);

        // Clean all buttons
        continueButton.onClick.RemoveAllListeners();
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        continueButton.gameObject.SetActive(false);
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);

        dialogueStage = 0;

        StartTyping("Welcome, Operative. I am Agent R-01, your mission handler. You’ve been selected for a critical operation in the fight against digital threats.", () =>
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.AddListener(HandleContinue);
        });
    }


    void HandleContinue()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.gameObject.SetActive(false);

        dialogueStage++;

        switch (dialogueStage)
        {
            case 1:
                StartTyping("A rogue hacker known only as Cipher has embedded a series of encrypted traps across our network. Your task: infiltrate the simulation, uncover hidden clues, and crack the passwords protecting our core systems.", () =>
                {
                    option1Button.gameObject.SetActive(true);
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "What’s the objective?";
                    option1Button.onClick.AddListener(HandleObjective);
                });
                break;

            default:
                break;
        }
    }

    void HandleObjective()
    {
        option1Button.onClick.RemoveAllListeners();
        option1Button.gameObject.SetActive(false);

        StartTyping("You’ll enter a virtual escape room. Inside, you must collect keywords, analyze clues, and simulate a dictionary attack to breach Cipher’s defenses. Each level reveals more about Cipher’s motives—and your own skills.", () =>
        {
            option2Button.gameObject.SetActive(true);
            option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "I’m ready";
            option2Button.onClick.AddListener(HandleReady);
        });
    }


    void HandleReady()
    {
        option2Button.onClick.RemoveAllListeners();
        option2Button.gameObject.SetActive(false);

        StartTyping("Good. Your training begins now. Remember: every detail matters. Trust your instincts. And Operative… don’t get locked in.", () =>
        {
            StartCoroutine(FinishConversation());
        });
    }


    IEnumerator FinishConversation()
    {
        yield return new WaitForSeconds(2f);
        dialoguePanel.SetActive(false);
        //mainUI.SetActive(true);
        lobbyScript.SendRobotBack();
    }

    // Typing coroutine helper
    void StartTyping(string message, System.Action onComplete = null)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeTextCoroutine(message, onComplete));
    }

    IEnumerator TypeTextCoroutine(string message, System.Action onComplete)
    {
        dialogueText.text = "";
        foreach (char letter in message)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }


        onComplete?.Invoke();
    }

    public void ShowHintWithContinue(string message, System.Action onContinue)
    {
        dialoguePanel.SetActive(true);

        continueButton.onClick.RemoveAllListeners();
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false); // Hide during typing

        StartTyping(message, () =>
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.AddListener(() =>
            {
                continueButton.onClick.RemoveAllListeners();
                dialoguePanel.SetActive(false);
                onContinue?.Invoke();
            });
        });
    }
}
