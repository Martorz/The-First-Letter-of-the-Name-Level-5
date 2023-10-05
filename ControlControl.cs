using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ControlControl : MonoBehaviour {

    public static ControlControl cc;
    public static KeyCode z, x, c, up, down, left, right;
    public bool isMM;
    public bool currentControl; //false - wasd, true - classic
    public GameObject startInformation, continueBlock;
    public Sprite type1, type2;

    // Use this for initialization
    void Start () {
        cc = this;
        ControlData data = LoadingGame();
        if (data.currentControl) SetControl(0);
        else SetControl(1);
        if (currentControl) startInformation.GetComponent<SpriteRenderer>().sprite = type1;
        else startInformation.GetComponent<SpriteRenderer>().sprite = type2;

        GameData data1 = LoadingGame2();
        if (!data1.newGame) continueBlock.SetActive(false);
    }

    public void SetControl(int type)
    {
        if (type == 0) //classic
        {
            Debug.Log("Changed to classic.");
            z = KeyCode.Z;
            x = KeyCode.X;
            c = KeyCode.C;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            currentControl = true;
        }
        else if (type == 1) //wasd
        {
            Debug.Log("Changed to wasd.");
            z = KeyCode.Comma;
            x = KeyCode.Period;
            c = KeyCode.Slash;
            up = KeyCode.W;
            down = KeyCode.S;
            left = KeyCode.A;
            right = KeyCode.D;
            currentControl = false;
        }
        //ControlData data = LoadingGame();
        SaveSettings(this);//, data.wasCutscene);
    }

    public void SaveSettings(ControlControl cc)//, bool wC)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/controldata.meh";
        FileStream stream = new FileStream(path, FileMode.Create);
        ControlData data = new ControlData(0, cc);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    ControlData LoadingGame()
    {
        string path = Application.dataPath + "/controldata.meh";

        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            ControlData data = formatter.Deserialize(stream) as ControlData;
            stream.Close();
            return data;
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            ControlData data = new ControlData(1);
            formatter.Serialize(stream, data);
            stream.Close();
            return LoadingGame();
        }
    }

    GameData LoadingGame2()
    {
        //string path = Application.persistentDataPath + "/gamedata.meh";
        string path = Application.dataPath + "/gamedata.meh";

        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(1);
            formatter.Serialize(stream, data);
            stream.Close();
            return LoadingGame2();
        }
    }

    public bool CheckIfNewGame()
    {
        GameData data = LoadingGame2();
        return data.newGame;
    }
}
