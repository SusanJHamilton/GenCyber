using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using System;
using System.Linq;

/// <summary>
/// Author: Mack M
/// 2024
/// 
/// this file is designed to handle the password cracking underlying systems.
/// TODO: touch up UI, impliment door opening, make the password cracking UI (generate dictionary of possible passwords)
/// </summary>

public class PassWordCracking : MonoBehaviour
{
    // variables! fun
    public string[] requiredInputs;
    private List<string> dictionary = new List<string>(50);
    public TextMeshProUGUI dictField;
    private string[] inputStrings;
    private int inputLength;
    private int dictLength;
    public int requiredCorrect;
    public string password;
    public Vector3 CamLocation;
    public Vector2 camAngle;
    public float camFOV;
    public GameObject portal;
    public GameObject avatar;
    public PlayerNotebook notebook;
    public Button checkPassword;
    public Button crackPassword;
    public Button exit;
    public Button USB;
    public Button runAttack;
    public TextMeshProUGUI attackResultText;
    public TMP_InputField passwordInput;
    public TMP_InputField crackInput;
    public TextMeshProUGUI crackPlaceholder;
    private string crackPlaceText;
    public TextMeshProUGUI passwordPlaceholder;
    public GameObject ui;
    public GameObject crackUI;
    public Image tempPassFail;
    public bool active = true;
    private bool passed = false;

    // Start is called before the first frame update
    void Start()
    {
        this.crackPlaceText = this.crackPlaceholder.text;

        crackPassword.onClick.AddListener(() => {
            if (this.passed)    return;
            updateDictfield();
        });

        // checkPassword.onClick.AddListener(() => {
        //     if (this.active)
        //     {
        //         crackPasswordVisuals(this.passwordInput.text.Equals(password), false);
        //     }
        // });

        exit.onClick.AddListener(() => {
            closePasswordCrackingUI();
        });

        USB.onClick.AddListener(() => {
            openCrack();
        });

        runAttack.onClick.AddListener(() => {
            if (this.passed)    return;

            crackPasswordVisuals(passwordCrackCheck(), true);
        });
    }

    public void updateDictfield(){
        if (crackInput.text != null && crackInput.text != ""){
            //remove space from text
            // note from mack, i don't like this. means we can't have commas in passwords. i'd prefer text.Split(", ");
            string text = crackInput.text.Replace(" ", "");
            inputStrings = text.Split(",");
            inputLength = inputStrings.Length;
            Debug.Log("inputLength: " + inputLength);

            dictionary = new List<string>(50);
            dictLength = 0;

            #region this generates the dictionary. it is hell. i reccomend collapsing this for loop.
            for (int i = 0; i < inputLength; i++)
            {
                for (int j = i + 1; j < inputLength; j++)
                {
                    dictionary.Add(inputStrings[i] + inputStrings[j]);
                    dictLength++;

                    dictionary.Add(inputStrings[j] + inputStrings[i]);
                    dictLength++;

                    for (int k = j + 1; k < inputLength; k++)
                    {
                        dictionary.Add(inputStrings[i] + inputStrings[j] + inputStrings[k]);
                        dictLength++;

                        dictionary.Add(inputStrings[i] + inputStrings[k] + inputStrings[j]);
                        dictLength++;

                        dictionary.Add(inputStrings[j] + inputStrings[i] + inputStrings[k]);
                        dictLength++;

                        dictionary.Add(inputStrings[j] + inputStrings[k] + inputStrings[i]);
                        dictLength++;

                        dictionary.Add(inputStrings[k] + inputStrings[j] + inputStrings[i]);
                        dictLength++;

                        dictionary.Add(inputStrings[k] + inputStrings[i] + inputStrings[j]);
                        dictLength++;

                        for (int l = k + 1; l < inputLength; l++)
                        {
                            dictionary.Add(inputStrings[i] + inputStrings[j] + inputStrings[k] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[i] + inputStrings[j] + inputStrings[l] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[i] + inputStrings[k] + inputStrings[l] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[i] + inputStrings[k] + inputStrings[j] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[i] + inputStrings[l] + inputStrings[j] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[i] + inputStrings[l] + inputStrings[k] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[i] + inputStrings[k] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[i] + inputStrings[l] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[k] + inputStrings[l] + inputStrings[i]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[k] + inputStrings[i] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[l] + inputStrings[i] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[j] + inputStrings[l] + inputStrings[k] + inputStrings[i]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[j] + inputStrings[i] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[j] + inputStrings[l] + inputStrings[i]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[i] + inputStrings[l] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[i] + inputStrings[j] + inputStrings[l]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[l] + inputStrings[j] + inputStrings[i]);
                            dictLength++;

                            dictionary.Add(inputStrings[k] + inputStrings[l] + inputStrings[i] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[i] + inputStrings[k] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[i] + inputStrings[j] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[k] + inputStrings[j] + inputStrings[i]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[k] + inputStrings[i] + inputStrings[j]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[j] + inputStrings[i] + inputStrings[k]);
                            dictLength++;

                            dictionary.Add(inputStrings[l] + inputStrings[j] + inputStrings[k] + inputStrings[i]);
                            dictLength++;
                        }
                    }
                }
            }
            #endregion

            dictField.text = string.Join(", ", dictionary.ToArray());
        }
        else
        {
            dictField.text = "";
        }  
    } 

