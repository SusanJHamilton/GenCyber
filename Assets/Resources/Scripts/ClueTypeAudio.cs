using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTypeAudio : ClueOnObject
{
    public AudioSource audio = null;

    public void playAudio()
    {
        if (audio != null) 
        {
            audio.Play();
        }
    }
}
