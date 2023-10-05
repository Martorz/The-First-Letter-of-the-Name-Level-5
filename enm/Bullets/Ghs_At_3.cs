using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghs_At_3 : MonoBehaviour {

    Animator heh;

	// Use this for initialization
	void Start () {
        heh = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (heh.GetCurrentAnimatorStateInfo(0).IsName("wave_idle")) Destroy(gameObject);
        }
        else Destroy(gameObject);
    }
}
