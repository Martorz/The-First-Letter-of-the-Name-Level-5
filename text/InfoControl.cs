using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoControl : MonoBehaviour {

    public GameObject iBox, decide, attackDescription, attackChoose;
    public GameObject[] all, items;
    public Image rmk, rmkItm;
    public Sprite itemA, itemB, itmThrow, itm;
    public bool iActive = false, youCanOpenI = false;
    public string name;
    public static InfoControl info;

    public Text health, exp, lvl, namae, text, effect, actions, money, rage, def;
    public Text[] attacks, pps, anotherAttacks;

    int currentItemID, currentAttackID = 1;
    bool itemChosen = false;
    bool[] position = { true, false, false, false },
           itmPosition = { true, false };

    public GameObject menuChoose, menuChosen, menuUpr, menuSound, menuUprFrame, menuUprChoose;
    public Text menuAct;
    public Slider music, sfx;
    int menuPosition = 0;
    bool menuUprPosition = false, menuSndPosition = false, specialOccasionBattle = false;

    // Use this for initialization
    void Start () {
        info = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update () {
        if (youCanOpenI)
        {
            if ((!itemChosen && !menuChosen.activeSelf) || specialOccasionBattle)
            {
                if (iActive && Input.GetKeyDown(ControlControl.x)) //выход
                {
                    DialogController.theDC.dActive = false;
                    iBox.SetActive(false);
                    iActive = false;
                }
                else if (!iActive && Input.GetKeyDown(ControlControl.c)) //запуск
                {
                    iBox.SetActive(true);
                    iActive = true;
                    specialOccasionBattle = false;
                    DialogController.theDC.dActive = true;
                    UpdateStats();
                    UpdateItems();
                }
            }

            if (iActive)
            {
                if (!itemChosen && !menuChosen.activeSelf)
                {
                    if (Input.GetKeyUp(ControlControl.right) && all[0].activeSelf)
                    {
                        all[0].SetActive(false);
                        all[1].SetActive(true);
                    }
                    else if (Input.GetKeyUp(ControlControl.right) && all[1].activeSelf)
                    {
                        all[1].SetActive(false);
                        all[2].SetActive(true);
                    }
                    else if (Input.GetKeyUp(ControlControl.right) && all[2].activeSelf)
                    {
                        all[2].SetActive(false);
                        all[3].SetActive(true);
                    }
                    else if (Input.GetKeyUp(ControlControl.left) && all[2].activeSelf)
                    {
                        all[2].SetActive(false);
                        all[1].SetActive(true);
                    }
                    else if (Input.GetKeyUp(ControlControl.left) && all[1].activeSelf)
                    {
                        all[1].SetActive(false);
                        all[0].SetActive(true);
                    }
                    else if (Input.GetKeyUp(ControlControl.left) && all[3].activeSelf)
                    {
                        all[3].SetActive(false);
                        all[2].SetActive(true);
                    }
                }

                if (all[0].activeSelf) 
                {
                    if (Input.GetKeyUp(ControlControl.up) && (position[1] == true))
                    {
                        rmk.transform.localPosition = new Vector2(113f, 144f);
                        position[0] = true;
                        position[1] = false;
                        text.text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[0], 4];
                    }
                    else if ((Input.GetKeyUp(ControlControl.down) && (position[0] == true)) || (Input.GetKeyUp(ControlControl.up) && (position[2] == true)))
                    {
                        rmk.transform.localPosition = new Vector2(113f, 108f);
                        position[1] = true;
                        position[0] = false;
                        position[2] = false;
                        text.text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[1], 4];
                    }
                    else if ((Input.GetKeyUp(ControlControl.down) && (position[1] == true)) || (Input.GetKeyUp(ControlControl.up) && (position[3] == true)))
                    {
                        rmk.transform.localPosition = new Vector2(113f, 72f);
                        position[2] = true;
                        position[1] = false;
                        position[3] = false;
                        text.text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[2], 4];
                    }
                    else if (Input.GetKeyUp(ControlControl.down) && (position[2] == true))
                    {
                        rmk.transform.localPosition = new Vector2(113f, 36f);
                        position[3] = true;
                        position[2] = false;
                        text.text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 4];
                    }
                } //атак, стат
                else if (all[2].activeSelf)
                {
                    if (PStats.pStats.itemIndex.Count != 0)
                    {
                        if (itemChosen)
                        {
                            if (attackChoose.activeSelf)
                            {
                                if (Input.GetKeyUp(ControlControl.z))
                                {
                                    if (IfIsNotEnoughPP(currentAttackID))
                                    {
                                        PStats.pStats.ChooseItem(PStats.pStats.itemIndex[currentItemID], currentAttackID);
                                        if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 3])) PStats.pStats.itemIndex.RemoveAt(currentItemID);
                                        UpdateItems();
                                        itemChosen = false;
                                        attackChoose.SetActive(false);
                                        rmkItm.gameObject.SetActive(false);
                                        rmkItm.transform.localPosition = new Vector2(-129.5f, -126.7f);
                                        attackDescription.SetActive(true);
                                        actions.text = "Действия: A";
                                        currentAttackID = 1;
                                        UpdateStats();
                                    }
                                }
                                else if (Input.GetKeyUp(ControlControl.x))
                                {
                                    itemChosen = false;
                                    attackChoose.SetActive(false);
                                    rmkItm.gameObject.SetActive(false);
                                    rmkItm.transform.localPosition = new Vector2(-129.5f, -126.7f);
                                    attackDescription.SetActive(true);
                                    currentAttackID = 1;
                                    actions.text = "Действия: A";
                                }
                                else if (Input.GetKeyUp(ControlControl.right) && currentAttackID == 1)
                                {
                                    rmkItm.transform.localPosition = new Vector2(-47.5f, -126.7f);
                                    currentAttackID++;
                                }
                                else if (Input.GetKeyUp(ControlControl.left) && currentAttackID == 2)
                                {
                                    rmkItm.transform.localPosition = new Vector2(-158.5f, -126.7f);
                                    currentAttackID--;
                                }
                                else if (Input.GetKeyUp(ControlControl.right) && currentAttackID == 2)
                                {
                                    rmkItm.transform.localPosition = new Vector2(63.5f, -126.7f);
                                    currentAttackID++;
                                }
                                else if (Input.GetKeyUp(ControlControl.left) && currentAttackID == 3)
                                {
                                    rmkItm.transform.localPosition = new Vector2(-47.5f, -126.7f);
                                    currentAttackID--;
                                }
                            }
                            else
                            {
                                if (Input.GetKeyUp(ControlControl.z))
                                {
                                    if (itmPosition[0])
                                    {
                                        if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 7]))
                                        {
                                            attackChoose.SetActive(true);
                                            decide.SetActive(false);
                                            rmkItm.transform.localPosition = new Vector2(-158.5f, -126.7f); 
                                            for (int i = 0; i < 3; i++)
                                            {
                                                anotherAttacks[i].text = attacks[i + 1].text + "\n" + pps[i].text;
                                            }
                                        }
                                        else
                                        {
                                            PStats.pStats.ChooseItem(PStats.pStats.itemIndex[currentItemID]);
                                            if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 3])) PStats.pStats.itemIndex.RemoveAt(currentItemID);
                                            UpdateItems();
                                            itemChosen = false;
                                            decide.SetActive(false);
                                            rmkItm.gameObject.SetActive(false);
                                            attackDescription.SetActive(true);
                                            actions.text = "Действия: A";
                                            UpdateStats();
                                        }
                                    }
                                    else
                                    {
                                        if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 3])) PStats.pStats.itemIndex.RemoveAt(currentItemID);
                                        UpdateItems();
                                        itemChosen = false;
                                        decide.SetActive(false);
                                        rmkItm.gameObject.SetActive(false);
                                        attackDescription.SetActive(true);
                                        actions.text = "Действия: A";
                                        UpdateStats();
                                    }
                                    
                                }
                                else if (Input.GetKeyUp(ControlControl.x))
                                {
                                    itemChosen = false;
                                    decide.SetActive(false);
                                    rmkItm.gameObject.SetActive(false);
                                    attackDescription.SetActive(true);
                                    actions.text = "Действия: A";
                                }
                                else if (Input.GetKeyUp(ControlControl.right) && itmPosition[0])
                                {
                                    rmkItm.transform.localPosition = new Vector2(34.5f, -126.7f);
                                    itmPosition[0] = false;
                                    itmPosition[1] = true;

                                }
                                else if (Input.GetKeyUp(ControlControl.left) && itmPosition[1] && System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 4]))
                                {
                                    rmkItm.transform.localPosition = new Vector2(-129.5f, -126.7f);
                                    itmPosition[1] = false;
                                    itmPosition[0] = true;
                                }
                            }
                        }
                        else
                        {
                            if (Input.GetKeyUp(ControlControl.down))
                            {
                                items[currentItemID].GetComponent<Image>().sprite = itemA;
                                if (currentItemID >= PStats.pStats.itemIndex.Count - 1) currentItemID = 0;
                                else currentItemID += 1;
                                items[currentItemID].GetComponent<Image>().sprite = itemB;
                                attackDescription.GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]);
                                if (!actions.enabled) actions.enabled = true;
                            }
                            else if (Input.GetKeyUp(ControlControl.up))
                            {
                                items[currentItemID].GetComponent<Image>().sprite = itemA;
                                if (currentItemID == 0) currentItemID = PStats.pStats.itemIndex.Count - 1;
                                else currentItemID -= 1;
                                items[currentItemID].GetComponent<Image>().sprite = itemB;
                                attackDescription.GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]);
                                if (/*System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 4]) && */!actions.enabled) actions.enabled = true;
                                //else if (!System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 4]) && actions.enabled) actions.enabled = false;
                            }

                            if (Input.GetKeyUp(ControlControl.z)/* && System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 4])*/)
                            {
                                if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 4]))
                                {
                                    itemChosen = true;
                                    decide.SetActive(true);
                                    decide.GetComponent<Image>().sprite = itm;
                                    rmkItm.transform.localPosition = new Vector2(-129.5f, -126.7f);
                                    itmPosition[1] = false;
                                    itmPosition[0] = true;
                                    attackDescription.SetActive(false);
                                    rmkItm.gameObject.SetActive(true);
                                    actions.text = "Назад: B";
                                }
                                else
                                {
                                    itemChosen = true;
                                    decide.SetActive(true);
                                    decide.GetComponent<Image>().sprite = itmThrow;
                                    rmkItm.transform.localPosition = new Vector2(-47.5f, -126.7f);
                                    itmPosition[0] = false;
                                    itmPosition[1] = true;
                                    attackDescription.SetActive(false);
                                    rmkItm.gameObject.SetActive(true);
                                    actions.text = "Назад: B";
                                }
                            }
                        }
                    }
                } //вещи
                else if (all[3].activeSelf)
                {
                    if (!menuChosen.activeSelf)
                    {
                        if (Input.GetKeyUp(ControlControl.down))
                        {
                            if (menuPosition == 0)
                            {
                                menuPosition++;
                                menuChoose.transform.localPosition = new Vector2(-4.9f, 63.2f);
                            }
                            else if (menuPosition == 1)
                            {
                                menuPosition++;
                                menuChoose.transform.localPosition = new Vector2(-4.9f, 4.8f);
                            }
                        }
                        else if (Input.GetKeyUp(ControlControl.up))
                        {
                            if (menuPosition == 2)
                            {
                                menuPosition--;
                                menuChoose.transform.localPosition = new Vector2(-4.9f, 63.2f);
                            }
                            else if (menuPosition == 1)
                            {
                                menuPosition--;
                                menuChoose.transform.localPosition = new Vector2(-4.9f, 122.1f);
                            }
                        }

                        if (Input.GetKeyUp(ControlControl.z))
                        {
                            if (menuPosition == 0)
                            {
                                menuChosen.SetActive(true);
                                menuChosen.transform.localPosition = new Vector2(-4.998f, 122.5f);
                                menuAct.text = "Назад: В";
                                menuSound.SetActive(true);
                                music.interactable = true;
                                music.Select();
                                menuSndPosition = false;
                            }
                            else if (menuPosition == 1)
                            {
                                menuChosen.SetActive(true);
                                menuChosen.transform.localPosition = new Vector2(-4.998f, 63.5f);
                                menuAct.text = "Назад: В";
                                menuUpr.SetActive(true);
                                if (ControlControl.cc.currentControl) menuUprChoose.transform.localPosition = new Vector2(-56f, -4.6f);
                                else menuUprChoose.transform.localPosition = new Vector2(55.99f, -4.6f);
                                menuUprFrame.transform.localPosition = new Vector2(-56f, -4.6f);
                                menuUprPosition = false;
                            }
                            else if (menuPosition == 2)
                            {
                                //UnityEditor.EditorApplication.isPlaying = false;
                                Application.Quit();
                            }
                        }
                        
                    }
                    else
                    {
                        if (menuPosition == 0)
                        {
                            if (Input.GetKeyUp(ControlControl.down))
                            {
                                if (!menuSndPosition)
                                {
                                    menuSndPosition = true;
                                    music.interactable = false;
                                    sfx.interactable = true;
                                    sfx.Select();
                                }
                            }
                            else if (Input.GetKeyUp(ControlControl.up))
                            {
                                if (menuSndPosition)
                                {
                                    menuSndPosition = false;
                                    sfx.interactable = false;
                                    music.interactable = true;
                                    music.Select();
                                }
                            }
                        }
                        else if (menuPosition == 1)
                        {
                            if (Input.GetKeyUp(ControlControl.right))
                            {
                                if (!menuUprPosition)
                                {
                                    menuUprPosition = true;
                                    menuUprFrame.transform.localPosition = new Vector2(56f, -4.6f);
                                }
                            }
                            else if (Input.GetKeyUp(ControlControl.left))
                            {
                                if (menuUprPosition)
                                {
                                    menuUprPosition = false;
                                    menuUprFrame.transform.localPosition = new Vector2(-56f, -4.6f);
                                }
                            }

                            if (Input.GetKeyUp(ControlControl.z))
                            {
                                if (menuUprPosition)
                                {
                                    menuUprChoose.transform.localPosition = new Vector2(55.99f, -4.6f);
                                    ControlControl.cc.SetControl(1);
                                }
                                else
                                {
                                    menuUprChoose.transform.localPosition = new Vector2(-56f, -4.6f);
                                    ControlControl.cc.SetControl(0);
                                }
                            }
                        }

                        if (Input.GetKeyUp(ControlControl.x))
                        {
                            menuUpr.SetActive(false);
                            music.interactable = false;
                            sfx.interactable = false;
                            menuSound.SetActive(false);
                            menuChosen.SetActive(false);
                            menuAct.text = "Выбрать: А";
                        }
                    }
                } //меню
            }
        }
    }



    void UpdateStats()
    {
        health.text = "" + Mathf.Ceil(PStats.pStats.pCurrentHealth) + "/" + PStats.pStats.pMaxHealth;
        namae.text = "" + name;
        lvl.text = "" + PStats.pStats.currentLvl;
        money.text = "" + PStats.pStats.money;

        if (PStats.pStats.currentLvl == 10) exp.text = "max";
        else exp.text = "" + PStats.pStats.currentExp + "/" + PStats.pStats.needExp[PStats.pStats.currentLvl - 7];

        for (int i = 0; i < 4; i++) attacks[i].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[i], 0];
        pps[0].text = "" + PStats.pStats.ppTwo + "/" + PStats.pStats.attacks[PStats.pStats.attackIndex[1], 3];
        pps[1].text = "" + PStats.pStats.ppThree + "/" + PStats.pStats.attacks[PStats.pStats.attackIndex[2], 3];
        pps[2].text = "" + PStats.pStats.ppFour + "/" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 3];
        effect.text = "" + PStats.pStats.curEffect;
        rage.text = "" + PStats.pStats.rage;
        def.text = "" + PStats.pStats.pDefense;

        //старт позиция
        rmk.transform.localPosition = new Vector2(113f, 144f);
        position[0] = true;
        for (int i = 1; i < 4; i++) position[i] = false;
        text.text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[0], 4];
    }

    void UpdateItems()
    {
        //старт, все делается невидимым
        items[currentItemID].GetComponent<Image>().sprite = itemA;
        items[0].GetComponent<Image>().sprite = itemB;
        for (int i = 0; i < 20; i++) items[i].SetActive(false);
        
        //активируются нужные ячейки инвентаря
        for (int i = 0; i < PStats.pStats.itemIndex.Count; i++)
        {
            items[i].SetActive(true);
            items[i].GetComponentInChildren<Text>().text = System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]);
        }

        if (PStats.pStats.itemIndex.Count != 0) //проверка на нулевой инвентарь
        {
            currentItemID = 0;
            attackDescription.GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[0], 2]);
            if (!actions.enabled) actions.enabled = true;
        }
        else
        {
            currentItemID = 0;
            attackDescription.GetComponent<Text>().text = "";
        }

        //рамка выбора действия ставится на стартовую позицию
        rmkItm.transform.localPosition = new Vector2(-129.5f, -126.7f);
        itmPosition[1] = false;
        itmPosition[0] = true;
    }

    bool IfIsNotEnoughPP(int a)
    {
        if (a == 1)
        {
            if (PStats.pStats.ppTwo < System.Convert.ToInt32(PStats.pStats.attacks[PStats.pStats.attackIndex[1], 3])) return true;
            else return false;
        }
        else if (a == 2)
        {
            if (PStats.pStats.ppThree < System.Convert.ToInt32(PStats.pStats.attacks[PStats.pStats.attackIndex[2], 3])) return true;
            else return false;
        }
        else if (a == 3)
        {
            if (PStats.pStats.ppFour < System.Convert.ToInt32(PStats.pStats.attacks[PStats.pStats.attackIndex[3], 3])) return true;
            else return false;
        }
        else return false;
    }

    bool CheckIfDance()
    {
        if (LoadNewArea.lastLvlLoaded == "Club")
        {
            if (GameObject.FindObjectOfType<Dancefloor>() != null)
            {
                if (Dancefloor.thing.danceActive) return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (LoadNewArea.lastLvlLoaded == "Battle")
        {
            if (iActive) //выключает и-панель, если включена
            {
                DialogController.theDC.dActive = false;
                iBox.SetActive(false);
                iActive = false;
                specialOccasionBattle = true;
            }
        }
    }
}
