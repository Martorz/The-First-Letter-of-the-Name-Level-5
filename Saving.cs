using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Saving : MonoBehaviour {

    public static Saving thing;

    private void Start()
    {
        thing = this;
    }

    public void DoTheThing(PStats ps, ShopControl sc)
    {
        PStats.pStats.SetMaxHealth();
        //PStats.pStats.SetMaxPP(4); восстанавливается в геймдате
        InfoControl.info.youCanOpenI = true;

        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/gamedata.meh";
        string path = Application.dataPath + "/gamedata.meh";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(0, ps);
        formatter.Serialize(stream, data);
        stream.Close();

        //path = Application.persistentDataPath + "/shopdata.meh";
        path = Application.dataPath + "/shopdata.meh";
        stream = new FileStream(path, FileMode.Create);
        ShopData data1 = new ShopData(0, sc);
        formatter.Serialize(stream, data1);
        stream.Close();
    }
    
    public void NewGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/gamedata.meh";
        string path = Application.dataPath + "/gamedata.meh";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(1);
        formatter.Serialize(stream, data);
        stream.Close();

        //path = Application.persistentDataPath + "/shopdata.meh";
        path = Application.dataPath + "/shopdata.meh";
        stream = new FileStream(path, FileMode.Create);
        ShopData data1 = new ShopData(1);
        formatter.Serialize(stream, data1);
        stream.Close();
    }
}
