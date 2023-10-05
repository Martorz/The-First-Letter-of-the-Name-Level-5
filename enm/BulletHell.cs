using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour {

    public bool invis, hat;
    public GameObject hatt;

    private Rigidbody2D rigidbodyA;
    float moveSpeed = 4;
    bool starting = true;

    private float timeToStop;
    Animator anm;

    // Use this for initialization
    void Start () {
        rigidbodyA = GetComponent<Rigidbody2D>();
        if (BattleInter.Bi.justEnemyID != 4) anm = BattleInter.Bi.enemiesPics[BattleInter.Bi.justEnemyID].GetComponent<Animator>();
        else anm = BattleInter.Bi.enemiesPics[0].GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (starting)
        {
                if (BattleInter.Bi.invis)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                    BattleInter.Bi.invis = false;
                    invis = true;
                }
                else if (BattleInter.Bi.hat)
                {
                    BattleInter.Bi.hat = false;
                    hatt.SetActive(true);
                    hat = true;
                }
                timeToStop = 20;

                switch (BattleInter.Bi.justEnemyID)
                {
                    case 0:
                        Bullet_Spawner_Pack.bsp.chooseAttack = Random.Range(3, 6);
                        if (Bullet_Spawner_Pack.bsp.chooseAttack == 3) anm.SetTrigger("attack2");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 4) anm.SetTrigger("attack1");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 5) anm.SetTrigger("attack3");
                    break;

                    case 1:
                        Bullet_Spawner_Pack.bsp.chooseAttack = Random.Range(6, 9);
                        if (Bullet_Spawner_Pack.bsp.chooseAttack == 6) anm.SetTrigger("attack1");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 7) anm.SetTrigger("attack3");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 8) anm.SetTrigger("attack2");
                    break;

                    case 2:
                        Bullet_Spawner_Pack.bsp.chooseAttack = Random.Range(0, 3);
                        if (Bullet_Spawner_Pack.bsp.chooseAttack == 0) anm.SetTrigger("attack2");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 1) anm.SetTrigger("attack3");
                        else if (Bullet_Spawner_Pack.bsp.chooseAttack == 2) anm.SetTrigger("attack1");
                    break;

                    case 3:
                        Bullet_Spawner_Pack.bsp.chooseAttack = Random.Range(9, 12);
                        anm.SetTrigger("attack");
                        break;

                    case 4:
                        Bullet_Spawner_Pack.bsp.chooseAttack = Random.Range(12, 15);
                        if (Random.Range(0, 2) == 0) anm.SetTrigger("attack1");
                        else anm.SetTrigger("attack2");
                    break;
                }
            
            starting = false;
                switch (Bullet_Spawner_Pack.bsp.chooseAttack)
                {
                    case 1:
                        Bullet_Spawner_Pack.bsp.spawners[2].SetActive(true);
                        break;

                    case 4:
                        Bullet_Spawner_Pack.bsp.EyeAt2();
                        break;

                    case 5:
                        timeToStop = 30;
                        break;

                    case 6:
                        Bullet_Spawner_Pack.bsp.GhsAt1();
                        break;

                    case 9:
                        Bullet_Spawner_Pack.bsp.GolAt1();
                        break;

                    case 10:
                        Bullet_Spawner_Pack.bsp.GolAt2();
                        break;

                    case 11:
                        Bullet_Spawner_Pack.bsp.GolAt3();
                        break;

                    case 12:
                        Bullet_Spawner_Pack.bsp.LAt1();
                        break;

                    case 13:
                        Bullet_Spawner_Pack.bsp.LAt2();
                        break;

                    case 14:
                        Bullet_Spawner_Pack.bsp.LAt3();
                        break;

                case 99:
                            timeToStop = 1;
                            break;
                }

                Bullet_Spawner_Pack.bsp.start = true;
            
        }

        if (!starting)
        {
            if (BattleInter.bulletHell)
            {
                if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
                {
                    rigidbodyA.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidbodyA.velocity.y);
                }

                if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
                {
                    rigidbodyA.velocity = new Vector2(rigidbodyA.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                }

                if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
                {
                    rigidbodyA.velocity = new Vector2(0f, rigidbodyA.velocity.y);
                }

                if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
                {
                    rigidbodyA.velocity = new Vector2(rigidbodyA.velocity.x, 0f);
                }
            }

            timeToStop -= Time.deltaTime;
            if (timeToStop < 0f || PStats.pStats.pCurrentHealth <= 0)
            {
                if (invis)
                {
                    invis = false;
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                else if (hat)
                {
                    hat = false;
                    hatt.SetActive(false);
                }

                if (BattleInter.Bi.justEnemyID == 0)
                {
                    if (Bullet_Spawner_Pack.bsp.chooseAttack == 3) anm.SetTrigger("attack2end");
                    else if (Bullet_Spawner_Pack.bsp.chooseAttack == 4) anm.SetTrigger("attack1end");
                }
                else if (BattleInter.Bi.justEnemyID == 1)
                {
                    if (Bullet_Spawner_Pack.bsp.chooseAttack == 8) anm.SetTrigger("attack2end");
                    else if (Bullet_Spawner_Pack.bsp.chooseAttack == 6) anm.SetTrigger("attack1end");
                }
                else if (BattleInter.Bi.justEnemyID == 2)
                {
                    if (Bullet_Spawner_Pack.bsp.chooseAttack == 1) anm.SetTrigger("attack3end");
                }

                Bullet_Spawner_Pack.bsp.start = false;
                BattleInter.bulletHell = false;
                starting = true;
                Bullet_Spawner_Pack.bsp.Restart();
                gameObject.SetActive(false);
            }
        }
	}
}
