using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_At_1 : MonoBehaviour {

    public float timer, waiting = 0.3f;
    bool isIn;
    private float waitingTime = 0;

    // Use this for initialization
    void Start () {
        gameObject.transform.Rotate(0, 0, Random.Range(-180f, 180f));
        Vector3 pos = new Vector3(Random.Range(-4f, 2.42f), Random.Range(-1.55f, 4.89f), 0);
        gameObject.transform.position = pos;
        timer /= BattleInter.Bi.mSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (isIn)
                {
                    if (waitingTime <= 0)
                    {
                        PStats.pStats.HurtPlayer(1);
                        waitingTime = waiting;
                    }
                    else waitingTime -= Time.deltaTime;
                }
            }
            else Destroy(gameObject);
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
