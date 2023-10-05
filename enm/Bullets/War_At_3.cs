using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_At_3 : MonoBehaviour {

    public float timeToWait, timeToWait2, angle;
    public GameObject a, b;
    
    // Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (timeToWait > 0) timeToWait -= Time.deltaTime;
            else
            {
                if (b.transform.localRotation.z < 0.563)
                {
                    a.transform.Rotate(0, 0, -angle);
                    b.transform.Rotate(0, 0, angle);
                }
                else
                {
                    if (timeToWait2 > 0) timeToWait2 -= Time.deltaTime;
                    else Destroy(gameObject);
                }
            }
        }
        else Destroy(gameObject);
    }

    
}
