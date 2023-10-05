using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour {

    string enemyName;
    public Text displayName;
    public bool namae, health, lvl, effect, ifEnemy = false;
    public GameObject BatInt;
    int enID, maxH;

	// Use this for initialization
	void Start () {
        
        if (ifEnemy)
        {
            enID = BatInt.GetComponent<BattleInter>().justEnemyID;
            if (namae)
            {
                displayName.text = "" + BatInt.GetComponent<BattleInter>().enemies[enID, 0];
            }
        }
        else
        {
            if (health) maxH = PStats.pStats.pMaxHealth;
            
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ifEnemy && health && maxH == 0) maxH = System.Convert.ToInt32(BatInt.GetComponent<BattleInter>().maxHealth);
        if (ifEnemy)
        {
            if (health) displayName.text = "" + maxH + "/" + System.Convert.ToInt32(BatInt.GetComponent<BattleInter>().health);
            else if (lvl) displayName.text = "Уровень: " + BatInt.GetComponent<BattleInter>().lvlEnemy;
            else if (effect)
            {
                if (BatInt.GetComponent<BattleInter>().curEffect == "нет") displayName.text = "";
                else displayName.text = "" + BatInt.GetComponent<BattleInter>().curEffect;
            }
        }
        else
        {
            if (health)
            {
                if (AllAttacks.allAttacks.pAddon > 0) displayName.text = "" + maxH + "/" + Mathf.Ceil(PStats.pStats.pCurrentHealth) + " (" + Mathf.Ceil(AllAttacks.allAttacks.pAddon) + ")";
                else displayName.text = "" + maxH + "/" + Mathf.Ceil(PStats.pStats.pCurrentHealth);
            }
            else if (lvl) displayName.text = "Уровень: " + PStats.pStats.currentLvl;
            else if (effect)
            {
                if (PStats.pStats.curEffect == "нет") displayName.text = "";
                else displayName.text = "" + PStats.pStats.curEffect;
            }
        }

    }
}
