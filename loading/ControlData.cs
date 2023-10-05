using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlData {
    
    public bool currentControl, wasCutscene;

    public ControlData(int type, ControlControl cc = null)//, bool wasCutLast = false)
    {
        switch (type)
        {
            case 0:
                currentControl = cc.currentControl;
                //wasCutscene = wasCutLast;
                break;

            case 1:
                currentControl = true;
                //wasCutscene = false;
                break;
        }
    }
}
