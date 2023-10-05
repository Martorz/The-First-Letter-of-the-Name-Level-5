using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForGhsAt1 : MonoBehaviour {

    public bool check = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act" && (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start))
        {
            BattleInter.Bi.DefaultLogUpdate("Вы слишком сильно погрузились во тьму и застыли от страха.");
            BattleInter.Bi.freezePlayer = true;
            Destroy(gameObject);
        }
    }

    
}
