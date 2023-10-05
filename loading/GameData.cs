[System.Serializable]

public class GameData {

    public int maxHealth, money, rage, currentLvl, currentExp;
    public float[] position;
    public float currentHealth, defense;
    public double currentAttack;
    public bool newGame;
    public string lastSortingLayer, curEffect, startPoint, lastLvlLoaded;
    public int[] attackIndex;
    public int[] itemIndex;
    public GameData(int type, PStats ps = null)
    {
        switch (type)
        {
            case 0:
                newGame = false;

                position = new float[3];
                position[0] = ps.transform.position.x;
                position[1] = ps.transform.position.y;
                position[2] = ps.transform.position.z;

                lastSortingLayer = ps.lastSortingLayer;
                maxHealth = ps.pMaxHealth;
                currentHealth = ps.pCurrentHealth;
                money = ps.money;
                rage = ps.rage;
                defense = ps.pDefense;
                currentAttack = ps.currentAttack;
                currentLvl = ps.currentLvl;
                currentExp = ps.currentExp;
                curEffect = ps.curEffect;
                attackIndex = new int[4];
                attackIndex[0] = ps.attackIndex[0];
                attackIndex[1] = ps.attackIndex[1];
                attackIndex[2] = ps.attackIndex[2];
                attackIndex[3] = ps.attackIndex[3];
                PStats.pStats.SetMaxPP(4);
                if (ps.itemIndex.Count != 0)
                {
                    itemIndex = new int[ps.itemIndex.Count];
                    for (int i = 0; i < ps.itemIndex.Count; i++) itemIndex[i] = ps.itemIndex[i];
                }
                else
                {
                    itemIndex = new int[1];
                    itemIndex[0] = -1;
                }
                startPoint = PControl.pControl.startPoint;
                lastLvlLoaded = LoadNewArea.lastLvlLoaded;
                break;

            case 1:
                newGame = true;

                position = new float[3];
                position[0] = 41.73f;
                position[1] = -48.1f;
                position[2] = 0;

                lastSortingLayer = "All";
                maxHealth = 100;
                currentHealth = 100;
                money = 20000;
                rage = 8;
                defense = 10;
                currentAttack = 4;
                currentLvl = 8;
                currentExp = 0;
                curEffect = "нет";
                attackIndex = new int[4];
                attackIndex[0] = 0;
                attackIndex[1] = 1;
                attackIndex[2] = 2;
                attackIndex[3] = 3;
                PStats.pStats.SetMaxPP(4);
                itemIndex = new int[1];
                itemIndex[0] = -1;
                startPoint = "Start";
                lastLvlLoaded = "Scene1";
                break;
        }
    }
}
