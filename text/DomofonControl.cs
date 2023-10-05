using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DomofonControl : MonoBehaviour {

    public bool domActive;
    string input;
    public GameObject domBox, frame;
    public static DomofonControl thing;
    public Text text;
    public Animator transition;

    int position;
    float[,] framePosition = { { -53, 98.5f }, { 1, 98.5f }, { 54, 98.5f },
                               { -53, 60.5f }, { 1, 60.5f }, { 54, 60.5f },
                               { -53, 23.5f }, { 1, 23.5f }, { 54, 23.5f },
                               { -53, -12.5f }, { 1, -12.5f }, { 54, -12.5f }};

	// Use this for initialization
	void Start () {
        thing = this;
	}
	
	// Update is called once per frame
	void Update () {
            if (Input.GetKeyDown(ControlControl.x)) //выход
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    DialogController.theDC.dActive = false;
                    domBox.SetActive(false);
                    domActive = false;
                    InfoControl.info.youCanOpenI = true;
                }
            }

            if (Input.GetKeyDown(ControlControl.z))
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    switch (position)
                    {
                        case 10:
                            if (input == "413") text.text = "Fav date.";
                            else if (input == "17082001") text.text = "Author bday.";
                            else if (input == "21012001") text.text = "Author bday.";
                            else if (input == "69696969") text.text = "FUCK.";
                            else if (input == "30121922") text.text = "USSR YAY.";
                            else if (input == "26121991") text.text = "USSR NAY.";
                            else if (input == "21111997")
                            {
                                DialogController.theDC.dActive = false;
                                domBox.SetActive(false);
                                domActive = false;
                                PControl.pControl.startPoint = "HomeOut";
                                LoadNewArea.loadNA.LoadLvl("Home", transition);
                                InfoControl.info.youCanOpenI = true;
                            }
                            break;

                        case 11:
                            if (input.Length > 11) input = input.Substring(1);
                            input += "0";
                            text.text = "" + input;
                            break;

                        case 12:
                            input = "";
                            text.text = "";
                            break;

                        default:
                            if (input.Length > 11) input = input.Substring(1);
                            input += System.Convert.ToString(position);
                            text.text = "" + input;
                            break;
                    }
                }
            }

            if (Input.GetKeyUp(ControlControl.right))
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    if (position != 12)
                    {
                        position++;
                        frame.transform.localPosition = new Vector2(framePosition[position - 1, 0], framePosition[position - 1, 1]);
                    }
                }
            }
            else if (Input.GetKeyUp(ControlControl.left))
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    if (position != 1)
                    {
                        position--;
                        frame.transform.localPosition = new Vector2(framePosition[position - 1, 0], framePosition[position - 1, 1]);
                    }
                }
            }
            else if (Input.GetKeyUp(ControlControl.up))
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    if (position - 3 > 0)
                    {
                        position -= 3;
                        frame.transform.localPosition = new Vector2(framePosition[position - 1, 0], framePosition[position - 1, 1]);
                    }
                }
            }
            else if (Input.GetKeyUp(ControlControl.down))
            {
                if (domActive && !InfoControl.info.iActive)
                {
                    if (position + 3 < 13)
                    {
                        position += 3;
                        frame.transform.localPosition = new Vector2(framePosition[position - 1, 0], framePosition[position - 1, 1]);
                    }
                }
            }
        

    }

    public void DoTheThing()
    {
        domBox.SetActive(true);
        domActive = true;
        DialogController.theDC.dActive = true;
        position = 1;
        frame.transform.localPosition = new Vector2(framePosition[0, 0], framePosition[0, 1]);
        input = "";
        text.text = "";
    }
}
