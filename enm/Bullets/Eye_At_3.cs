using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_At_3 : MonoBehaviour {

    public float meh, wweh, waiting = 0.3f;
    private float a = 0, waitingTime = 0;

    bool isIn;

    // Update is called once per frame
    void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (a < 24)
            {
                a += meh * BattleInter.Bi.mSpeed;
                transform.position = new Vector2(a * Mathf.Cos(a) / 4 + Bullet_Spawner_Pack.bsp.forEyeAt3, a * Mathf.Sin(a) / 4 + 1.5f * Mathf.Sin(Bullet_Spawner_Pack.bsp.a) + 3.43f);
            }
            else Destroy(gameObject);

            if (isIn)
            {
                if (waitingTime <= 0)
                {
                    PStats.pStats.HurtPlayer(3);
                    waitingTime = waiting;
                }
                else waitingTime -= Time.deltaTime;
            }
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act" && !other.gameObject.GetComponent<BulletHell>().invis)
        {
            isIn = true;
            waitingTime = 0;
        }

        if (other.gameObject.name == "hat") Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act") isIn = false;
    }
}
