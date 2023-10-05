using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghs_At_1 : MonoBehaviour {

    private Vector3 point;
    public float speed, waiting = 0.3f;
    private float waitingTime = 0;
    bool isIn, isHat;

    // Use this for initialization
    void Start () {
        BattleInter.Bi.freezePlayer = false;
        point = new Vector3(Random.Range(-3.518f, 2.105f), Random.Range(-1.214f, 4.89f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * BattleInter.Bi.mSpeed);
            if (Vector3.Distance(point, gameObject.transform.position) <= 0.001 && Vector3.Distance(point, gameObject.transform.position) >= -0.001)
            {
                point = new Vector3(Random.Range(-3.518f, 2.105f), Random.Range(-1.214f, 4.89f), 0);
            }
            if (isIn && !isHat)
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
