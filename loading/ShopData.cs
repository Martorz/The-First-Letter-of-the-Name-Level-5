[System.Serializable]

public class ShopData
{
    public int[] itemIndex;
    public int bananaStage;

    public ShopData(int type, ShopControl cc = null)
    {
        switch (type)
        {
            case 0:
                bananaStage = cc.bananaStage;
                itemIndex = new int[cc.sellItemID.Count];
                for (int i = 0; i < cc.sellItemID.Count; i++) itemIndex[i] = cc.sellItemID[i];
                break;

            case 1:
                bananaStage = 1;
                itemIndex = new int[7];
                itemIndex[0] = 0;
                itemIndex[1] = 1;
                itemIndex[2] = 3;
                itemIndex[3] = 4;
                itemIndex[4] = 8;
                itemIndex[5] = 9;
                itemIndex[6] = 10;
                break;
        }
    }
}

