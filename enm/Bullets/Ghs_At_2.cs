using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghs_At_2 : MonoBehaviour {

    private Vector3 point;
    public float speed, chasingSpeed;
    bool check;
    private Transform target;

    // Use this for initialization
    void Start () {
        point = new Vector3(Random.Range(-4.2f, 2.69f), Random.Range(-1.79f, 5.2f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chasingSpeed * BattleInter.Bi.mSpeed);
        }
        else
        {
            if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, speed * BattleInter.Bi.mSpeed);
                float temp = Vector3.Distance(point, gameObject.transform.position);
                if (temp <= 0.001 && temp >= -0.001) point = new Vector3(Random.Range(-4.2f, 2.69f), Random.Range(-1.79f, 5.2f), 0);
            }
        else Destroy(gameObject);
    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act")
        {
            check = true;
            target = other.gameObject.GetComponent<Transform>();
            point = new Vector3(Random.Range(-4.2f, 2.69f), Random.Range(-1.79f, 5.2f), 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act")
        {
            check = false;
        }
    }
}
