using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTextControl : MonoBehaviour {

    public Text[] display, PPs;
    int[] index = new int[4];

    public static AttackTextControl atc;

    // Use this for initialization
    void Start() {
        atc = this;

        for (int i = 0; i < 4; i++)
        {
            index[i] = PStats.pStats.attackIndex[i];
            display[i].text = "" + PStats.pStats.attacks[index[i], 0];
        }
        PPs[0].text = "" + PStats.pStats.attacks[index[1], 3] + "/" + PStats.pStats.ppTwo;
        PPs[1].text = "" + PStats.pStats.attacks[index[2], 3] + "/" + PStats.pStats.ppThree;
        PPs[2].text = "" + PStats.pStats.attacks[index[3], 3] + "/" + PStats.pStats.ppFour;
    }

    public void UpdatePP(int which)
    {
        switch (which)
        {
            case 1:
                PPs[0].text = "" + PStats.pStats.attacks[index[1], 3] + "/" + PStats.pStats.ppTwo;
                break;

            case 2:
                PPs[1].text = "" + PStats.pStats.attacks[index[2], 3] + "/" + PStats.pStats.ppThree;
                break;

            case 3:
                PPs[2].text = "" + PStats.pStats.attacks[index[3], 3] + "/" + PStats.pStats.ppFour;
                break;

            case 4:
                PPs[0].text = "" + PStats.pStats.attacks[index[1], 3] + "/" + PStats.pStats.ppTwo;
                PPs[1].text = "" + PStats.pStats.attacks[index[2], 3] + "/" + PStats.pStats.ppThree;
                PPs[2].text = "" + PStats.pStats.attacks[index[3], 3] + "/" + PStats.pStats.ppFour;
                break;
        }
    }

    public void UpdateAttack(int which)
    {
        switch (which)
        {
            case 0:
                index[0] = PStats.pStats.attackIndex[0];
                display[0].text = "" + PStats.pStats.attacks[index[which], 0];
                break;

            case 1:
                display[1].text = "" + PStats.pStats.attacks[index[which], 0];
                break;

            case 2:
                display[2].text = "" + PStats.pStats.attacks[index[which], 0];
                break;

            case 3:
                display[3].text = "" + PStats.pStats.attacks[index[which], 0];
                break;
        }
    }
}
