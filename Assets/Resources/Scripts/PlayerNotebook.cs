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
/// this script is designed to handle the player's notebook.
/// it stores clues the player finds in a 'notebook.' looking UI
/// TODO: touch up UI
/// </summary>

public class PlayerNotebook : MonoBehaviour
{
    #region UI part
    public GameObject hint_item_template;
    public VerticalLayoutGroup hint_panel;
    public CanvasGroup myNoteCG;
    public Button openMyNoteButton;
    #endregion


    private List<ClueOnObject> clues = new List<ClueOnObject>();
    public GameObject notebookUI;

    public void findClue(ClueOnObject clue)
    {
        this.clues.Add(clue);

        //update UI
        hint_item_template.SetActive(true);
        GameObject hint_item = Instantiate(hint_item_template, hint_panel.transform);
        hint_item_template.SetActive(false);
        hint_item.transform.GetChild(0).GetComponent<Image>().sprite = clue.icon;
        // hint_item.GetComponentInChildren<TextMeshProUGUI>().text = clue.text;
        // hint_item.GetComponent<Button>().onClick.AddListener(() => {
        //     Debug.Log("Clue clicked");
        //     UI_Manager.getInstance().showInfo(clue.name, clue.text, null, false, clue);
        // });
    }

    void Start()
    {
        openMyNoteButton.onClick.AddListener(() => {
            myNoteCG.gameObject.SetActive(true);
            if (myNoteCG.alpha == 0)
            {
                myNoteCG.DOFade(1, 0.2f);
                myNoteCG.interactable = true;
                myNoteCG.blocksRaycasts = true;
            }
            else
            {
                myNoteCG.DOFade(0, 0.2f);
                myNoteCG.interactable = false;
                myNoteCG.blocksRaycasts = false;
            }
        });
    }

    public void resetClues() // removes all clues - used for when moving to new challenge.
    {
        this.clues = new List<ClueOnObject>();
    }

    public List<ClueOnObject> getClues() // returns the clues - unused currently, but good to have
    {
        return this.clues;
    }
}