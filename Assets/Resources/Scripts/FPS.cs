using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FPS : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //display FPS
        float fps = 1 / Time.deltaTime;
        text.text = "FPS: " + Mathf.Round(fps);

    }
}
