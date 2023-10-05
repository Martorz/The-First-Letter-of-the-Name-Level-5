using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInter : MonoBehaviour {
    
    public GameObject up, down, left, right;
    public float counter, minS, maxS;
    public static GameInter inter;
    public int arrowCounter = 16, arrowCurrent, attackNum, result; //0 - loose, 1 - good, 2 - crit
    public bool BIorDF = true; //true - BI, false - DF
    public int basicCounter, perfectCounter;

    private void Start()
    {
        inter = this;
    }

    // Update is called once per frame
    void Update () {
		if (Provirka())
        {
            if (BIorDF)
            {
                if (arrowCurrent != arrowCounter)
                {
                    if (GameObject.Find("Up(Clone)") == null)
                    {
                        up.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("Down(Clone)") == null)
                    {
                        down.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("Left(Clone)") == null)
                    {
                        left.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("Right(Clone)") == null)
                    {
                        right.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }
                }
                else if (GameObject.Find("Up(Clone)") == null && GameObject.Find("Down(Clone)") == null && GameObject.Find("Left(Clone)") == null && GameObject.Find("Right(Clone)") == null)
                {

                    float c = counter / arrowCounter * 100;
                    if (c <= 50) result = 0;
                    else if (c > 50 && c < 100) result = 1;
                    else result = 2;
                    BattleInter.Bi.nameGame = false;
                    BattleInter.Bi.NameAttack();
                    BattleInter.Bi.mainText.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (arrowCurrent < arrowCounter)
                {
                    if (GameObject.Find("UpA(Clone)") == null)
                    {
                        up.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("DownA(Clone)") == null)
                    {
                        down.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("LeftA(Clone)") == null)
                    {
                        left.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }

                    if (GameObject.Find("RightA(Clone)") == null)
                    {
                        right.GetComponent<ArrowSpawner>().Spawn();
                        arrowCurrent++;
                    }
                }
                else if (GameObject.Find("UpA(Clone)") == null && GameObject.Find("DownA(Clone)") == null && GameObject.Find("LeftA(Clone)") == null && GameObject.Find("RightA(Clone)") == null)
                {
                    float c = counter / arrowCounter * 100;
                    if (c <= 50) result = 0;
                    else if (c > 50 && c < 100) result = 1;
                    else result = 2;
                    Dancefloor.thing.StartItog(result, basicCounter, perfectCounter);
                }
            }
        }
	}

    public void GameSetup(int attack, int howMany, float minSpeed, float maxSpeed)
    {
        arrowCurrent = 0;
        counter = 0;
        arrowCounter = howMany;
        minS = minSpeed;
        maxS = maxSpeed;
        attackNum = attack;
        basicCounter = 0; perfectCounter = 0;
    }

    bool Provirka()
    {
        if (GameObject.FindObjectOfType<BattleInter>() != null)
        {
            if (BattleInter.Bi.nameGame) return true;
            else return false;
        }
        else if (GameObject.FindObjectOfType<Dancefloor>() != null)
        {
            if (Dancefloor.thing.scene3IsActive) return true;
            else return false;
        }
        else return false;
    }
}
