using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dancefloor : MonoBehaviour {

    public bool danceActive = false, scene3IsActive;
    public GameObject danceBox, arrow, ar1, ar2, ar3, diffka;
    public GameObject[] scenes;
    public static Dancefloor thing;
    public Text arrowNum, arrowSpeedMn, arrowSpeedMx, basText, perfText, win;
    public Animator anm;

    int position, currentScene, chosenDiff;
    bool chooseNum, chooseSpeedMin, chooseSpeedMax, ifCustom;
    int numberOfArrows, bet;
    float minSpeed, maxSpeed;

    float[,] framePositionStart = { {-97.3f, 3.7f }, { -15.6f, 3.7f }, { 55.6f, 3.7f },
                               { -37.6f, -26.1f },
                               { -183.6f, -85.2f }, { 87.9f, -85.2f }};
    float[,] framePositionCustom = { {94.3f, 2.5f },
                               { -15.3f, -27.6f }, { 79f, -27.6f },
                               { -183.6f, -85.2f }, { 114.7f, -85.2f }};
    float[,] framePositionFin = {{ -183.6f, -85.2f }, { 70.4f, -85.2f }};
    float[] diff = { -80f, 5f, 80f };

    // Use this for initialization
    void Start () {
        thing = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (danceActive)
        {
            switch (currentScene)
            {
                case 0:
                    Scene1();
                    break;

                case 1:
                    Scene2();
                    break;

                case 2:
                    Scene4();
                    break;

                case 3:
                    Scene3();
                    break;
            }
        }
    }

    public void DoTheThing()
    {
        danceBox.SetActive(true);
        danceActive = true;
        DialogController.theDC.dActive = true;
        position = 0;
        arrow.transform.localPosition = new Vector2(framePositionStart[0, 0], framePositionStart[0, 1]);
        currentScene = 0;
        scenes[0].SetActive(true);
        numberOfArrows = 4;
        arrowNum.text = "" + numberOfArrows;
        minSpeed = 0.01f;
        arrowSpeedMn.text = "" + minSpeed;
        maxSpeed = 0.01f;
        arrowSpeedMx.text = "" + maxSpeed;
        chosenDiff = 0;
        diffka.transform.localPosition = new Vector2(diff[chosenDiff], 12.4f);
    }

    void Scene1()
    {
        if (Input.GetKeyDown(ControlControl.z))
        {
            switch (position)
            {
                case 0:
                    chosenDiff = 0;
                    diffka.transform.localPosition = new Vector2(diff[chosenDiff], 12.4f);
                    break;

                case 1:
                    chosenDiff = 1;
                    diffka.transform.localPosition = new Vector2(diff[chosenDiff], 12.4f);
                    break;

                case 2:
                    chosenDiff = 2;
                    diffka.transform.localPosition = new Vector2(diff[chosenDiff], 12.4f);
                    break;

                case 3:
                    scenes[0].SetActive(false);
                    currentScene = 1;
                    scenes[1].SetActive(true);
                    position = 0;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                    break;

                case 4:
                    DialogController.theDC.dActive = false;
                    danceBox.SetActive(false);
                    danceActive = false;
                    InfoControl.info.youCanOpenI = true;
                    break;

                case 5:
                    if (PStats.pStats.money >= 5)
                    {
                        scenes[0].SetActive(false);
                        currentScene = 3;
                        anm.SetBool("game", true);
                        scene3IsActive = true;
                        arrow.SetActive(false);
                        scenes[3].SetActive(true);
                        bet = 5;
                        if (chosenDiff == 0) scenes[3].GetComponent<GameInter>().GameSetup(0, 8, 0.02f * 100, 0.07f * 100);
                        else if (chosenDiff == 1) scenes[3].GetComponent<GameInter>().GameSetup(0, 15, 0.04f * 100, 0.08f * 100);
                        else if (chosenDiff == 2) scenes[3].GetComponent<GameInter>().GameSetup(0, 22, 0.06f * 100, 0.1f * 100);
                    }
                    break;
            }
        }

        if (Input.GetKeyUp(ControlControl.right))
        {
            if (position < 5)
            {
                position++;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
        }
        else if (Input.GetKeyUp(ControlControl.left))
        {
            if (position > 0)
            {
                position--;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
        }
        else if (Input.GetKeyUp(ControlControl.up))
        {
            if (position == 4 || position == 5)
            {
                position = 3;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
            else if (position == 3)
            {
                position = 0;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
        }
        else if (Input.GetKeyUp(ControlControl.down))
        {
            if (position >= 0 && position <= 2)
            {
                position = 3;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
            else if (position == 3)
            {
                position = 4;
                arrow.transform.localPosition = new Vector2(framePositionStart[position, 0], framePositionStart[position, 1]);
            }
        }
    }

    void Scene2()
    {
        if (Input.GetKeyDown(ControlControl.z))
        {
            if (chooseNum)
            {
                ar1.SetActive(false);
                chooseNum = false;
                arrow.SetActive(true);
            }
            else if (chooseSpeedMin)
            {
                ar2.SetActive(false);
                chooseSpeedMin = false;
                arrow.SetActive(true);
            }
            else if (chooseSpeedMax)
            {
                ar3.SetActive(false);
                chooseSpeedMax = false;
                arrow.SetActive(true);
            }
            else
            {
                switch (position)
                {
                    case 0:
                        ar1.SetActive(true);
                        chooseNum = true;
                        arrow.SetActive(false);
                        break;

                    case 1:
                        ar2.SetActive(true);
                        chooseSpeedMin = true;
                        arrow.SetActive(false);
                        break;

                    case 2:
                        ar3.SetActive(true);
                        chooseSpeedMax = true;
                        arrow.SetActive(false);
                        break;

                    case 3:
                        scenes[1].SetActive(false);
                        currentScene = 0;
                        scenes[0].SetActive(true);
                        position = 0;
                        arrow.transform.localPosition = new Vector2(framePositionStart[0, 0], framePositionStart[0, 1]);
                        break;

                    case 4:
                        scenes[1].SetActive(false);
                        currentScene = 3;
                        ifCustom = true;
                        anm.SetBool("game", true);
                        scene3IsActive = true;
                        arrow.SetActive(false);
                        bet = 0;
                        scenes[3].SetActive(true);
                        scenes[3].GetComponent<GameInter>().GameSetup(0, numberOfArrows, minSpeed * 100f, maxSpeed * 100f);
                        break;
                }
            }
        }

        if (Input.GetKeyUp(ControlControl.right))
        {
            if (!chooseNum && !chooseSpeedMin && !chooseSpeedMax)
            {
                if (position < 4)
                {
                    position++;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
            }
        }
        else if (Input.GetKeyUp(ControlControl.left))
        {
            if (!chooseNum && !chooseSpeedMin && !chooseSpeedMax)
            {
                if (position > 0)
                {
                    position--;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
            }
        }
        else if (Input.GetKeyUp(ControlControl.up))
        {
            if (chooseNum)
            {
                if (numberOfArrows == 50) numberOfArrows = 4;
                else numberOfArrows++;
                arrowNum.text = "" + numberOfArrows;
            }
            else if (chooseSpeedMin)
            {
                if (minSpeed >= 0.7f) minSpeed = 0.01f; //здесь менять скорость
                else minSpeed += 0.01f;
                if (maxSpeed < minSpeed)
                {
                    maxSpeed = minSpeed;
                    arrowSpeedMx.text = "" + maxSpeed;
                }
                arrowSpeedMn.text = "" + minSpeed;
            }
            else if (chooseSpeedMax)
            {
                if (maxSpeed >= 0.7f) maxSpeed = minSpeed; //здесь менять скорость
                else maxSpeed += 0.01f;
                arrowSpeedMx.text = "" + maxSpeed;
            }
            else
            {
                if (position == 3 || position == 4)
                {
                    position = 1;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
                else if (position == 1 || position == 2)
                {
                    position = 0;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
            }
        }
        else if (Input.GetKeyUp(ControlControl.down))
        {
            if (chooseNum)
            {
                if (numberOfArrows == 4) numberOfArrows = 50;
                else numberOfArrows--;
                arrowNum.text = "" + numberOfArrows;
            }
            else if (chooseSpeedMin)
            {
                if (minSpeed < 0.02f) minSpeed = 0.7f; //здесь менять скорость
                else minSpeed -= 0.01f;
                if (maxSpeed < minSpeed)
                {
                    maxSpeed = minSpeed;
                    arrowSpeedMx.text = "" + maxSpeed;
                }
                arrowSpeedMn.text = "" + minSpeed;
            }
            else if (chooseSpeedMax)
            {
                if (maxSpeed <= minSpeed) maxSpeed = 0.7f; //здесь менять скорость
                else maxSpeed -= 0.01f;
                arrowSpeedMx.text = "" + maxSpeed;
            }
            else
            {
                if (position == 0)
                {
                    position = 1;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
                else if (position == 1 || position == 2)
                {
                    position = 3;
                    arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
                }
            }
        }
    }

    void Scene3()
    {
        if (ifCustom)
        {
            if (Input.GetKeyDown(ControlControl.x))
            {
                if (GameObject.Find("UpA(Clone)") != null) Destroy(GameObject.Find("UpA(Clone)"));
                if (GameObject.Find("DownA(Clone)") != null) Destroy(GameObject.Find("DownA(Clone)"));
                if (GameObject.Find("LeftA(Clone)") != null) Destroy(GameObject.Find("LeftA(Clone)"));
                if (GameObject.Find("RightA(Clone)") != null) Destroy(GameObject.Find("RightA(Clone)"));
                scenes[3].SetActive(false);
                currentScene = 1;
                ifCustom = false;
                anm.SetBool("game", false);
                scene3IsActive = false;
                arrow.SetActive(true);
                scenes[1].SetActive(true);
                position = 0;
                arrow.transform.localPosition = new Vector2(framePositionCustom[position, 0], framePositionCustom[position, 1]);
            }
        }
    }

    void Scene4()
    {
        if (Input.GetKeyDown(ControlControl.z))
        {
            switch (position)
            {
                case 0:
                    scenes[2].SetActive(false);
                    DialogController.theDC.dActive = false;
                    danceBox.SetActive(false);
                    danceActive = false;
                    InfoControl.info.youCanOpenI = true;
                    break;

                case 1:
                    scenes[2].SetActive(false);
                    currentScene = 0;
                    scenes[0].SetActive(true);
                    position = 0;
                    arrow.transform.localPosition = new Vector2(framePositionStart[0, 0], framePositionStart[0, 1]);
                    break;
            }
        }

        if (Input.GetKeyUp(ControlControl.right))
        {
            if (position == 0)
            {
                position++;
                arrow.transform.localPosition = new Vector2(framePositionFin[position, 0], framePositionFin[position, 1]);
            }
        }
        else if (Input.GetKeyUp(ControlControl.left))
        {
            if (position == 1)
            {
                position--;
                arrow.transform.localPosition = new Vector2(framePositionFin[position, 0], framePositionFin[position, 1]);
            }
        }
    }

    public void StartItog(int res, int bas, int perf)
    {
        scenes[3].SetActive(false);
        currentScene = 2;
        anm.SetBool("game", false);
        scene3IsActive = false;
        arrow.SetActive(true);
        scenes[2].SetActive(true);
        position = 0;
        if (res == 0) bet = -bet;
        else if (res == 1)
        {
            if (chosenDiff == 0) bet *= 2;
            else if (chosenDiff == 1) bet *= 3;
            else if (chosenDiff == 2) bet *= 4;
        }
        else if (res == 2)
        {
            if (chosenDiff == 0) bet *= 2;
            else if (chosenDiff == 1) bet *= 3;
            else if (chosenDiff == 2) bet *= 4;

            bet *= 2;
        }
        PStats.pStats.AddMoney(bet);
        win.text = "" + bet;
        basText.text = "" + bas;
        perfText.text = "" + perf;
        arrow.transform.localPosition = new Vector2(framePositionFin[position, 0], framePositionFin[position, 1]);
    }
}
