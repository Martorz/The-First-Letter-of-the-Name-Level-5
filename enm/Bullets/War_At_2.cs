using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_At_2 : MonoBehaviour {

    private Rigidbody2D rgb;
    private PolygonCollider2D border;
    private BoxCollider2D box;
    public float moveSpeed, waiting = 0.3f;
    private float waitingTime = 0;
    public int moveCount;
    public Sprite ah;
    bool isIn, isHat;

    // Use this for initialization
    void Start () {
        rgb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        border = GameObject.Find("bi_back").GetComponent<PolygonCollider2D>();
        gameObject.transform.Rotate(0, 0, Random.Range(-180f, 180f));
        Vector3 pos = new Vector3();
        if (gameObject.transform.rotation.z < 0.383f && gameObject.transform.rotation.z >= -0.383f) pos = new Vector3(Random.Range(-3.92f, 2.73f), -2.46f, 0);
        else if (gameObject.transform.rotation.z < 0.924f && gameObject.transform.rotation.z >= 0.383f) pos = new Vector3(3.34f, Random.Range(-1.47f, 5.06f), 0);
        else if ((gameObject.transform.rotation.z <= 1 && gameObject.transform.rotation.z >= 0.924f) || (gameObject.transform.rotation.z >= -1 && gameObject.transform.rotation.z < -0.924f)) pos = new Vector3(Random.Range(-3.89f, 2.39f), 5.72f, 0);
        else if (gameObject.transform.rotation.z < -0.383f && gameObject.transform.rotation.z > -0.924f) pos = new Vector3(-4.76f, Random.Range(-1.82f, 4.7f), 0);
        gameObject.transform.position = pos;
        rgb.velocity = transform.up * moveSpeed * BattleInter.Bi.mSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (moveCount == 2)
            {
                moveCount++;
                rgb.velocity = Vector2.zero;
                gameObject.GetComponent<SpriteRenderer>().sprite = ah;
                Bullet_Spawner_Pack.bsp.createWarAt2 = true;
            }
            if (isIn && !isHat)
            {
                if (waitingTime <= 0)
                {
                    PStats.pStats.HurtPlayer(4);
                    waitingTime = waiting;
                }
                else waitingTime -= Time.deltaTime;
            }
        }
        else
        {
            Bullet_Spawner_Pack.bsp.spawners[2].SetActive(false);
            Bullet_Spawner_Pack.bsp.createWarAt2 = true;
            Destroy(gameObject);
        }
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
