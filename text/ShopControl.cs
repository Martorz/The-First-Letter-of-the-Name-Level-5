using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ShopControl : MonoBehaviour {

    public GameObject shop, chooserA, chooserB, arrowA, arrowB;
    public static ShopControl thing;
    public Text money;
    public GameObject[] items, sellItems, actionBoxes;
    public List<int> sellItemID;
    public Sprite itemAA, itemAB, itemBA, itemBB, exitA, exitB;
    public bool shopActive = false;
    public int bananaStage = 1;

    int currentItemID, currentColumnID, currentLine;
    List<int> xd = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    bool action;
    string[] say = {  "Очень хороший пуддинг. Пролежал здесь всего 100 лет. Еще и со скидкой, вот это да!"  ,
        "Самая лучшая Лакрица. Я эту гадость не пью, но вам советую."  ,
        "Это же новая версия Банана! В ней есть множество новых функций. Не знаю, каких, но они все вам нужны.",
        "Не знаю, что в этой банке, но, наверное, пустота.",
        "Самая модная шляпа, которая осталась у нас в продаже.",
        "Ето же Яблоко!",
        "Ето же Яблоко Х!",
        "Ето же Ананас!",
        "Броня высокого класса. Увеличивает ярость на единицу и немного повышает атаку.",
        "Броня высокого класса. Увеличивает ярость на единицу и немного повышает атаку.",
        "Броня высшего класса. Увеличивает ярость и атаку на единицу и повышает атаку." };

    // Use this for initialization
    void Start () {
        thing = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (shopActive)
        {
            if (Input.GetKeyUp(ControlControl.right) && currentColumnID == 0)
            {
                currentColumnID = 1;
                if (!actionBoxes[0].activeSelf)
                {
                    action = false;
                    actionBoxes[1].GetComponent<Image>().sprite = exitB;
                }
                else
                {
                    action = true;
                    actionBoxes[0].GetComponent<Image>().sprite = itemAB;
                }
            }
            else if (Input.GetKeyUp(ControlControl.left) && currentColumnID == 1)
            {
                if (PStats.pStats.itemIndex.Count != 0)
                {
                    currentColumnID = 0;
                    chooserA.SetActive(true);
                    chooserB.SetActive(false);
                    actionBoxes[0].GetComponent<Image>().sprite = itemAA;
                    actionBoxes[1].GetComponent<Image>().sprite = exitA;
                    if (PStats.pStats.itemIndex.Count <= currentItemID)
                    {
                        currentItemID = PStats.pStats.itemIndex.Count - 1;
                        currentLine = currentItemID;
                    }
                    else if (xd.IndexOf(currentItemID) != currentLine)
                    {
                        if (xd.IndexOf(currentItemID) != -1) currentLine = xd.IndexOf(currentItemID);
                        else
                        {
                            currentLine = 0;
                            xd[0] = currentItemID;
                            //items[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]);
                            if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                                items[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]);
                            else
                                items[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]);
                            for (int i = 1; i < 10; i++)
                            {
                                if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID + i], 5]) == -1)
                                    items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID + i], 0]);
                                else
                                    items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID + i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID + i], 5]);
                                xd[i] = xd[i - 1] + 1;
                            }
                            arrowA.SetActive(true);
                        }
                    }
                    chooserA.transform.localPosition = new Vector2(0, items[currentLine].transform.localPosition.y);
                    DialogController.theDC.ShowTextText(System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]));

                    if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                        actionBoxes[0].SetActive(false);
                    else if (!actionBoxes[0].activeSelf) actionBoxes[0].SetActive(true);
                }
            }
            else if (Input.GetKeyUp(ControlControl.right) && currentColumnID == 1)
            {
                currentColumnID = 2;
                chooserA.SetActive(false);
                chooserB.SetActive(true);
                actionBoxes[0].SetActive(true);
                actionBoxes[0].GetComponent<Image>().sprite = itemBA;
                actionBoxes[1].GetComponent<Image>().sprite = exitA;
                if (sellItemID.Count <= currentItemID)
                {
                    currentItemID = sellItemID.Count - 1;
                }
                currentLine = currentItemID;
                chooserB.transform.localPosition = new Vector2(0, sellItems[currentLine].transform.localPosition.y);
                DialogController.theDC.ShowTextText(say[sellItemID[currentItemID]]);
            }
            else if (Input.GetKeyUp(ControlControl.left) && currentColumnID == 2)
            {
                action = true;
                currentColumnID = 1;
                actionBoxes[0].GetComponent<Image>().sprite = itemBB;
            }

            if (Input.GetKeyUp(ControlControl.z) && currentColumnID == 1)
            {
                if (action)
                {
                    if (actionBoxes[0].GetComponent<Image>().sprite == itemAB && PStats.pStats.itemIndex.Count != 0)
                    {
                        PStats.pStats.money += System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]);
                        PStats.pStats.itemIndex.RemoveAt(currentItemID);
                        UpdateHero();
                        if (PStats.pStats.itemIndex.Count != 0) DialogController.theDC.ShowTextText(System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]));
                        else DialogController.theDC.ShowTextText(" ");
                    } //продать 
                    else if ((actionBoxes[0].GetComponent<Image>().sprite == itemBB) && (PStats.pStats.money - System.Convert.ToInt32(PStats.pStats.items[sellItemID[currentItemID], 1]) >= 0) && (PStats.pStats.itemIndex.Count < 20))
                    {
                        PStats.pStats.money -= System.Convert.ToInt32(PStats.pStats.items[sellItemID[currentItemID], 1]);
                        if (sellItemID[currentItemID] == 2 || sellItemID[currentItemID] == 5 || sellItemID[currentItemID] == 6 || sellItemID[currentItemID] == 7)
                        {
                            switch (sellItemID[currentItemID])
                            {
                                case 2:
                                    PStats.pStats.itemIndex.Add(sellItemID[currentItemID]);
                                    bananaStage = 2;
                                    break;

                                case 5:
                                    PStats.pStats.itemIndex[PStats.pStats.itemIndex.IndexOf(2)] = 5;
                                    bananaStage = 3;
                                    break;

                                case 6:
                                    PStats.pStats.itemIndex[PStats.pStats.itemIndex.IndexOf(5)] = 6;
                                    bananaStage = 4;
                                    break;

                                case 7:
                                    PStats.pStats.itemIndex[PStats.pStats.itemIndex.IndexOf(6)] = 7;
                                    bananaStage = 5;
                                    break;
                            }
                        }
                        else PStats.pStats.itemIndex.Add(sellItemID[currentItemID]);
                        if (!System.Convert.ToBoolean(PStats.pStats.items[sellItemID[currentItemID], 3]) || sellItemID[currentItemID] == 8 || sellItemID[currentItemID] == 9 || sellItemID[currentItemID] == 10) sellItemID.RemoveAt(currentItemID);
                        UpdateSeller();
                        if (sellItemID.Count != 0) DialogController.theDC.ShowTextText(System.Convert.ToString(say[sellItemID[currentItemID]]));
                        else DialogController.theDC.ShowTextText(" ");
                    } //купить
                }
                else
                {
                    actionBoxes[1].GetComponent<Image>().sprite = exitA;
                    shop.SetActive(false);
                    DialogController.theDC.dActive = false;
                    shopActive = false;
                    InfoControl.info.youCanOpenI = true;
                }
            }

            if (Input.GetKeyUp(ControlControl.down))
            {
                if (currentColumnID == 1)
                {
                    if (action)
                    {
                        action = false;
                        actionBoxes[1].GetComponent<Image>().sprite = exitB;
                        if (actionBoxes[0].GetComponent<Image>().sprite == itemBB)
                            actionBoxes[0].GetComponent<Image>().sprite = itemBA;
                        else
                            actionBoxes[0].GetComponent<Image>().sprite = itemAA;
                    }
                }
                else if (currentColumnID == 2)
                {
                    if (sellItemID.Count - 1 != currentItemID)
                    {
                        currentItemID++;
                        DialogController.theDC.ShowTextText(say[sellItemID[currentItemID]]);
                        if (currentItemID > 9 && currentLine == 9)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                sellItems[i].GetComponent<Text>().text = sellItems[i + 1].GetComponent<Text>().text;
                            }
                            sellItems[9].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[sellItemID[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[sellItemID[currentItemID], 1]);
                        }
                        else currentLine++;
                        chooserB.transform.localPosition = new Vector2(0, sellItems[currentLine].transform.localPosition.y);
                    }
                }
                else if (currentColumnID == 0)
                {
                    if (PStats.pStats.itemIndex.Count - 1 != currentItemID)
                    {
                        currentItemID++;
                        DialogController.theDC.ShowTextText(System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]));
                        if (currentItemID > 9 && currentLine == 9)
                        {
                            int temp = xd[9] + 1;
                            for (int i = 0; i < 9; i++)
                            {
                                items[i].GetComponent<Text>().text = items[i + 1].GetComponent<Text>().text;
                                xd[i] = xd[i + 1];
                            }
                            if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                                items[9].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]);
                            else
                                items[9].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]);
                            xd[9] = temp;
                        }
                        else currentLine++;
                        chooserA.transform.localPosition = new Vector2(0, items[currentLine].transform.localPosition.y);
                        if (PStats.pStats.itemIndex.Count > 10 && xd[9] == PStats.pStats.itemIndex.Count - 1) arrowA.SetActive(false);

                        if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                            actionBoxes[0].SetActive(false);
                        else if (!actionBoxes[0].activeSelf) actionBoxes[0].SetActive(true);
                    }
                }
            }
            else if (Input.GetKeyUp(ControlControl.up))
            {
                if (currentColumnID == 1)
                {
                    if (!action && actionBoxes[0].activeSelf)
                    {
                        action = true;
                        actionBoxes[1].GetComponent<Image>().sprite = exitA;
                        if (actionBoxes[0].GetComponent<Image>().sprite == itemAA)
                            actionBoxes[0].GetComponent<Image>().sprite = itemAB;
                        else
                            actionBoxes[0].GetComponent<Image>().sprite = itemBB;
                    }
                }
                else if (currentColumnID == 2)
                {
                    if (0 != currentItemID)
                    {
                        currentItemID--;
                        DialogController.theDC.ShowTextText(say[sellItemID[currentItemID]]);
                        if (currentItemID >= 0 && currentLine == 0)
                        {
                            
                            for (int i = 9; i > 0; i--)
                            {
                                sellItems[i].GetComponent<Text>().text = sellItems[i - 1].GetComponent<Text>().text;
                                
                            }
                            sellItems[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[sellItemID[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[sellItemID[currentItemID], 1]);
                        }
                        else currentLine--;
                        chooserB.transform.localPosition = new Vector2(0, sellItems[currentLine].transform.localPosition.y);
                    }
                }
                else if (currentColumnID == 0)
                {
                    if (0 != currentItemID)
                    {
                        currentItemID--;
                        DialogController.theDC.ShowTextText(System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]));
                        if (currentItemID >= 0 && currentLine == 0)
                        {
                            int temp = xd[0] - 1;
                            for (int i = 9; i > 0; i--)
                            {
                                items[i].GetComponent<Text>().text = items[i - 1].GetComponent<Text>().text;
                                xd[i] = xd[i - 1];
                            }
                            xd[0] = temp;
                            if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                                items[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]);
                            else
                                items[0].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]);
                        }
                        else currentLine--;
                        chooserA.transform.localPosition = new Vector2(0, items[currentLine].transform.localPosition.y);

                        if (arrowA.activeSelf != true && PStats.pStats.itemIndex.Count > 10 && xd[9] != PStats.pStats.itemIndex.Count - 1) arrowA.SetActive(true);

                        if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
                            actionBoxes[0].SetActive(false);
                        else if (!actionBoxes[0].activeSelf) actionBoxes[0].SetActive(true);
                    }
                }
            }
        }
	}

    public void DoTheThing()
    {
        shopActive = true;
        shop.SetActive(true);
        DialogController.theDC.dActive = true;
        currentItemID = 0;
        currentLine = 0;
        xd = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        actionBoxes[0].SetActive(true);
        chooserA.transform.localPosition = new Vector2(0, items[0].transform.localPosition.y);
        chooserB.transform.localPosition = new Vector2(0, sellItems[0].transform.localPosition.y);
        if (!sellItemID.Contains(2) && !sellItemID.Contains(5) && !sellItemID.Contains(6) && !sellItemID.Contains(7))
        {
            if (bananaStage == 1) sellItemID.Add(2);
            else if (bananaStage == 2) sellItemID.Add(5);
            else if (bananaStage == 3) sellItemID.Add(6);
            else if (bananaStage == 4) sellItemID.Add(7);
        }

        if (PStats.pStats.itemIndex.Count != 0)
        {
            currentColumnID = 0;
            chooserA.SetActive(true);
            chooserB.SetActive(false);
            actionBoxes[0].GetComponent<Image>().sprite = itemAA;
            DialogController.theDC.ShowTextText(System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 2]));
        }
        else
        {
            currentColumnID = 2;
            chooserB.SetActive(true);
            chooserA.SetActive(false);
            actionBoxes[0].GetComponent<Image>().sprite = itemBA;
            DialogController.theDC.ShowTextText(say[sellItemID[currentItemID]]);
        }

        LoadShop(PStats.pStats.itemIndex.Count);

    }

    void LoadShop(int itemCount)
    {
        money.text = "" + PStats.pStats.money;

        for (int i = 0; i < 10; i++)
        {
            items[i].GetComponent<Text>().text = "";
            items[i].SetActive(false);
        }

        int temp;
        if (itemCount != 0)
        {

            if (itemCount > 10)
            {
                temp = 10;
                arrowA.SetActive(true);
            }
            else temp = itemCount;

            int info1;
            string info2;

            for (int i = 0; i < temp; i++)
            {
                info1 = System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]); //название
                info2 = System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]); //цена продажи

                items[i].SetActive(true);
                if (info1 == -1)
                    items[i].GetComponent<Text>().text = "" + info2;
                else
                    items[i].GetComponent<Text>().text = "" + info2 + ": " + System.Convert.ToString(info1);
            }

            if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1) actionBoxes[0].SetActive(false);
        }

        if (sellItemID.Count > 10) temp = 10;
        else temp = sellItemID.Count;

        for (int i = 0; i < temp; i++)
        {
            sellItems[i].SetActive(true);
            sellItems[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[sellItemID[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[sellItemID[i], 1]);
        }
        //arrowB.SetActive(true);
    }

    void UpdateHero()
    {
        money.text = "" + PStats.pStats.money;
        currentItemID = 0;
        currentLine = 0;
        xd = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 10; i++)
        {
            items[i].GetComponent<Text>().text = "";
            items[i].SetActive(false);
        }

        if (PStats.pStats.itemIndex.Count != 0)
        {
            chooserA.SetActive(true);
            chooserA.transform.localPosition = new Vector2(0, items[0].transform.localPosition.y);
            if (PStats.pStats.itemIndex.Count > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    items[i].SetActive(true);
                    if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]) == -1)
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]);
                    else
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]);
                }
                arrowA.SetActive(true);
            }
            else
            {
                for (int i = 0; i < PStats.pStats.itemIndex.Count; i++)
                {
                    items[i].SetActive(true);
                    if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]) == -1)
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]);
                    else
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]);
                }
                arrowA.SetActive(false);
            }
            if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[currentItemID], 5]) == -1)
            {
                actionBoxes[0].SetActive(false);
                action = false;
                actionBoxes[1].GetComponent<Image>().sprite = exitB;
            }
        }
        else chooserA.SetActive(false);
    }

    void UpdateSeller()
    {
        money.text = "" + PStats.pStats.money;
        currentItemID = 0;
        currentLine = 0;
        xd = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < 10; i++)
        {
            items[i].GetComponent<Text>().text = "";
            items[i].SetActive(false);
            sellItems[i].GetComponent<Text>().text = "";
            sellItems[i].SetActive(false);
        }

        if (PStats.pStats.itemIndex.Count > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    items[i].SetActive(true);
                    if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]) == -1)
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]);
                    else
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]);
                }
                arrowA.SetActive(true);
            }
        else
            {
                for (int i = 0; i < PStats.pStats.itemIndex.Count; i++)
                {
                    items[i].SetActive(true);
                    if (System.Convert.ToInt32(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]) == -1)
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]);
                    else
                        items[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[PStats.pStats.itemIndex[i], 5]);
                }
                arrowA.SetActive(false);
            }

        for (int i = 0; i < sellItemID.Count; i++)
        {
            sellItems[i].SetActive(true);
            sellItems[i].GetComponent<Text>().text = "" + System.Convert.ToString(PStats.pStats.items[sellItemID[i], 0]) + ": " + System.Convert.ToString(PStats.pStats.items[sellItemID[i], 1]);
        }
        arrowB.SetActive(false);

        chooserB.transform.localPosition = new Vector2(0, sellItems[0].transform.localPosition.y);
    }

    public void RestartShop()
    {
        ShopData data = LoadingGame();

        bananaStage = data.bananaStage;
        sellItemID.Clear();
        for (int i = 0; i < data.itemIndex.Length; i++) sellItemID.Add(data.itemIndex[i]);
    }

    ShopData LoadingGame()
    {
        //string path = Application.persistentDataPath + "/shopdata.meh";
        string path = Application.dataPath + "/shopdata.meh";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ShopData data = formatter.Deserialize(stream) as ShopData;
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
