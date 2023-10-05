using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    public GameObject console;
    public Text textext, textlog;
    public float NameGameMinS = 0.04f, NameGameMaxS = 0.2f, k = 16.5f;
    public float time1_1 = 0.1f, time1_2 = 1, time2 = 1.3f;
    public bool cutscene = true;
    public int attack = 0;
    public static Console cmd;

	// Use this for initialization
	void Start () {
        cmd = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!console.activeSelf && Input.GetKeyUp(KeyCode.Tab))
        {
            textlog.text = "";
            InfoControl.info.youCanOpenI = false;
            console.SetActive(true);
            DialogController.theDC.dActive = true;
        }
        else if (console.activeSelf && (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(ControlControl.x) || Input.GetKeyUp(KeyCode.Escape)))
        {
            InfoControl.info.youCanOpenI = true;
            DialogController.theDC.dActive = false;
            console.SetActive(false);
        }

        

        if (console.activeSelf)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                if (textext.text.Length > 0) textext.text = "" + textext.text.Remove(textext.text.Length - 1);
            }
            else if (Input.GetKeyUp(KeyCode.Insert))
            {
                Thing(textext.text);
            }
            else if (Input.anyKey && !Input.GetKey(KeyCode.Space))
            {
                textext.text += Input.inputString;
            }
        }
    }

    void Thing(string text)
    {
        switch (text)
        {
            case "help":
                textlog.text += "\ngodgodmode[on|off], set[enter_num], phonenumsong, cutscene[on|off]";
                break;

            case "godgodmodeon":
                textlog.text += "\nu r now the god^2";
                PStats.pStats.godgodmode = true;
                break;

            case "godgodmodeoff":
                textlog.text += "\nu r now just the god";
                PStats.pStats.godgodmode = false;
                break;

            case "cutsceneoff":
                textlog.text += "\ncutscene is turned off";
                cutscene = false;
                break;

            case "cutsceneon":
                textlog.text += "\ncutscene is turned on";
                cutscene = true;
                break;

            default:
                if (text.Substring(0, 3) == "set")
                {
                    attack = System.Convert.ToInt32(text.Substring(3));
                    textlog.text += "\nchanged to " + attack;
                }
                else
                {
                    switch (text.Substring(0, 6))
                    {
                        case "ngmins":
                            if (text.Contains("show")) textlog.text += "\n" + NameGameMinS;
                            else if (text.Contains("izm"))
                            {
                                NameGameMinS = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + NameGameMinS;
                            }
                            break;

                        case "ngmaxs":
                            if (text.Contains("show")) textlog.text += "\n" + NameGameMaxS;
                            else if (text.Contains("izm"))
                            {
                                NameGameMaxS = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + NameGameMaxS;
                            }
                            break;

                        case "koefff":
                            if (text.Contains("show")) textlog.text += "\n" + k;
                            else if (text.Contains("izm"))
                            {
                                k = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + k;
                            }
                            break;

                        case "time11":
                            if (text.Contains("show")) textlog.text += "\n" + time1_1;
                            else if (text.Contains("izm"))
                            {
                                time1_1 = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + time1_1;
                            }
                            break;

                        case "time12":
                            if (text.Contains("show")) textlog.text += "\n" + time1_2;
                            else if (text.Contains("izm"))
                            {
                                time1_2 = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + time1_2;
                            }
                            break;

                        case "time21":
                            if (text.Contains("show")) textlog.text += "\n" + time2;
                            else if (text.Contains("izm"))
                            {
                                time2 = (float)(System.Convert.ToDouble(text.Substring(9)));
                                textlog.text += "\nchanged to " + time2;
                            }
                            break;
                    }
                }
                break;
        }
    }
}
