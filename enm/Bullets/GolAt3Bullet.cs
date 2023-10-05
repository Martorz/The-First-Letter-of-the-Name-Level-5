using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolAt3Bullet : MonoBehaviour {

    public float speed;
    public Rigidbody2D rig;
    public bool isFirst, upOrDown; //true - up, false - down

    Collider2D pol, anotherPol;
    bool once;
    string yourNextName;

    // Use this for initialization
    void Start () {
        gameObject.transform.localScale = new Vector2(Random.Range(0.39f, 0.84f), Random.Range(0.685f, 1.31f));
        rig = GetComponent<Rigidbody2D>();
        pol = GetComponent<PolygonCollider2D>();
        if (isFirst) rig.velocity = new Vector2(-1, 0) * speed * BattleInter.Bi.mSpeed;
        if (!upOrDown)
        {
            switch (gameObject.name)
            {
                case "bullets_9(Clone)":
                    yourNextName = "bullets_10(Clone)";
                    break;

                case "bullets_10(Clone)":
                    yourNextName = "bullets_11(Clone)";
                    break;

                case "bullets_11(Clone)":
                    yourNextName = "bullets_12(Clone)";
                    break;

                case "bullets_12(Clone)":
                    yourNextName = "bullets_13(Clone)";
                    break;

                case "bullets_13(Clone)":
                    yourNextName = "bullets_9(Clone)";
                    break;
            }
        }
        else
        {
            switch (gameObject.name)
            {
                case "bullets_16(Clone)":
                    yourNextName = "bullets_15(Clone)";
                    break;

                case "bullets_17(Clone)":
                    yourNextName = "bullets_16(Clone)";
                    break;

                case "bullets_18(Clone)":
                    yourNextName = "bullets_17(Clone)";
                    break;

                case "bullets_14(Clone)":
                    yourNextName = "bullets_18(Clone)";
                    break;

                case "bullets_15(Clone)":
                    yourNextName = "bullets_14(Clone)";
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (transform.position.x <= -6.05f) Destroy(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "arrow" && !isFirst) collision.gameObject.GetComponent<GolAt3Bullet>().rig.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.gameObject.name == yourNextName)
            {
                collision.gameObject.GetComponent<GolAt3Bullet>().rig.velocity = new Vector2(-1, 0) * speed * BattleInter.Bi.mSpeed;
                if (!upOrDown) FindObjectOfType<Gol_At_3>().meh = true;
                else FindObjectOfType<Gol_At_3>().wweh = true;
        }
    }
}
