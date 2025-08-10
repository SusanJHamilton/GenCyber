using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPanelScript : MonoBehaviour
{
    public HoldButton fwdBtn;
    public HoldButton leftBtn;
    public HoldButton backBtn;
    public HoldButton rightBtn;

    public MouseLook playerMovement;

    public float sensitivity = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (fwdBtn.press)
        {
            playerMovement.targetDirection += new Vector2(-sensitivity, 0);
        }

        if (leftBtn.press)
        {
            playerMovement.targetCharacterDirection += new Vector2(0, -sensitivity);
        }

        if (backBtn.press)
        {
            playerMovement.targetDirection += new Vector2(sensitivity, 0);
        }

        if (rightBtn.press)
        {
            playerMovement.targetCharacterDirection += new Vector2(0, sensitivity);
        }
    }
}
