using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleInter : MonoBehaviour {

    bool[,] position = { { true, false }, { false, false } }, anPosition = { { true, false }, { false, false } }; //первое и атака
    bool[] whatChosen = { false, false, false }; //атака, имя или вещи?
    List<int> inventory = new List<int>();
    List<int> thingsToThrowOut = new List<int>();

    public GameObject rmk, mainText, attacks, rmk2, ggsprite, act, itms, blocked, description, name1, name2, creditsforBB;
    string enemy; //противник
    public int health, maxHealth, justEnemyID, maxAttack, lvlEnemy;
    public float mDefense, mDefenseSave, mAttack = 1, mSpeed;
    public string curEffect;
    public object[,] enemies = { { "ПРОЗРЕНИЕ", 17, 1, 1.5, 0.5, 8, 4, 1, -1, 13, 1.4, 20 },
                                 { "ПРИЗРАК", 18, 2, 1.5, 0.5, 8, 4, 0, 0, 10, 1.4, 3},
                                 { "ВОИН", 19, 0.5, 1.5, 0.5, 8, 4, -1, 1, 21, 1.2, 2},
                                 { "ГОЛЕМ", 23, 2, 1.5, 0.5, 8, 4, 0, 0, 25, 1.6, 50},
                                 { "ЛОРДЛИНГ", 23, 2, 1.5, 0.5, 8, 4, 0, 0, 25, 1.6, 1}};
    //имя, коэфф здоровья, коэфф атаки, коэфф влияния маг атак, коэфф влияния физ атак, коэфф хп, опыт, слабость к магическим атакам, слабость к физическим, коэфф защиты, коэфф дропа, вероятность побега
    //инвентарь
    float[] rmkPos = { -10.804f, -6.888f, -3.036f, 0.843f };
    int framePosition, itemPosition;
    bool[,] framePositionForItems = { { false, true }, { false, false } };
    public GameObject rmkItm, arrow1, arrow2;
    public GameObject[] itemBoxes, enemiesPics;

    private Animator anm; //анимация
    public Animator transition;
    public static BattleInter Bi;

    public Text logOne, logTwo; //лог битвы
    public static bool logCheck = true, StartBattle = true, bulletHell = false;
    public int[] turnsToSpeak;
    public string[] wordsToSpeak;

    bool loaded, PTurn = true, logSpeaking, chooseAttackToChange; //тоже больше по логу, но есть и другое
    int turnCounter = 0, currentStageOfWordsToSpeak = 0;

    int currentLine = 0, gotExp, gotMoney, whatSpeak, startLvl; //концовка битвы
    int lastForAttackToChange;

    public bool broccoli, invis, hat, freezePlayer; //состояния, созданные атаками или предметами

    float[] rmkNamePos = { -6.72f, -3.28f, 0.16f, 3.6f, 7.03f }; //имя
    int rmkNameCurrentPos = 0;
    public GameObject rmkName;
    public bool nameGame = false;

    // Use this for initialization
    void Start () {
        Bi = this;

        anm = ggsprite.GetComponent<Animator>();

        for (int i = 0; i < PStats.pStats.itemIndex.Count; i++) //готовит боевой инвентарь
        {
            if (System.Convert.ToBoolean(PStats.pStats.items[PStats.pStats.itemIndex[i], 6])) inventory.Add(i);
        }

        if (inventory.Count != 0)
        {
            if (inventory.Count <= 4)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    itemBoxes[i].SetActive(true);
                    itemBoxes[i].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[i]], 0]);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    itemBoxes[i].SetActive(true);
                    itemBoxes[i].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[i]], 0]);
                }
                arrow2.SetActive(true);
            }
            rmkItm.SetActive(true);
        }

        framePosition = 0;
        itemPosition = 0;

        enemy = PControl.pControl.attackedBy;  //выбирает противника
        switch (enemy)
        {
            case "eye(Clone)":
                enemiesPics[0].SetActive(true);
                justEnemyID = 0;
                lvlEnemy = Random.Range(8, 11);
                maxAttack = System.Convert.ToInt32(enemies[0, 2]) * System.Convert.ToInt32(lvlEnemy / 4);
                break;

            case "ghost(Clone)":
                enemiesPics[1].SetActive(true);
                justEnemyID = 1;
                lvlEnemy = Random.Range(8, 11);
                maxAttack = System.Convert.ToInt32(enemies[1, 2]) * System.Convert.ToInt32(lvlEnemy / 4);
                break;

            case "warrior(Clone)":
                enemiesPics[2].SetActive(true);
                justEnemyID = 2;
                lvlEnemy = Random.Range(8, 11);
                maxAttack = System.Convert.ToInt32(enemies[2, 2]) * System.Convert.ToInt32(lvlEnemy / 4);
                break;

            case "golem(Clone)":
                enemiesPics[3].SetActive(true);
                justEnemyID = 3;
                lvlEnemy = Random.Range(8, 11);
                maxAttack = System.Convert.ToInt32(enemies[3, 2]) * System.Convert.ToInt32(lvlEnemy / 4);
                break;

            case "lord":
                justEnemyID = 4;
                lvlEnemy = 10;
                maxAttack = System.Convert.ToInt32(enemies[3, 2]) * System.Convert.ToInt32(lvlEnemy / 4);
                break;
        }
        mDefense = System.Convert.ToInt32(enemies[justEnemyID, 9]) * lvlEnemy / Random.Range(5f, 10f);
        mAttack = lvlEnemy * 2 * (float)(System.Convert.ToDouble(enemies[justEnemyID, 2]));
        mDefenseSave = mDefense;
        mSpeed = 1;
        maxHealth = System.Convert.ToInt32((System.Convert.ToInt32(enemies[justEnemyID, 1]) * (lvlEnemy + 2) * (mDefense / 100 + 1)) / 2); //разделить на два - дополнение. можно убрать
        health = maxHealth;

        gameObject.GetComponent<AllAttacks>().UpdateWeaks();

        startLvl = PStats.pStats.currentLvl;
    }

    // Update is called once per frame
    void Update() {
        if (anm.GetCurrentAnimatorStateInfo(0).IsName("idle_animation") || anm.GetCurrentAnimatorStateInfo(0).IsName("death_idle_animation")) //проигралась ли боевая анимация
        {
            if (!bulletHell && !nameGame) //идет ли буллет-хэлл или имя-игра
            {
                if (PTurn) //ход гг
                {
                    if (!logSpeaking) //если в логе не пишутся никакие сообщения, меню выбора действия становится активным
                    {
                        if (PStats.pStats.pCurrentHealth <= 0) //смееееееррррррррррть, не теряй больше
                        {
                            anm.SetTrigger("death");
                            logSpeaking = true;
                            whatSpeak = 2;
                        }
                        else if (justEnemyID == 4 && currentStageOfWordsToSpeak < turnsToSpeak.Length && turnCounter == turnsToSpeak[currentStageOfWordsToSpeak])
                        {
                            //Debug.Log(wordsToSpeak[currentStageOfWordsToSpeak]);
                            //currentStageOfWordsToSpeak++;
                            logSpeaking = true;
                            whatSpeak = 3;
                        }
                        else if (freezePlayer)
                        {
                            freezePlayer = false;
                            PTurn = false;
                        }
                        else
                        {
                            if (whatChosen[0]) AttackChoose();
                            else if (whatChosen[2]) ItemChoose();
                            else if (whatChosen[1]) NameChoose();
                            else
                            {
                                //перемещает рамку и выбирает одно из четырех действий
                                if (((Input.GetAxisRaw("Horizontal") > 0.5f) && (position[0, 0] == true)) || ((Input.GetAxisRaw("Vertical") > 0.5f) && (position[1, 1] == true)))
                                {
                                    rmk.transform.position = new Vector2(10.31f, -5.46f);
                                    position[0, 1] = true;
                                    position[0, 0] = false;
                                    position[1, 1] = false;
                                }
                                else if (((Input.GetAxisRaw("Horizontal") < -0.5f) && (position[0, 1] == true)) || ((Input.GetAxisRaw("Vertical") > 0.5f) && (position[1, 0] == true)))
                                {
                                    rmk.transform.position = new Vector2(6.49f, -5.46f);
                                    position[0, 0] = true;
                                    position[0, 1] = false;
                                    position[1, 0] = false;
                                }
                                else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (position[0, 0] == true)) || ((Input.GetAxisRaw("Horizontal") < -0.5f) && (position[1, 1] == true)))
                                {
                                    rmk.transform.position = new Vector2(6.49f, -7.36f);
                                    position[1, 0] = true;
                                    position[0, 0] = false;
                                    position[1, 1] = false;
                                }
                                else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (position[0, 1] == true)) || ((Input.GetAxisRaw("Horizontal") > 0.5f) && (position[1, 0] == true)))
                                {
                                    rmk.transform.position = new Vector2(10.31f, -7.36f);
                                    position[1, 1] = true;
                                    position[0, 1] = false;
                                    position[1, 0] = false;
                                }
                                
                                if (Input.GetKeyDown(ControlControl.z))
                                {
                                    if (position[1, 1] == true)
                                    {
                                        Run();
                                    }
                                    else if (position[0, 0] == true)
                                    {
                                        mainText.SetActive(false);
                                        attacks.SetActive(true);
                                        blocked.SetActive(false);
                                        whatChosen[0] = true;
                                    }
                                    else if (position[1, 0] == true)
                                    {
                                        mainText.SetActive(false);
                                        itms.SetActive(true);
                                        blocked.SetActive(true);
                                        whatChosen[2] = true;
                                    }
                                    else if (position[0, 1] == true)
                                    {
                                        mainText.SetActive(false);
                                        name1.SetActive(true);
                                        whatChosen[1] = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (chooseAttackToChange) AttackChooseToChange();
                        else SpeakFromMyHeart(whatSpeak);
                    }
                }
                else //ход противника
                {
                    if (!broccoli)
                    {
                        if (logOne.text == "")
                        {
                            logOne.text = "Монстр " + System.Convert.ToString(enemies[justEnemyID, 0]) + " атакует.";
                        }
                        else if (logTwo.text == "")
                        {
                            logTwo.text = "Монстр " + System.Convert.ToString(enemies[justEnemyID, 0]) + " атакует.";
                        }
                        else
                        {
                            logOne.text = logTwo.text;
                            logTwo.text = "Монстр " + System.Convert.ToString(enemies[justEnemyID, 0]) + " атакует.";
                        }
                        act.transform.position = new Vector2(-0.68f, 1.46f);
                        act.SetActive(true);
                        bulletHell = true;
                        turnCounter++;
                        PTurn = true;
                    }
                    else PTurn = true;
                }
            }
        }
    }

    void Run()
    {
        if (justEnemyID == 4)
        {
            DefaultLogUpdate("Нельзя сбежать из босс-битвы.");
        }
        else
        {
            int a = Random.Range(0, (int)enemies[justEnemyID, 11]);
            Debug.Log(a);
            if (a == 0 || PStats.pStats.godgodmode)
            {
                StartBattle = false;
                DeleteItems();
                PControl.leavingBattle = true;
                logSpeaking = true;
                whatSpeak = 1;
            }
            else
            {
                DefaultLogUpdate("Вам не удалось сбежать.");
                PTurn = false;
            }
        }
    }

    void Attack(int id) {
        AllAttacks.allAttacks.ChooseAttack(id);
        if (id != -1)
        {
            AttackTextControl.atc.UpdatePP(id);
            mainText.SetActive(true);
            attacks.SetActive(false);
        }
        if (health > 0)
        {
            PTurn = false;
        }
        else
        {
            StartBattle = false;
            if (justEnemyID == 4)
            {
                whatSpeak = 4;
                enemiesPics[0].GetComponent<Animator>().SetTrigger("death");
                gotMoney = (int)(50000 / PStats.pStats.currentLvl);
                PStats.pStats.AddMoney(gotMoney);
            }
            else
            {
                enemiesPics[justEnemyID].GetComponent<Animator>().SetTrigger("death");
                gotExp = (int)(lvlEnemy * lvlEnemy * 10 * System.Convert.ToDouble(enemies[justEnemyID, 10]) / PStats.pStats.currentLvl);
                PStats.pStats.AddExp(gotExp);
                gotMoney = (int)(lvlEnemy * lvlEnemy * mDefenseSave * System.Convert.ToDouble(enemies[justEnemyID, 10]) / PStats.pStats.currentLvl);
                PStats.pStats.AddMoney(gotMoney);
                whatSpeak = 0;
            }
            DeleteItems();
            PControl.leavingBattle = true;
            logSpeaking = true;
        }
    }
    
    void AttackChoose()
    {
        if (((Input.GetAxisRaw("Horizontal") > 0.5f) && (anPosition[0, 0] == true)) || ((Input.GetAxisRaw("Vertical") > 0.5f) && (anPosition[1, 1] == true)))
        {
            rmk2.transform.position = new Vector2(-0.41f, -5.41f);
            anPosition[0, 1] = true;
            anPosition[0, 0] = false;
            anPosition[1, 1] = false;
        }
        else if (((Input.GetAxisRaw("Horizontal") < -0.5f) && (anPosition[0, 1] == true)) || ((Input.GetAxisRaw("Vertical") > 0.5f) && (anPosition[1, 0] == true)))
        {
            rmk2.transform.position = new Vector2(-8.37f, -5.41f);
            anPosition[0, 0] = true;
            anPosition[0, 1] = false;
            anPosition[1, 0] = false;
        }
        else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (anPosition[0, 0] == true)) || ((Input.GetAxisRaw("Horizontal") < -0.5f) && (anPosition[1, 1] == true)))
        {
            rmk2.transform.position = new Vector2(-8.37f, -7.29f);
            anPosition[1, 0] = true;
            anPosition[0, 0] = false;
            anPosition[1, 1] = false;
        }
        else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (anPosition[0, 1] == true)) || ((Input.GetAxisRaw("Horizontal") > 0.5f) && (anPosition[1, 0] == true)))
        {
            rmk2.transform.position = new Vector2(-0.41f, -7.29f);
            anPosition[1, 1] = true;
            anPosition[0, 1] = false;
            anPosition[1, 0] = false;
        }


        if (Input.GetKeyDown(ControlControl.z))
        {
            bool good = false;
            if (anPosition[1, 1] == true)
            {
                if (PStats.pStats.ppFour > 0)
                {
                    whatChosen[0] = false;
                    good = true;
                    Attack(PStats.pStats.attackIndex[3]);
                    AttackTextControl.atc.UpdatePP(3);
                    anm.SetTrigger("stat_attacking");
                }

            }
            else if (anPosition[0, 0] == true)
            {
                whatChosen[0] = false;
                good = true;
                Attack(PStats.pStats.attackIndex[0]);
                anm.SetTrigger("phys_attacking");
            }
            else if (anPosition[0, 1] == true)
            {
                if (PStats.pStats.ppTwo > 0)
                {
                    whatChosen[0] = false;
                    good = true;
                    Attack(PStats.pStats.attackIndex[1]);
                    AttackTextControl.atc.UpdatePP(1);
                    anm.SetTrigger("phys_attacking");
                }
            }
            else if (anPosition[1, 0] == true)
            {
                if (PStats.pStats.ppThree > 0)
                {
                    whatChosen[0] = false;
                    good = true;
                    Attack(PStats.pStats.attackIndex[2]);
                    AttackTextControl.atc.UpdatePP(2);
                    anm.SetTrigger("mag_attacking");
                }
            }

            if (good)
            {
                anPosition[0, 0] = true;
                anPosition[0, 1] = false;
                anPosition[1, 0] = false;
                anPosition[1, 1] = false;
                rmk2.transform.position = new Vector2(-8.37f, -5.41f);
                whatChosen[0] = false;
            }
        }

        if (Input.GetKeyDown(ControlControl.x))
        {
            anPosition[0, 0] = true;
            anPosition[0, 1] = false;
            anPosition[1, 0] = false;
            anPosition[1, 1] = false;
            rmk2.transform.position = new Vector2(-8.37f, -5.41f);
            whatChosen[0] = false;
            mainText.SetActive(true);
            attacks.SetActive(false);
        }
    }

    void ItemChoose()
    {
        if (attacks.activeSelf)
        {
            if ((Input.GetAxisRaw("Vertical") > 0.5f) && (framePositionForItems[1, 1] == true))
            {
                rmk2.transform.position = new Vector2(-0.41f, -5.41f);
                framePositionForItems[0, 1] = true;
                framePositionForItems[1, 1] = false;
            }
            else if ((Input.GetAxisRaw("Horizontal") < -0.5f) && (framePositionForItems[1, 1] == true))
            {
                rmk2.transform.position = new Vector2(-8.37f, -7.29f);
                framePositionForItems[1, 0] = true;
                framePositionForItems[1, 1] = false;
            }
            else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (framePositionForItems[0, 1] == true)) || ((Input.GetAxisRaw("Horizontal") > 0.5f) && (framePositionForItems[1, 0] == true)))
            {
                rmk2.transform.position = new Vector2(-0.41f, -7.29f);
                framePositionForItems[1, 1] = true;
                framePositionForItems[0, 1] = false;
                framePositionForItems[1, 0] = false;
            }

            if (Input.GetKeyDown(ControlControl.z))
            {
                bool good = false;
                if (framePositionForItems[1, 1] == true)
                {
                    if (IfIsNotEnoughPP(3))
                    {
                        PStats.pStats.ChooseItem(PStats.pStats.itemIndex[inventory[itemPosition]], 3);
                        good = true;
                    }
                }
                else if (framePositionForItems[0, 1] == true)
                {
                    if (IfIsNotEnoughPP(1))
                    {
                        PStats.pStats.ChooseItem(PStats.pStats.itemIndex[inventory[itemPosition]], 1);
                        good = true;
                    }
                }
                else if (framePositionForItems[1, 0] == true)
                {
                    if (IfIsNotEnoughPP(2))
                    {
                        PStats.pStats.ChooseItem(PStats.pStats.itemIndex[inventory[itemPosition]], 2);
                        good = true;
                    }
                }
                if (good)
                {
                    framePositionForItems[0, 0] = false;
                    framePositionForItems[0, 1] = true;
                    framePositionForItems[1, 0] = false;
                    framePositionForItems[1, 1] = false;
                    rmk2.transform.position = new Vector2(-8.37f, -5.41f);
                    AttackTextControl.atc.UpdatePP(4);
                    Attack(-1); //для случая, когда моб страдает от эффекта
                    ItemTextControl(1);
                    attacks.SetActive(false);
                    BasicInventoryCleaning();
                }
            }

            if (Input.GetKeyDown(ControlControl.x))
            {
                framePositionForItems[0, 0] = false;
                framePositionForItems[0, 1] = true;
                framePositionForItems[1, 0] = false;
                framePositionForItems[1, 1] = false;
                rmk2.transform.position = new Vector2(-8.37f, -5.41f);
                attacks.SetActive(false);
                itms.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyUp(ControlControl.right))
            {
                if (inventory.Count > 0)
                {
                    if (itemPosition != inventory.Count - 1)
                    {
                        itemPosition++;
                        if (itemPosition > 3 && framePosition == 3)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                itemBoxes[i].GetComponentInChildren<Text>().text = "" + itemBoxes[i + 1].GetComponentInChildren<Text>().text;
                            }
                            itemBoxes[3].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[itemPosition]], 0]);
                        }
                        else
                        {
                            framePosition++;
                            rmkItm.transform.localPosition = new Vector2(rmkPos[framePosition], -4.782f);
                        }

                        if (itemPosition == inventory.Count - 1) arrow2.SetActive(false);
                        if (itemPosition - 3 > 0) arrow1.SetActive(true);
                    }
                }
            }
            else if (Input.GetKeyUp(ControlControl.left))
            {
                if (inventory.Count > 0)
                {
                    if (itemPosition != 0)
                    {
                        itemPosition--;
                        if (itemPosition >= 0 && framePosition == 0)
                        {
                            for (int i = 3; i > 0; i--)
                            {
                                itemBoxes[i].GetComponentInChildren<Text>().text = "" + itemBoxes[i - 1].GetComponentInChildren<Text>().text;
                            }
                            itemBoxes[0].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[itemPosition]], 0]);
                        }
                        else
                        {
                            framePosition--;
                            rmkItm.transform.localPosition = new Vector2(rmkPos[framePosition], -4.782f);
                        }

                        if (itemPosition == 0) arrow1.SetActive(false);
                        if (itemPosition + 3 < inventory.Count - 1) arrow2.SetActive(true);
                    }
                }
            }

            if (Input.GetKeyDown(ControlControl.z))
            {
                if (inventory.Count > 0)
                {
                    if (PStats.pStats.itemIndex[inventory[itemPosition]] == 1)
                    {
                        rmk2.transform.position = new Vector2(-0.41f, -5.41f);
                        attacks.SetActive(true);
                        itms.SetActive(false);
                    }
                    else
                    {
                        PStats.pStats.ChooseItem(PStats.pStats.itemIndex[inventory[itemPosition]]);
                        if (PStats.pStats.itemIndex[inventory[itemPosition]] != 3 && PStats.pStats.itemIndex[inventory[itemPosition]] != 4) Attack(-1); //для случая, когда моб страдает от эффекта
                        ItemTextControl(PStats.pStats.itemIndex[inventory[itemPosition]]);
                        BasicInventoryCleaning();
                    }

                }
            }

            if (Input.GetKeyDown(ControlControl.x))
            {
                whatChosen[2] = false;
                mainText.SetActive(true);
                itms.SetActive(false);
            }
        }
    }

    void NameChoose()
    {
        if (!nameGame)
        {
            if (Input.GetKeyUp(ControlControl.right))
            {
                if (rmkNameCurrentPos != 4)
                {
                    rmkNameCurrentPos++;
                    rmkName.transform.localPosition = new Vector2(rmkNamePos[rmkNameCurrentPos], 0.13f);
                }
            }
            else if (Input.GetKeyUp(ControlControl.left))
            {
                if (rmkNameCurrentPos != 0)
                {
                    rmkNameCurrentPos--;
                    rmkName.transform.localPosition = new Vector2(rmkNamePos[rmkNameCurrentPos], 0.13f);
                }
            }

            if (Input.GetKeyDown(ControlControl.z))
            {
                if (PStats.pStats.currentLvl >= lvlEnemy)
                {
                    whatChosen[1] = false;
                    mainText.SetActive(true);
                    name1.SetActive(false);
                    DefaultLogUpdate("Противник должен быть боссом, или его уровень должен быть выше вашего.");
                }
                else if (AllAttacks.allAttacks.nameUsed[rmkNameCurrentPos])
                {
                    whatChosen[1] = false;
                    mainText.SetActive(true);
                    name1.SetActive(false);
                    DefaultLogUpdate("Данную атаку можно использовать только один раз за бой.");
                }
                else
                {
                    name1.SetActive(false);
                    switch (rmkNameCurrentPos)
                    {
                        case 0:
                            name2.GetComponent<GameInter>().GameSetup(0, 15, 0.04f, 0.08f);
                            break;

                        case 1:
                            name2.GetComponent<GameInter>().GameSetup(1, 15, 0.04f, 0.08f);
                            break;

                        case 2:
                            name2.GetComponent<GameInter>().GameSetup(2, 22, 0.06f, 0.1f);
                            break;

                        case 3:
                            name2.GetComponent<GameInter>().GameSetup(3, 22, 0.06f, 0.1f);
                            break;

                        case 4:
                            name2.GetComponent<GameInter>().GameSetup(4, 8, 0.02f, 0.07f);
                            break;
                    }
                    name2.SetActive(true);
                    nameGame = true;
                    whatChosen[1] = false;
                }
            }

            if (Input.GetKeyDown(ControlControl.x))
            {
                whatChosen[1] = false;
                mainText.SetActive(true);
                name1.SetActive(false);
            }
        }
    }

    void SpeakFromMyHeart(int num = 0)
    {
        if (currentLine == 0)
        {
            string txt;
            switch (num)
            {
                case 0: //endgame
                    txt = "Единиц опыта получено: " + System.Convert.ToString(gotExp) + ".";
                    DefaultLogUpdate(txt);
                    if (startLvl != PStats.pStats.currentLvl) currentLine++;
                    else currentLine = 4;
                    break;

                case 1: //run
                    txt = "Вам удалось сбежать.";
                    DefaultLogUpdate(txt);
                    currentLine++;
                    break;

                case 2: //dead
                    txt = "Битва закончилась. Вы проиграли.";
                    DefaultLogUpdate(txt);
                    currentLine++;
                    break;

                case 3: //just speak
                    txt = wordsToSpeak[currentStageOfWordsToSpeak];
                    currentStageOfWordsToSpeak++;
                    DefaultLogUpdate(txt);
                    if (currentStageOfWordsToSpeak < turnsToSpeak.Length && turnCounter == turnsToSpeak[currentStageOfWordsToSpeak]) currentLine++;
                    else
                    {
                        currentLine = 0;
                        logSpeaking = false;
                    }
                    break;

                case 4: //boss endgame
                    txt = "Денежных единиц получено: " + System.Convert.ToString(gotMoney) + ".";
                    DefaultLogUpdate(txt);
                    currentLine++;
                    break;
            }
        }
        else
        {
            if (Input.GetKeyDown(ControlControl.z))
            {
                switch (num)
                {
                    case 0: //endgame
                        string txt;
                        switch (currentLine)
                        {
                            case 1: //уровень
                                txt = "Вы получили новый уровень! Ваш уровень: " + System.Convert.ToString(startLvl + 1) + ".";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 2: //атаки
                                txt = "Вы можете выучить новые атаки. Продолжить?                                     Да: A  Нет: B";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 3: //включает выбор атаки на замену(разветвление)
                                int a = 4;
                                if (PStats.pStats.currentLvl == 10) a += 4;
                                attacks.SetActive(true);
                                blocked.SetActive(true);
                                rmk2.transform.position = new Vector2(-0.41f, -5.41f);
                                AttackTextControl.atc.display[1].text = "" + PStats.pStats.attacks[a + 1, 0];
                                AttackTextControl.atc.PPs[0].text = "" + PStats.pStats.attacks[a + 1, 3] + "/" + PStats.pStats.attacks[a + 1, 3];
                                chooseAttackToChange = true;
                                lastForAttackToChange = 1;
                                description.SetActive(true);
                                description.GetComponentInChildren<Text>().text = "" + PStats.pStats.attacks[a + 1, 4];
                                mainText.SetActive(false);
                                break;

                            case 4: //money
                                txt = "Денежных единиц получено: " + System.Convert.ToString(gotMoney) + ".";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            default:
                                if (loaded == false)
                                {
                                    transition.SetTrigger("battex");
                                    SceneManager.LoadScene("Scene1", LoadSceneMode.Additive);
                                    InfoControl.info.youCanOpenI = true;
                                    LoadNewArea.loadNA.UnloadScene(LoadNewArea.lastLvlLoaded);
                                    LoadNewArea.lastLvlLoaded = "Scene1";
                                    loaded = true;
                                }
                                break;
                        }
                        break;

                    case 1: //run
                        switch (currentLine)
                        {
                            default:
                                if (loaded == false)
                                {
                                    transition.SetTrigger("battex");
                                    SceneManager.LoadScene("Scene1", LoadSceneMode.Additive);
                                    InfoControl.info.youCanOpenI = true;
                                    LoadNewArea.loadNA.UnloadScene(LoadNewArea.lastLvlLoaded);
                                    LoadNewArea.lastLvlLoaded = "Scene1";
                                    loaded = true;
                                }
                                break;
                        }
                        break;

                    case 2: //dead
                        switch (currentLine)
                        {
                            case 1:
                                txt = "Вы не получили опыта.";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 2:
                                txt = "Вы не получили денег.";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 3:
                                txt = "Но, благодаря точкам сохранения, вы можете вернуться назад во времени...";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 4:
                                txt = "...и исправить свою ошибку.";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            default:
                                if (loaded == false)
                                {
                                    transition.SetTrigger("battex");
                                    PStats.pStats.SetBasicStats();
                                    ShopControl.thing.RestartShop();
                                    SceneManager.LoadScene(LoadNewArea.lastLvlLoaded, LoadSceneMode.Additive);
                                    InfoControl.info.youCanOpenI = true;
                                    if (justEnemyID == 4) LoadNewArea.loadNA.UnloadScene("BossBattle");
                                    else LoadNewArea.loadNA.UnloadScene("Battle");
                                    loaded = true;
                                }
                                break;
                        }
                        break;

                    case 3: //just speak
                        txt = wordsToSpeak[currentStageOfWordsToSpeak];
                        currentStageOfWordsToSpeak++;
                        DefaultLogUpdate(txt);
                        if (currentStageOfWordsToSpeak >= turnsToSpeak.Length || turnCounter != turnsToSpeak[currentStageOfWordsToSpeak])
                        {
                            currentLine = 0;
                            logSpeaking = false;
                        }
                        break;

                    case 4: //boss endgame
                        switch (currentLine)
                        {
                            case 1:
                                txt = "Босс Уровня 5 был повержен.";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 2:
                                creditsforBB.SetActive(true);
                                currentLine++;
                                break;

                            default:
                                if (creditsforBB.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("credits")) creditsforBB.GetComponent<Animator>().Play("credits_idle");
                                else
                                {
                                    Application.Quit();
                                    /*if (loaded == false)
                                    {
                                        LoadNewArea.loadNA.UnloadScene("EverythingImportant");
                                        SceneManager.LoadSceneAsync("EverythingImportant", LoadSceneMode.Additive);
                                        LoadNewArea.loadNA.UnloadScene("BossBattle");
                                        loaded = true;
                                    }*/
                                }
                                break;
                        }
                        break;
                }
            }

            if (Input.GetKeyDown(ControlControl.x))
            {
                switch (num)
                {
                    case 0: //endgame
                        string txt;
                        switch (currentLine)
                        {
                            case 3: //провiрка(разветвление)
                                txt = "Вы уверены?                                                                                           Да: A  Нет: B";
                                DefaultLogUpdate(txt);
                                currentLine++;
                                break;

                            case 4: //атаки(разветвление)
                                txt = "Вы можете выучить новые атаки. Продолжить?                                     Да: A  Нет: B";
                                DefaultLogUpdate(txt);
                                currentLine--;
                                break;

                            default:
                                
                                break;
                        }
                        break;
                }
            }
        }
    }

    void AttackChooseToChange()
    {
        int a = 4;
        if (PStats.pStats.currentLvl == 10) a += 4;
        if ((Input.GetAxisRaw("Vertical") > 0.5f) && (framePositionForItems[1, 1] == true))
        {
            AttackTextControl.atc.display[1].text = "" + PStats.pStats.attacks[a + 1, 0];
            AttackTextControl.atc.PPs[0].text = "" + PStats.pStats.attacks[a + 1, 3] + "/" + PStats.pStats.attacks[a + 1, 3];
            description.GetComponentInChildren<Text>().text = "" + PStats.pStats.attacks[a + 1, 4];
            framePositionForItems[0, 1] = true;
            lastForAttackToChange = 1;
            rmk2.transform.position = new Vector2(-0.41f, -5.41f); //задает новое значение

            AttackTextControl.atc.display[3].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 0];
            AttackTextControl.atc.PPs[2].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 3] + "/" + PStats.pStats.ppFour;
            framePositionForItems[1, 1] = false; //возвращает старое
        }
        else if ((Input.GetAxisRaw("Horizontal") < -0.5f) && (framePositionForItems[1, 1] == true))
        {
            AttackTextControl.atc.display[2].text = "" + PStats.pStats.attacks[a + 2, 0];
            AttackTextControl.atc.PPs[1].text = "" + PStats.pStats.attacks[a + 2, 3] + "/" + PStats.pStats.attacks[a + 2, 3];
            description.GetComponentInChildren<Text>().text = "" + PStats.pStats.attacks[a + 2, 4];
            rmk2.transform.position = new Vector2(-8.37f, -7.29f);
            lastForAttackToChange = 2;
            framePositionForItems[1, 0] = true;

            AttackTextControl.atc.display[3].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 0];
            AttackTextControl.atc.PPs[2].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[3], 3] + "/" + PStats.pStats.ppFour;
            framePositionForItems[1, 1] = false;
        }
        else if (((Input.GetAxisRaw("Vertical") < -0.5f) && (framePositionForItems[0, 1] == true)) || ((Input.GetAxisRaw("Horizontal") > 0.5f) && (framePositionForItems[1, 0] == true)))
        {
            AttackTextControl.atc.display[3].text = "" + PStats.pStats.attacks[a + 3, 0];
            AttackTextControl.atc.PPs[2].text = "" + PStats.pStats.attacks[a + 3, 3] + "/" + PStats.pStats.attacks[a + 3, 3];
            description.GetComponentInChildren<Text>().text = "" + PStats.pStats.attacks[a + 3, 4];
            rmk2.transform.position = new Vector2(-0.41f, -7.29f);
            framePositionForItems[1, 1] = true;
            lastForAttackToChange = 3;

            AttackTextControl.atc.display[2].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[2], 0];
            AttackTextControl.atc.PPs[1].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[2], 3] + "/" + PStats.pStats.ppThree;
            AttackTextControl.atc.display[1].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[1], 0];
            AttackTextControl.atc.PPs[0].text = "" + PStats.pStats.attacks[PStats.pStats.attackIndex[1], 3] + "/" + PStats.pStats.ppTwo;
            framePositionForItems[0, 1] = false;
            framePositionForItems[1, 0] = false;
        }

        if (Input.GetKeyDown(ControlControl.z))
        {
            string txt;
            txt = "Денежных единиц получено: " + System.Convert.ToString(gotMoney) + ".";
            DefaultLogUpdate(txt);
            framePositionForItems[0, 0] = false;
            framePositionForItems[0, 1] = true;
            framePositionForItems[1, 0] = false;
            framePositionForItems[1, 1] = false;
            rmk2.transform.position = new Vector2(-8.37f, -5.41f);
            attacks.SetActive(false);
            chooseAttackToChange = false;
            description.SetActive(false);
            currentLine = 5;
            PStats.pStats.ChangeAttack(lastForAttackToChange);
            mainText.SetActive(true);
        }

        if (Input.GetKeyDown(ControlControl.x))
        {
            framePositionForItems[0, 0] = false;
            framePositionForItems[0, 1] = true;
            framePositionForItems[1, 0] = false;
            framePositionForItems[1, 1] = false;
            rmk2.transform.position = new Vector2(-8.37f, -5.41f);
            AttackTextControl.atc.UpdatePP(lastForAttackToChange);
            AttackTextControl.atc.UpdateAttack(lastForAttackToChange);
            attacks.SetActive(false);
            description.SetActive(false);
            chooseAttackToChange = false;
            currentLine = 3;
            mainText.SetActive(true);
        }
    }

    void BasicInventoryCleaning()
    {
        thingsToThrowOut.Add(inventory[itemPosition]);
        inventory.RemoveAt(itemPosition);

        //рестарт инвентаря
        rmkItm.transform.localPosition = new Vector2(rmkPos[0], -4.782f);
        rmkItm.SetActive(false);
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        for (int i = 0; i < 4; i++) itemBoxes[i].SetActive(false);
        if (inventory.Count != 0)
        {

            if (inventory.Count <= 4)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    itemBoxes[i].SetActive(true);
                    itemBoxes[i].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[i]], 0]);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    itemBoxes[i].SetActive(true);
                    itemBoxes[i].GetComponentInChildren<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[inventory[i]], 0]);
                }
                arrow2.SetActive(true);
            }
            rmkItm.SetActive(true);
        }
        framePosition = 0;
        itemPosition = 0;
        //конец рестарта

        whatChosen[2] = false;
        if (PStats.pStats.itemIndex[inventory[itemPosition]] != 3 && PStats.pStats.itemIndex[inventory[itemPosition]] != 4) PTurn = false; //если это не БП и не ЖШ, ход переходит врагу
        mainText.SetActive(true);
        itms.SetActive(false);
    }

    void DeleteItems()
    {
        if (thingsToThrowOut.Count != 0)
        {
            for (int i = 0; i < thingsToThrowOut.Count; i++) PStats.pStats.itemIndex[thingsToThrowOut[i]] = -1;
            for (int i = 0; i < thingsToThrowOut.Count; i++) PStats.pStats.itemIndex.Remove(-1);
        }
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

    public void DefaultLogUpdate(string txt)
    {
            if (logOne.text == "")
            {
                logOne.text = "" + txt;
            }
            else if (logTwo.text == "")
            {
                logTwo.text = "" + txt;
            }
            else
            {
                logOne.text = logTwo.text;
                logTwo.text = "" + txt;
            }
    }

    void ItemTextControl(int a)
    {
        string txt;
        switch (a)
        {
            case 0:
                txt = "Вы использовали пуддинг и восстановили 40 очков здоровья.";
                break;

            case 1:
                txt = "Вы использовали лакрицу. Очки атак восстановлены.";
                break;

            case 3:
                txt = "Вы использовали пустоту. Вы почуствовали себя прозрачным.";
                break;

            case 4:
                txt = "Вы использовали шляпу. Ваша голова защищена.";
                break;

            default:
                txt = "Шо.";
                break;
        }

        DefaultLogUpdate(txt);
    }

    public void NameAttack()
    {
        AllAttacks.allAttacks.ChooseAttack(rmkNameCurrentPos + 12);
        if (GameInter.inter.result != 0)
        {
            switch (rmkNameCurrentPos)
            {
                case 0:
                    anm.SetTrigger("var");
                    break;

                case 1:
                    anm.SetTrigger("ieg");
                    break;

                case 2:
                    anm.SetTrigger("raf");
                    break;

                case 3:
                    anm.SetTrigger("url");
                    break;

                case 4:
                    anm.SetTrigger("sel");
                    break;
            }
        }

        if (health > 0)
        {
            PTurn = false;
        }
        else
        {
            StartBattle = false;
            if (justEnemyID == 4)
            {
                whatSpeak = 4;
                enemiesPics[0].GetComponent<Animator>().SetTrigger("death");
                gotMoney = (int)(50000 / PStats.pStats.currentLvl);
                PStats.pStats.AddMoney(gotMoney);
            }
            else
            {
                enemiesPics[justEnemyID].GetComponent<Animator>().SetTrigger("death");
                gotExp = (int)(lvlEnemy * lvlEnemy * 10 * System.Convert.ToDouble(enemies[justEnemyID, 10]) / PStats.pStats.currentLvl);
                PStats.pStats.AddExp(gotExp);
                gotMoney = (int)(lvlEnemy * lvlEnemy * mDefenseSave * System.Convert.ToDouble(enemies[justEnemyID, 10]) / PStats.pStats.currentLvl);
                PStats.pStats.AddMoney(gotMoney);
                whatSpeak = 0;
            }
            DeleteItems();
            PControl.leavingBattle = true;
            logSpeaking = true;
        }
    }

}
