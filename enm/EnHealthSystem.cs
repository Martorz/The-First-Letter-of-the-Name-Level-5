using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnHealthSystem : MonoBehaviour {
    
    void Update()
    {
        if (PControl.leavingBattle == true)
        {
            PControl.leavingBattle = false;
            BattleInter.StartBattle = true;
            enabled = false;
        }
    }
}
