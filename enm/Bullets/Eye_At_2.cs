using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_At_2 : MonoBehaviour {

    Rigidbody2D rig;
    GameObject act;
    float dir = 1, timeToWait, timeLeft, speed, waiting = 0.3f;
    private float waitingTime = 0;
    bool isIn, action = true, isHat;

    // Use this for initialization
    void Start () {
        act = GameObject.Find("bi_act");
        rig = GetComponent<Rigidbody2D>();
        timeToWait = Random.Range(0f, 2f);
        timeLeft = timeToWait;
        speed = Random.Range(20f, 30f) * BattleInter.Bi.mSpeed;
        rig.velocity = new Vector2(dir * speed, 0);
	}

    // Update is called once per frame
    void Update() {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (action)
            {
                if ((dir == 1 && transform.position.x >= 4.37f) || (dir == -1 && transform.position.x <= -5.78f))
                {
                    action = false;
                    rig.velocity = Vector2.zero;
                }
            }
            else
            {
                if (timeLeft <= 0)
                {
                    dir = -dir;
                    speed = Random.Range(20f, 30f) * BattleInter.Bi.mSpeed;
                    timeToWait = Random.Range(0f, 2f);
                    timeLeft = timeToWait;
                    transform.position = new Vector2(transform.position.x, act.transform.position.y);
                    action = true;
                    rig.velocity = new Vector2(dir * speed, 0);
                }
                else timeLeft -= Time.deltaTime;
            }

            if (isIn && !isHat)
            {
                if (waitingTime <= 0)
                {
                    PStats.pStats.HurtPlayer(5);
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
        if (other.gameObject.name == "hat") isHat = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act") isIn = false;
        if (other.gameObject.name == "hat") isHat = false;
    }
}
