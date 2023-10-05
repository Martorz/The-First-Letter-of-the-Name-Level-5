using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_At_1_2 : MonoBehaviour {

    public float speed = 0.03f, waiting = 0.3f;
    private float waitingTime = 0;
    bool isIn;

    void Update()
    {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            gameObject.transform.position = new Vector2(Mathf.Sin(-Bullet_Spawner_Pack.bsp.a * 0.5f * Mathf.PI) + GameObject.Find("War_At_1 (1)").transform.position.x, gameObject.transform.position.y - speed * BattleInter.Bi.mSpeed);
            if (gameObject.transform.position.y <= -2.93f) Destroy(gameObject);
            if (isIn)
            {
                if (waitingTime <= 0)
                {
                    PStats.pStats.HurtPlayer(2);
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
