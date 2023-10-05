using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllAttacks : MonoBehaviour {
    
    public static AllAttacks allAttacks;

    int idTwo, idThree, idFour;
    int weakToMag, weakToPhys;

    public Text logOne, logTwo;

    int index, broccoliCounter, burnCounter, critCounter;
    public int pRageChanger;
    public float pAddon, pAttackChanger;
    public bool[] nameUsed = { false, false, false, false, false };
    public GameObject[] nameUsedBlock;

    // Use this for initialization
    void Start () {
        allAttacks = this;
    }

    public void UpdateWeaks()
    {
        weakToMag = System.Convert.ToInt32(BattleInter.Bi.enemies[BattleInter.Bi.justEnemyID, 7]);
        weakToPhys = System.Convert.ToInt32(BattleInter.Bi.enemies[BattleInter.Bi.justEnemyID, 8]);
    }

    public void ChooseAttack(int id)
    {
        switch (id)
        {
            case 0:
                PunchIV();
                break;

            case 1:
                FocusedPunch();
                break;

            case 2:
                BlackberrySphere();
                break;

            case 3:
                RadioactiveAsh();
                break;

            case 4:
                PunchV();
                break;

            case 5:
                UshiroGeri();
                break;

            case 6:
                PineLightning();
                break;

            case 7:
                Heatwave();
                break;

            case 8:
                PunchV();
                break;

            case 9:
                Uppercut();
                break;

            case 10:
                InkBeam();
                break;

            case 11:
                Broccoli();
                break;

            case 12:
                Varahiil();
                break;

            case 13:
                Iegudiil();
                break;

            case 14:
                Rafail();
                break;

            case 15:
                Uriil();
                break;

            case 16:
                Selafiil();
                break;
        }
        if (BattleInter.Bi.curEffect == "яд")
        {
            if (BattleInter.Bi.health > 0)
            {
                BattleInter.Bi.health -= (int)((float)BattleInter.Bi.health / 100 * 5);
                MessageSender(-1, "");
            }
        }
        else if (BattleInter.Bi.curEffect == "огонь")
        {
            if (burnCounter > 0)
            {
                if (BattleInter.Bi.health > 0)
                {
                    BattleInter.Bi.health -= (int)((float)BattleInter.Bi.health / 100 * 15);
                    MessageSender(-2, "");
                    burnCounter--;
                }
            }
            else BattleInter.Bi.curEffect = "";
        }

        if (BattleInter.Bi.broccoli)
        {
            if (broccoliCounter == 3)
            {
                BattleInter.Bi.broccoli = false;
                weakToPhys = System.Convert.ToInt32(BattleInter.Bi.enemies[BattleInter.Bi.justEnemyID, 8]);
            }
            else broccoliCounter++;
        }

        if (BattleInter.Bi.health < 0) BattleInter.Bi.health = 0;
    }

    void MessageSender(int id, string ah, int damage = 0, bool nameFail = false)
    {
        if (id == 3 || id == 7 || id == 11)
        {
            switch (id)
            {
                case 3:
                    if (BattleInter.Bi.mDefense < 2) ah += "Защита монстра на минимуме.";
                    else if (BattleInter.Bi.curEffect == "яд") ah += "Защита понижена. Монстр отравлен.";
                    else if (BattleInter.Bi.curEffect == "огонь") ah += "Защита понижена. Монстр не может быть отравлен, пока горит.";
                    break;

                case 7:
                    ah += "Скорость и сила противника понижены.";
                    break;

                case 11:
                    ah += "Монстр был превращен в брокколи.";
                    break;
            }
        }
        else if (id > 11 && id <= 16)
        {
            if (!nameFail)
            {
                switch (id)
                {
                    case 14:
                        ah += "Вы восстановили свое здоровье.";
                        break;

                    case 16:
                        ah += "Вокруг вас появился щит, защищающий от атак противника.";
                        break;

                    default:
                        ah += "Монстру был нанесен урон в " + damage + " единиц.";
                        break;
                }
            }
        }
        else if (id == -1) ah += "Монстр был отравлен и потерял очки здоровья.";
        else if (id == -2) ah += "Монстр горит и теряет очки здоровья.";
        else ah += "Монстру был нанесен урон в " + damage + " единиц.";

        if (logOne.text == "") logOne.text = ah;
        else if (logTwo.text == "") logTwo.text = ah;
        else
        {
            logOne.text = logTwo.text;
            logTwo.text = ah;
        }
    }

    void PunchIV()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = (float)(Random.Range(2f, 4f) * (PStats.pStats.currentAttack + pAttackChanger));
        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        MessageSender(0, output, (int)kek);
    }

    void FocusedPunch()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = (float)(Random.Range(2f, 4f) * (PStats.pStats.currentAttack + pAttackChanger) * 23 / BattleInter.Bi.mDefense);
        if (18 - (PStats.pStats.rage + pRageChanger + 1) >= 1) pRageChanger += 1;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToPhys == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToPhys == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppTwo -= 1;
        MessageSender(1, output, (int)kek);
    }

    void BlackberrySphere()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = Random.Range(2f, 4f) * 4 * 24 / BattleInter.Bi.mDefense;
        if (BattleInter.Bi.mDefense - 0.6 > 1) BattleInter.Bi.mDefense -= 0.6f;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToMag == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToMag == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppThree -= 1;
        MessageSender(2, output, (int)kek);
    }

    void RadioactiveAsh()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = 3;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        if (BattleInter.Bi.mDefense - kek > 1) BattleInter.Bi.mDefense -= kek;
        if (BattleInter.Bi.curEffect != "огонь") BattleInter.Bi.curEffect = "яд";

        if (!PStats.pStats.godgodmode) PStats.pStats.ppFour -= 1;
        MessageSender(3, output);
    }

    void PunchV()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = (float)(Random.Range(3f, 4f) * (PStats.pStats.currentAttack + pAttackChanger));
        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        MessageSender(4, output, (int)kek);
    }

    void PineLightning()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = Random.Range(2f, 3f) * 4.5f * 24 / BattleInter.Bi.mDefense;
        if (PStats.pStats.pCurrentHealth < PStats.pStats.pMaxHealth) PStats.pStats.pCurrentHealth += (int)(PStats.pStats.pCurrentHealth / 100 * 5);
        if (PStats.pStats.pCurrentHealth > PStats.pStats.pMaxHealth) PStats.pStats.pCurrentHealth = PStats.pStats.pMaxHealth;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToMag == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToMag == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppThree -= 1;
        MessageSender(5, output, (int)kek);
    }

    void UshiroGeri()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = (float)(Random.Range(2f, 4f) * (PStats.pStats.currentAttack + pAttackChanger) * 23 / BattleInter.Bi.mDefense + (BattleInter.Bi.mDefense + BattleInter.Bi.lvlEnemy));

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToPhys == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToPhys == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 2;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppTwo -= 1;
        MessageSender(6, output, (int)kek);
    }

    void Heatwave() 
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = 0.05f;
        if (BattleInter.Bi.mSpeed > 0.02) BattleInter.Bi.mSpeed *= 0.8f;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        if (BattleInter.Bi.mAttack - kek > 0.02f) BattleInter.Bi.mAttack -= kek;

        if (!PStats.pStats.godgodmode) PStats.pStats.ppFour -= 1;
        MessageSender(7, output);
    } //снижает скорость и атаку

    void InkBeam()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = Random.Range(2f, 3f) * 5 * 4;
        if (BattleInter.Bi.mDefense - 0.6 > 1) BattleInter.Bi.mDefense -= 0.6f;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToMag == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToMag == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 1.5f;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppThree -= 1;
        MessageSender(10, output, (int)kek);
    }

    void Uppercut()
    {
        string output = "";
        int gmax;
        if (critCounter > 0)
        {
            gmax = 2;
            critCounter--;
        }
        else gmax = (int)(18 - (PStats.pStats.rage + pRageChanger));
        int rnd = Random.Range(1, gmax); //крит
        float kek = Random.Range(2f, 3f) * 5 * 4;
        if (BattleInter.Bi.mDefense - 0.6 > 1) BattleInter.Bi.mDefense -= 0.6f;

        if (rnd < 3)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }

        if (weakToPhys == 1)
        {
            output += "Атака супер-эффективна. ";
            kek *= 2;
        }
        else if (weakToPhys == -1)
        {
            output += "Атака не эффективна. ";
            kek /= 1.5f;
        }
        BattleInter.Bi.health -= (int)kek;
        if (!PStats.pStats.godgodmode) PStats.pStats.ppTwo -= 1;
        MessageSender(9, output, (int)kek);
    }

    void Broccoli()
    {
        string output = "";
        BattleInter.Bi.broccoli = true;
        broccoliCounter = 0;
        weakToPhys = 1;

        if (!PStats.pStats.godgodmode) PStats.pStats.ppFour -= 1;
        MessageSender(11, output);
    }

    void Varahiil()
    {
        nameUsed[0] = true;
        nameUsedBlock[0].SetActive(true);
        string output = "";
        bool nameFail = false;
        critCounter = 2;
        float kek = (float)(Random.Range(2f, 3f) * (PStats.pStats.currentAttack + pAttackChanger) * 7);
        if (GameInter.inter.result == 2)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        else if (GameInter.inter.result == 0)
        {
            kek = 0;
            output = "Атака неудачна.";
            nameFail = true;
            critCounter = 0;
        }
        BattleInter.Bi.health -= (int)kek;

        MessageSender(12, output, (int)kek, nameFail);
    }

    void Iegudiil()
    {
        nameUsed[1] = true;
        nameUsedBlock[1].SetActive(true);
        string output = "";
        bool nameFail = false;
        pAttackChanger += 1.5f;
        float kek = (float)(Random.Range(2f, 3f) * (PStats.pStats.currentAttack + pAttackChanger) * 7);
        if (GameInter.inter.result == 2)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        else if (GameInter.inter.result == 0)
        {
            kek = 0;
            output = "Атака неудачна.";
            nameFail = true;
        }
        BattleInter.Bi.health -= (int)kek;

        MessageSender(13, output, (int)kek, nameFail);
    }

    void Rafail()
    {
        nameUsed[2] = true;
        nameUsedBlock[2].SetActive(true);
        string output = "";
        bool nameFail = false;
        int kek;
        kek = (int)(PStats.pStats.pCurrentHealth / 100 * 60);

        if (GameInter.inter.result == 2)
        {
            output = "Крит-удар. ";
            kek += 20;
        }
        else if (GameInter.inter.result == 0)
        {
            output = "Атака неудачна.";
            kek = 0;
            nameFail = true;
        }
        if (PStats.pStats.pCurrentHealth + kek > PStats.pStats.pMaxHealth) PStats.pStats.pCurrentHealth = PStats.pStats.pMaxHealth;
        else PStats.pStats.pCurrentHealth += kek;
        MessageSender(14, output, 0, nameFail);
    }

    void Uriil()
    {
        nameUsed[3] = true;
        nameUsedBlock[3].SetActive(true);
        string output = "", effectSaver;
        bool nameFail = false;
        effectSaver = BattleInter.Bi.curEffect;
        BattleInter.Bi.curEffect = "огонь";
        burnCounter = Random.Range(2, 4);
        float kek = (float)(Random.Range(2f, 3f) * (PStats.pStats.currentAttack + pAttackChanger) * 9);
        if (GameInter.inter.result == 2)
        {
            output = "Крит-удар. ";
            kek *= 2;
        }
        else if (GameInter.inter.result == 0)
        {
            kek = 0;
            BattleInter.Bi.curEffect = effectSaver;
            output = "Атака неудачна.";
            nameFail = true;
        }
        BattleInter.Bi.health -= (int)kek;

        MessageSender(15, output, (int)kek, nameFail);
    }

    void Selafiil()
    {
        nameUsed[4] = true;
        nameUsedBlock[4].SetActive(true);
        string output = "";
        bool nameFail = false;
        pAddon = 50;
        if (GameInter.inter.result == 2)
        {
            pAddon += 25;
            output = "Крит-удар. ";
        }
        else if (GameInter.inter.result == 0)
        {
            pAddon = 0;
            output = "Атака неудачна.";
            nameFail = true;
        }

        MessageSender(16, output, 0, nameFail);
    }
}
