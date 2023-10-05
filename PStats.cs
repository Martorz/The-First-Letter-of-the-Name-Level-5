using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PStats : MonoBehaviour
{

    public static PStats pStats;

    public int pMaxHealth, money, rage = 8;
    public double currentAttack;
    public float pCurrentHealth, pDefense = 8;
    public string curEffect, lastSortingLayer = "All";

    //уровень
    public int currentLvl, currentExp;
    
    //pp и вещи
    public int ppTwo, ppThree, ppFour;
    public int[] attackIndex = { 0, 1, 2, 3 };
    public List<int> itemIndex = new List<int> { 0, 1, 2 };

    //инфа
    public object[,] attacks = { {"Удар IV", 1, 5, "--", "Базовый удар. Ничем не ослабляется и не усиляется. Слабый, но не имеет ограничений по PP."}, //название, мин урон, макс урон, пп, описание
                                 {"Нацеленный удар", 7, 15, 10, "Специальная физическая атака. С каждым ударом повышает вероятность критической атаки." },
                                 {"Черничная сфера", 5, 13, 10, "Магическая атака. Снижает защиту противника." },
                                 {"Радиоактивный пепел", "--", "--", 7, "Окружает противника ядовитым пеплом. Вдохнув его, существо мгновенно отравляется и начинает терять защиту." },
                                 {"Удар V", 5, 13, "--", "Базовый удар. Ничем не ослабляется и не усиляется. Слабый, но не имеет ограничений по PP." },
                                 {"Уширо-Гери", 5, 10, 10, "Специальная физическая атака. Чем больше защита и уровень противника, тем сильнее атака." },
                                 {"Хвойная молния", 5, 13, 7, "Магическая атака. Имеет малый разброс в вероятном уроне. Говорят, хвоя полезна для здоровья. Прибавляет к HP 5% от имеющегося." },
                                 {"Тепловая волна", "--", "--", 7, "Жар снижает скорость и способность противника атаковать." },
                                 {"Удар V", 5, 13, "--", "Базовый удар. Ничем не ослабляется и не усиляется. Слабый, но не имеет ограничений по PP." },
                                 {"Апперкот", 5, 13, 5, "Специальная физическая атака. Не ослабляется защитой противника. Немного пробивает устойчивость, если имеется." },
                                 {"Чернильный луч", 5, 13, 5, "Магическая атака. Не ослабляется защитой противника. Немного пробивает устойчивость, если имеется." },
                                 {"Брокколи", "--", "--", 3, "Превращает противника в овощ на несколько ходов, лишая его способности атаковать, и делает его уязвимым к физическим атакам." } };
    public object[,] items = { { "Пуддинг", 15, "Улучшенный Пуддинг, выпускавшийся много лет назад. Удивительно, но по каким-то причинам он все еще съедобен.\nВосстанавливает 40 единиц здоровья.", true, true, 10, true, false},
                               { "Лакрица", 20, "Лакрица много лет настаивалась в заброшенном магазине. Она явно не годилась для продажи.\nПолностью восстанавливает одну атаку.", true, true, 10 , true, true},
                               { "Банан", 1000, "Просто бесполезный гаджет. Ну и че ты с ним будешь делать?", false, false, -1, false, false},
                               { "Банка самовнушения", 70, "Дает неуязвимость на один ход.", true, false, 60, true, false},
                               { "Шляпа мафиозника", 60, "Шляпа Михаила Петровича. \"Соприкоснись с прекрасным\". Защищает голову.", true, false, 50, true, false},
                               { "Яблоко", 2000, "Просто бесполезный гаджет. Зачем он вообще был куплен?", false, false, -1, false, false},
                               { "Яблоко Х", 5000, "Просто бесполезный гаджет. Зачем он вообще был куплен?", false, false, -1, false, false},
                               { "Ананас", 10000, "Просто бесполезный гаджет. Зачем он вообще был куплен?", false, false, -1, false, false},
                               { "Серебро", 1000, "Броня. Увеличивает ярость на единицу и немного повышает атаку.", true, true, -1, false, false},
                               { "Золото", 2000, "Броня. Увеличивает ярость на единицу и немного повышает атаку.", true, true, -1, false, false},
                               { "Платина", 3000, "Броня. Увеличивает ярость и атаку на единицу и повышает атаку.", true, true, -1, false, false}}; //название, цена покупки, описание, сколько использовать/покупать (тру - один раз исп/много покупать, фолс - много раз/один раз покупать), можно ли использовать вне боя, цена продажи, можно ли использовать в бою, восстанавливает атаку
    public int[] needExp = { 0, 100, 200 }; //800, 1200
    int[] needHealth = { 20, 20, 45, 60, 65, 75, 90, 100, 125, 150 };
    double[] needAttack = { 1, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };

    public bool ifLoading;
    public Vector3 startPointForLoading;

    //~*~
    public bool godgodmode;
    //~*~

    // Use this for initialization
    void Start()
    {
        if (currentLvl != 10)
        {
            if (currentExp >= needExp[currentLvl - 7])
            {
                pMaxHealth = needHealth[currentLvl];
                currentAttack = needAttack[currentLvl];
                currentLvl++;
                currentExp = 0;
            }
        }
        pStats = this;
        pCurrentHealth = pMaxHealth;
        SetMaxPP(4);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLvl != 10)
        {
            if (currentExp >= needExp[currentLvl - 7])
            {
                currentExp = 0;
                attackIndex[0] += 4;
                AttackTextControl.atc.UpdateAttack(0);
                pMaxHealth = needHealth[currentLvl];
                currentAttack += 0.5;
                currentLvl++;
                rage++;
                pDefense++;
            }
        }
    }

    public void AddExp(int expToGive)
    {
        currentExp += expToGive;
    }

    public void AddMoney(int monToGive)
    {
        money += monToGive;
    }

    public void HurtPlayer(float damageToGive)
    {
        if (!godgodmode)
        {
            float damage = damageToGive * BattleInter.Bi.mAttack / pDefense;
            if (AllAttacks.allAttacks.pAddon > 0) AllAttacks.allAttacks.pAddon -= damage;
            else pCurrentHealth -= damage;
            if (pCurrentHealth < 0) pCurrentHealth = 0;
        }
    }

    public void SetMaxHealth()
    {
        pCurrentHealth = pMaxHealth;
    }

    public void AddHealth(int a)
    {
        pCurrentHealth += a;
        if (pCurrentHealth > pMaxHealth) pCurrentHealth = pMaxHealth;
    }

    public void SetMaxPP(int which)
    {
        switch (which)
        {
            case 1:
                ppTwo = System.Convert.ToInt32(attacks[attackIndex[1], 3]);
                break;

            case 2:
                ppThree = System.Convert.ToInt32(attacks[attackIndex[2], 3]);
                break;

            case 3:
                ppFour = System.Convert.ToInt32(attacks[attackIndex[3], 3]);
                break;

            case 4:
                ppTwo = System.Convert.ToInt32(attacks[attackIndex[1], 3]);
                ppThree = System.Convert.ToInt32(attacks[attackIndex[2], 3]);
                ppFour = System.Convert.ToInt32(attacks[attackIndex[3], 3]);
                break;
        }
        
    }

    public void ChooseItem(int id, int forSpecialNeeds = 1)
    {
        switch (id)
        {
            case 0:
                AddHealth(40);
                break;

            case 1:
                SetMaxPP(forSpecialNeeds);
                break;

            case 3:
                BattleInter.Bi.invis = true;
                break;

            case 4:
                BattleInter.Bi.hat = true;
                break;

            case 8:
                rage++;
                pDefense += 1;
                break;

            case 9:
                rage++;
                pDefense += 2;
                break;

            case 10:
                currentAttack += 1;
                pDefense += 3;
                rage++;
                break;

            default:
                break;
        }
    }
    
    public void ChangeAttack(int which)
    {
        switch (currentLvl)
        {
            case 9:
                attackIndex[which] = 4 + which;
                break;

            case 10:
                attackIndex[which] = 8 + which;
                break;
        }
        SetMaxPP(which);
    }

    public void SetBasicStats()
    {
        GameData data = LoadingGame();
        
        pMaxHealth = data.maxHealth;
        pCurrentHealth = data.currentHealth;
        money = data.money;
        rage = data.rage;
        pDefense = data.defense;
        currentAttack = data.currentAttack;
        currentLvl = data.currentLvl;
        currentExp = data.currentExp;
        curEffect = data.curEffect;

        attackIndex[0] = data.attackIndex[0];
        attackIndex[1] = data.attackIndex[1];
        attackIndex[2] = data.attackIndex[2];
        attackIndex[3] = data.attackIndex[3];
        itemIndex.Clear();
        if (data.itemIndex[0] != -1) for (int i = 0; i < data.itemIndex.Length; i++) itemIndex.Add(data.itemIndex[i]);

        lastSortingLayer = data.lastSortingLayer;
        PControl.pControl.startPoint = data.startPoint;
        LoadNewArea.lastLvlLoaded = data.lastLvlLoaded;
        ifLoading = true;
        startPointForLoading = new Vector3(data.position[0], data.position[1], data.position[2]);
    }

    public GameData LoadingGame()
    {
        //string path = Application.persistentDataPath + "/gamedata.meh";
        string path = Application.dataPath + "/gamedata.meh";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file is not found.");
            return null;
        }
    }
}

