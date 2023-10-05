using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {
    public static DialogHolder DH;

    public string[] dialog;
    //если дело касается предмета, надо делать одну дополнительную строчку,
    //в которой потом будет прописано, был ли взят предмет, либо инвентарь оказался заполнен
    public Sprite icon;
    public string nameOfScript;
    public int indexOfItemToGive;
    public bool[] dir = { false, false, false, false }; //верх, низ, право, лево (как должен быть повернут чел)
    public bool doingThings = false;

    string whatPositionNeed;
    bool allow = false;
    bool wait, ifDoThng = false;
    float timeToWait;
    PStats pp;

    Dancefloor danceCheck;
    DomofonControl domCheck;
    string lastLvlLoaded;

	// Use this for initialization
	void Start () {
        DH = this;
        lastLvlLoaded = LoadNewArea.lastLvlLoaded;
        danceCheck = FindObjectOfType<Dancefloor>();
        domCheck = FindObjectOfType<DomofonControl>();
        if (dir[0] & dir[1] & dir[2] & dir[3]) whatPositionNeed = "any";
        else if (dir[0]) whatPositionNeed = "up";
        else if (dir[1]) whatPositionNeed = "down";
        else if (dir[2]) whatPositionNeed = "right";
        else if (dir[3]) whatPositionNeed = "left";
    }
	
	// Update is called once per frame
	void Update () {
        if (allow)
        {
            if (!InfoControl.info.iActive && !ShopControl.thing.shopActive && !Check())
            {
                if (wait && Input.GetKeyUp(ControlControl.z))
                {
                    if (PControl.pControl.whatPositionNow == whatPositionNeed || whatPositionNeed == "any")
                    {
                        wait = false;
                        timeToWait = 0.2f;
                        InfoControl.info.youCanOpenI = false;
                        if (nameOfScript == "GiveItem")
                        {
                            if (PStats.pStats.itemIndex.Count == 20)
                            {
                                dialog[dialog.Length - 1] = "Ваш инвентарь заполнен. Вам не удалось взять предмет.";
                                doingThings = false;
                            }
                            else dialog[dialog.Length - 1] = "Вы получили предмет: " + System.Convert.ToString(PStats.pStats.items[indexOfItemToGive, 0]);
                        }
                        DialogController.theDC.ShowBox(dialog, icon);
                        if (doingThings) ifDoThng = true;
                    }
                }
                else
                {
                    if (!DialogController.theDC.dialogActive)
                    {
                        if (doingThings && ifDoThng)
                        {
                            InfoControl.info.youCanOpenI = false;
                            StartThings();
                        }
                        timeToWait -= Time.deltaTime;
                        if (timeToWait < 0f) wait = true;
                    }
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            allow = true;
            wait = true;
            pp = other.GetComponent<PStats>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            allow = false;
            wait = false;
        }
    }

    void StartThings()
    {
        ifDoThng = false;
        switch (nameOfScript)
        {
            case "ShopControl":
                ShopControl.thing.DoTheThing();
                break;

            case "Saving":
                Saving.thing.DoTheThing(pp, GameObject.Find("ShopManager").GetComponent<ShopControl>());
                break;

            case "DomofonControl":
                DomofonControl.thing.DoTheThing();
                break;

            case "Dancefloor":
                Dancefloor.thing.DoTheThing();
                break;

            case "GiveItem":
                PStats.pStats.itemIndex.Add(indexOfItemToGive);
                break;
        }
    }

    bool Check()
    {
        if (lastLvlLoaded == "Club")
        {
            if (danceCheck != null)
            {
                if (Dancefloor.thing.danceActive) return true;
                else return false;
            }
            else return false;
        }
        else if (lastLvlLoaded == "Scene1")
        {
            if (domCheck != null)
            {
                if (DomofonControl.thing.domActive) return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }
}
