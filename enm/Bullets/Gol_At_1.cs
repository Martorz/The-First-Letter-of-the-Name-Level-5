using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gol_At_1 : MonoBehaviour {

    private int side;
    public float speed, waiting = 0.3f;
    private float waitingTime = 0;
    float rot = 0;
    bool isIn, isHat;

    // Use this for initialization
    void Start()
    {
        side = Random.Range(1, 3);
        if (side == 1) side = 1;
        else side = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            transform.Rotate(0, 0, speed * BattleInter.Bi.mSpeed * side);
            if (isIn && !isHat)
            {
                if (waitingTime <= 0)
                {
                    PStats.pStats.HurtPlayer(8);
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
