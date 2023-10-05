using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour {

    public GameObject[] chooseFrame, chooseSett, biblocks, chooseContr;
    public GameObject help, mm, sett, control, mmToKill, camera, startInformation;
    public Slider music, sfx;
    public Sprite type1, type2;
    public ControlControl cc;
    int currentScene = 3, currentRow = 0, chooseFrameSett = 0;

    float[] pos = { -0.172f, 1.103f };

    // Update is called once per frame
    void Update() {
        switch (currentScene)
        {
            case 0:
                Scene0();
                break;

            case 1:
                Scene1();
                break;

            case 2:
                Scene2();
                break;

            case 3:
                Scene3();
                break;
        }
    }

    void Scene0()
    {
        if (Input.GetKeyUp(ControlControl.z))
        {
            switch (currentRow)
            {
                case 0:
                    if (!ControlControl.cc.CheckIfNewGame())
                    {
                        PStats.pStats.SetBasicStats();
                        ShopControl.thing.RestartShop();
                        camera.GetComponent<Camera>().orthographicSize = 7;
                        PControl.pControl.whenMMisClosed = true;
                        LoadNewArea.loadNA.Starting();
                        Destroy(mmToKill);
                    }
                    break;

                case 1:
                    /*SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                    SceneManager.MoveGameObjectToScene(control, SceneManager.GetSceneByBuildIndex(1));
                    SceneManager.MoveGameObjectToScene(GameObject.Find("Letter Box Camera"), SceneManager.GetSceneByBuildIndex(1));
                    StartCoroutine(Unload());*/
                    Saving.thing.NewGame();
                    PStats.pStats.SetBasicStats();
                    ShopControl.thing.RestartShop();
                    camera.GetComponent<Camera>().orthographicSize = 7;
                    PControl.pControl.whenMMisClosed = true;
                    LoadNewArea.loadNA.Starting();
                    Destroy(mmToKill);
                    break;

                case 2:
                    mm.SetActive(false);
                    sett.SetActive(true);
                    chooseFrame[2].SetActive(false);
                    currentScene = 2;
                    currentRow = 0;
                    if (cc.currentControl != true)
                    {
                        biblocks[0].SetActive(false);
                        biblocks[1].SetActive(true);
                    }
                    else
                    {
                        biblocks[0].SetActive(true);
                        biblocks[1].SetActive(false);
                    }
                    music.interactable = true;
                    music.Select();
                    break;

                case 3:
                    mm.SetActive(false);
                    help.SetActive(true);
                    currentScene = 1;
                    if (cc.currentControl == true)
                    {
                        chooseContr[0].SetActive(true);
                        chooseContr[1].SetActive(false);
                    }
                    else
                    {
                        chooseContr[1].SetActive(true);
                        chooseContr[0].SetActive(false);
                    }
                    break;

                case 4:
                    //UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                    break;
            }
        }
        
        if (Input.GetKeyUp(ControlControl.up))
        {
            if (currentRow > 0)
            {
                chooseFrame[currentRow].SetActive(false);
                currentRow--;
                chooseFrame[currentRow].SetActive(true);
            }
        }
        else if (Input.GetKeyUp(ControlControl.down))
        {
            if (currentRow < 4)
            {
                chooseFrame[currentRow].SetActive(false);
                currentRow++;
                chooseFrame[currentRow].SetActive(true);
            }
        }
    }

    void Scene1()
    {
        if (Input.GetKeyUp(ControlControl.z) || Input.GetKeyUp(ControlControl.x))
        {
            mm.SetActive(true);
            help.SetActive(false);
            currentScene = 0;
        }
    }

    void Scene2()
    {
        if (Input.GetKeyUp(ControlControl.z))
        {
            if (currentRow == 2)
            {
                switch (chooseFrameSett)
                {
                    case 0:
                        biblocks[0].SetActive(true);
                        biblocks[1].SetActive(false);
                        ControlControl.cc.SetControl(0);
                        break;

                    case 1:
                        biblocks[1].SetActive(true);
                        biblocks[0].SetActive(false);
                        ControlControl.cc.SetControl(1);
                        break;
                }
            }
        }

        if (Input.GetKeyUp(ControlControl.up))
        {
            if (currentRow == 1)
            {
                currentRow--;
                sfx.interactable = false;
                music.interactable = true;
                music.Select();
            }
            else if (currentRow == 2)
            {
                currentRow--;
                chooseSett[0].SetActive(false);
                sfx.interactable = true;
                sfx.Select();
            }
            else if (currentRow == 3)
            {
                currentRow--;
                chooseSett[1].SetActive(false);
                chooseSett[0].SetActive(true);
            }
        }
        else if (Input.GetKeyUp(ControlControl.down))
        {
            if (currentRow == 0)
            {
                currentRow++;
                music.interactable = false;
                sfx.interactable = true;
                sfx.Select();
            }
            else if (currentRow == 1)
            {
                currentRow++;
                sfx.interactable = false;
                chooseSett[0].SetActive(true);
            }
            else if (currentRow == 2)
            {
                currentRow++;
                chooseSett[0].SetActive(false);
                chooseSett[1].SetActive(true);
            }
        }
        else if (Input.GetKeyUp(ControlControl.left))
        {
            if (chooseFrameSett == 1)
            {
                chooseSett[0].transform.localPosition = new Vector2(pos[0], -4.677f);
                chooseFrameSett--;
            }
        }
        else if (Input.GetKeyUp(ControlControl.right))
        {
            if (chooseFrameSett == 0)
            {
                chooseSett[0].transform.localPosition = new Vector2(pos[1], -4.677f);
                chooseFrameSett++;
            }
        }

        if (Input.GetKeyUp(ControlControl.x) || (Input.GetKeyUp(ControlControl.z) && currentRow == 3))
        {
            mm.SetActive(true);
            sett.SetActive(false);
            chooseFrame[0].SetActive(true);
            currentScene = 0;
            currentRow = 0;
            chooseSett[0].SetActive(false);
            chooseSett[1].SetActive(false);
            sfx.interactable = false;
            music.interactable = false;
            chooseFrameSett = 0;
            chooseSett[0].transform.localPosition = new Vector2(pos[0], -4.677f);
        }
    }

    void Scene3()
    {
        if (Input.GetKeyUp(ControlControl.z))
        {
            GameObject.Find("NavigationTextHolder").SetActive(false);
            currentScene = 0;
        }
    }

    IEnumerator Unload()
    {
        yield return null;
        SceneManager.UnloadScene("MainMenu");

    }
}
