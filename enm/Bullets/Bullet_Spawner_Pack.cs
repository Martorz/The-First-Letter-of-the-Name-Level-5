using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet_Spawner_Pack : MonoBehaviour {

    public float time = 3, a = 0, time2, time3, forEyeAt3 = 0, time4, time5, time6; 
    public GameObject[] bullets, spawners;
    public GameObject act;
    public bool start, forOtherNeeds, createWarAt2 = true, createWarAt3 = true;
    public static Bullet_Spawner_Pack bsp;
    public int spawnChance = 5, chooseAttack = 5;

    float timeToStop = 0, timeToStop2 = 0, timeToStop3 = 0, timeToStop4 = 0, timeToStop5 = 0, timeToStop6 = 0;
    int countGhosts, currentLetter = 10;

    // Use this for initialization
    void Start () {
        bsp = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            switch (chooseAttack)
            {
                case 0:
                    WarAt1();
                    break;

                case 1:
                    WarAt2();
                    break;

                case 2:
                    WarAt3();
                    break;

                case 3:
                    EyeAt1();
                    break;

                case 5:
                    EyeAt3();
                    break;

                case 7:
                    GhsAt2();
                    break;

                case 8:
                    GhsAt3();
                    break;

                default:
                    break;
            }
        }
    }

    public void Restart()
    {
        timeToStop = 0;
        timeToStop2 = 0;
        timeToStop3 = 0;
        timeToStop4 = 0;
        timeToStop5 = 0;
        timeToStop6 = 0;
        a = 0;
        countGhosts = 0;
        currentLetter = 10;
    }

    void WarAt1()
    {
        if (timeToStop <= 0f)
        {
            timeToStop = time;
            if (Random.Range(0, spawnChance) != 0)
            {
                Vector2 whereToSpawn = new Vector2(spawners[0].transform.position.x, spawners[0].transform.position.y), whereToSpawn2 = new Vector2(spawners[1].transform.position.x, spawners[1].transform.position.y);
                SceneManager.MoveGameObjectToScene(Instantiate(bullets[0], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
                SceneManager.MoveGameObjectToScene(Instantiate(bullets[1], whereToSpawn2, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
            }
        }
        else timeToStop -= Time.unscaledDeltaTime;
        a += 0.01f;
    } //копья

    void WarAt2()
    {
        if (createWarAt2)
        {
            createWarAt2 = false;
            Vector2 whereToSpawn = new Vector2(-15.15f, -5.5f);
            SceneManager.MoveGameObjectToScene(Instantiate(bullets[2], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
        }
    } //гарпун

    void WarAt3()
    {
        if (timeToStop2 <= 0f)
        {
            timeToStop2 = time2;
            createWarAt3 = false;
            Vector2 whereToSpawn = act.transform.position;
            SceneManager.MoveGameObjectToScene(Instantiate(bullets[6], whereToSpawn, Quaternion.Euler(0, 0, Random.Range(0, 361))), SceneManager.GetSceneByName("Battle"));
        }
        else timeToStop2 -= Time.unscaledDeltaTime;
    } //ножи

    void EyeAt1()
    {
        if (GameObject.Find("bullets_21(Clone)") == null)
        {
            for (int i = 0; i < 8; i++)
            {
                Vector2 whereToSpawn = new Vector2(-15.15f, -5.5f);
                SceneManager.MoveGameObjectToScene(Instantiate(bullets[3], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
            }
        }
    } //реснички

    public void EyeAt2()
    {
        Vector2 whereToSpawn = new Vector2(-5.83f, act.transform.position.y);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[7], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
    } //тарелка

    void EyeAt3()
    {
        if (timeToStop3 <= 0f)
        {
            timeToStop3 = time3; //time3
            Vector2 whereToSpawn = new Vector2(forEyeAt3, 1.5f * Mathf.Sin(a) + 3.43f);
            SceneManager.MoveGameObjectToScene(Instantiate(bullets[8], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
        }
        else timeToStop3 -= Time.unscaledDeltaTime;

        a += 0.01f;
        forEyeAt3 = Mathf.Sin(a * 0.5f * Mathf.PI) - 0.79f;
    } //спираль

    public void GhsAt1()
    {
        Vector2 whereToSpawn = new Vector2(-0.79f, 1.74f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[4], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
    } //тень

    void GhsAt2()
    {
        if (timeToStop4 <= 0f)
        {
            timeToStop4 = time4;
            Vector2 whereToSpawn = new Vector2(-3.1f, 1.74f);
            SceneManager.MoveGameObjectToScene(Instantiate(bullets[9], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
            countGhosts++;
        }
        else timeToStop4 -= Time.unscaledDeltaTime;
    } //духи

    void GhsAt3()
    {
        if (timeToStop6 <= 0f)
        {
            timeToStop6 = time6 / BattleInter.Bi.mSpeed;
            Vector2 whereToSpawn = new Vector2(Random.Range(-4.31f, 2.68f), Random.Range(-1.85f, 5.02f));
            SceneManager.MoveGameObjectToScene(Instantiate(bullets[12], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
        }
        else timeToStop6 -= Time.unscaledDeltaTime;
    } //волны

    public void GolAt1()
    {
        Vector2 whereToSpawn = new Vector2(-0.79f, 1.3975f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[5], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
    } //голем

    public void GolAt2()
    {
        Vector2 whereToSpawn = new Vector2(-0.79f, 4.18f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[10], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
    } //фигурки

    public void GolAt3()
    {
        Vector2 whereToSpawn = new Vector2(4.47f, -2.27f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[11], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
    } //буквы

    public void LAt1()
    {
        Vector2 whereToSpawn = new Vector2(-3.92f, 1.72f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[0], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[1], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
    }

    public void LAt2()
    {
        Vector2 whereToSpawn = new Vector2(-3.92f, 1.72f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[2], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[3], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
    }

    public void LAt3()
    {
        Vector2 whereToSpawn = new Vector2(-3.92f, 1.72f);
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[4], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
        SceneManager.MoveGameObjectToScene(Instantiate(bullets[5], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("BossBattle"));
    }
}
