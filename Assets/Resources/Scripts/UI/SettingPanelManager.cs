using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    public Slider horizontalSensitivitySlider;
    public Slider verticalSensitivitySlider;
    public GameObject mouseLookGameObject;

    void Start()
    {
        horizontalSensitivitySlider.onValueChanged.AddListener((float value) =>
        {
            mouseLookGameObject.GetComponent<MouseLook>().setXMouseSensitivity(value);
        });

        verticalSensitivitySlider.onValueChanged.AddListener((float value) =>
        {
            mouseLookGameObject.GetComponent<MouseLook>().setYMouseSensitivity(value);
        });
    }
}
