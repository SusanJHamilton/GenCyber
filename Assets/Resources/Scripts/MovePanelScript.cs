using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePanelScript : MonoBehaviour
{
    public HoldButton fwdBtn;
    public HoldButton leftBtn;
    public HoldButton backBtn;
    public HoldButton rightBtn;

    public MouseLook playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (fwdBtn.press)
        {
            playerMovement.fwdKeyAlt = true;
        } else
        {
            playerMovement.fwdKeyAlt = false;
        }

        if (leftBtn.press)
        {
            playerMovement.leftKeyAlt = true;
        }
        else
        {
            playerMovement.leftKeyAlt = false;
        }

        if (backBtn.press)
        {
            playerMovement.backKeyAlt = true;
        }
        else
        {
            playerMovement.backKeyAlt = false;
        }

        if (rightBtn.press)
        {
            playerMovement.rightKeyAlt = true;
        }
        else
        {
            playerMovement.rightKeyAlt = false;
        }
    }
}
