using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Mack M
/// 2024
/// 
/// DEPRACATED - unused script. replaced primarily by ClueOnObject.cs
/// </summary>

public class ClueObject : MonoBehaviour
{
    public string text = "blank clue"; // title for the clue
    public string[] keyords = new string[0]; // what keywords come from this clue?
    public ClueType typeOfClue = ClueType.text; // is this clue a simple text clue or a media file that gets played
    public float mediaDelay = 0.0f; // if it's a media file, how long does it play?
    private bool mouseOver = false; 
    private Outline outline; // a reference to the local outline
    public GameObject notebook; // this references to the notebook UI page
    public bool inactive = false; // if this clue has been found or is yet to be activated
    public ParticleSystem particles; // reference to the particle system used

    public enum ClueType
    {
        text,   // this is something that is read
        media   // this is something that is listened to or watched
    }

    public void Start() // sets up the outline
    {
        this.outline = this.GetComponent<Outline>();
    }

    public void Update() // checks for being clicked on
    {
        if (Input.GetMouseButtonDown(0) && mouseOver && !inactive)
        {
            //this.notebook.GetComponent<PlayerNotebook>().findClue(this.text, this.keyords);
            inactive = true;
            mouseOver = false;
            if (this.particles != null)
            {
                this.particles.Stop();
                this.particles.Clear();
            }
            this.outline.OutlineMode = Outline.Mode.OutlineOff;
        }
    }

    public void activate()
    {
        this.inactive = false;
    }

    public void deactivate()
    {
        this.inactive = true;
    }

    public void OnMouseOver() // enable outline
    {
        if (!inactive)
        {
            mouseOver = true;
            this.outline.OutlineMode = Outline.Mode.OutlineAll;
        }
    }

    public void OnMouseExit() // disable outline
    {
        if (!inactive)
        {
            mouseOver = false;
            this.outline.OutlineMode = Outline.Mode.OutlineOff;
        }
    }
}