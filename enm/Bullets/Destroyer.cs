using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (!(BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)) Destroy(gameObject);
    }
}