    public void openPasswordCrackingUI()
    {
        GraphicRaycaster gr = GetComponentInChildren<GraphicRaycaster>(true);
        gr.enabled = true;

        // Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = false;
        // GameObject avatar = GameObject.Find("GameManager");
        // Abstract_GameManager gameManager = GameObject.Find("GameManager").GetComponent<Abstract_GameManager>();
        // avatar = gameManager.avatar;  

        // avatar.transform.DOMove( new Vector3(-1.057953f, 1, 5.360106f), 0.5f);
        // //set avatar rotation to 0, -179, 0
        // avatar.transform.DORotate( new Vector3(0, -179f, 0), 0.5f).onComplete += () => {
        //     //set camera rotation to 5.6, 0, 0
        //     Camera.main.transform.DOLocalRotate( new Vector3(5.6f, 0, 0), 0.5f);
        // }; 
        
        Camera.main.DOFieldOfView(27f, 0.5f);
    }

    void openCrack()
    {
        this.crackUI.gameObject.SetActive(true);
        this.USB.gameObject.SetActive(false);
    }

    void closePasswordCrackingUI()
    {
        Camera.main.DOFieldOfView(60f, 0.5f).onComplete += () => {
            Camera.main.gameObject.GetComponent<MouseLook>().isInputEnabled = true; //enable camera movement
            //get Canvas component from its children
            GraphicRaycaster gr = GetComponentInChildren<GraphicRaycaster>(true);
            gr.enabled = false; 
        };
        this.crackUI.gameObject.SetActive(false);
        this.USB.gameObject.SetActive(true);
    }

    bool passwordCrackCheck() // checks each required input. if a single one is not in the iputs, the password will not be cracked.
    {
        string text = crackInput.text.Replace(" ", "");
        string[] inputs = text.Split(",");
        
        int correct = 0;

        for (int i = 0; i < this.requiredInputs.Length; i++)
        {
            if (inputs.Contains(this.requiredInputs[i]))
            {
                correct++;
            }
        }

        if (correct >= requiredCorrect)
        {
            return true;
        } else {
            return false;
        }
    }

    void crackPasswordVisuals(bool pass, bool crack) // gives visual feedback for the pass/fail
    {
        int try_count = 0;
        // attackResultText.text = "Attack Try Count: " + try_count;
        //TODO: impliment door opening
        if (crack)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                if (i <= 60)
                {
                    StartCoroutine(dictAttack(i));
                }
            }
        }
        StartCoroutine(passCheck(pass));
    }

    IEnumerator resetColor() // waits 1 second before restting the color of the pass/fail
    {
        yield return new WaitForSeconds(Math.Min(((float)dictionary.Count * 0.0166666666666f), 1) + 1);
        this.tempPassFail.color = new Color32(100, 100, 100, 100);
    }

    IEnumerator dictAttack(int wait) // runs the visuals for cracking the password
    {
        yield return new WaitForSeconds((float)wait * 0.0166666666666f);
        this.passwordPlaceholder.text = this.dictionary[wait];
    }

    IEnumerator passCheck(bool pass)
    {
        yield return new WaitForSeconds(Math.Min(((float)dictionary.Count * 0.0166666666666f), 1));
        Debug.Log("Password cracked: " + pass);
        if (pass)
        {
            this.crackInput.text = "";
            this.tempPassFail.color = new Color32(100, 255, 100, 100);
            this.active = false;
            this.passed = true;
            attackResultText.text = "Password Cracked: " + this.password;

            yield return new WaitForSeconds(3);
            closePasswordCrackingUI();
            
            UI_Manager.getInstance().showInfo("Password Cracked! Access Granted.", "As the PC unlocks, you hear a noise behind you. The PC Quickly locks itself again and becomes unresponsive.");
            this.portal.SetActive(true);
        }
        else
        {
            this.tempPassFail.color = new Color32(250, 255, 100, 100);
            attackResultText.text = "Password Not Cracked!";
            StartCoroutine(resetColor());
        }
        this.passwordPlaceholder.text = "";
    }
}