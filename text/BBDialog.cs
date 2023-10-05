using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBDialog : MonoBehaviour {

    public GameObject box, arrow, battle;
    public Text dText;
    public bool textActive = false; //проверяет активность диалог-окна
    public Animator anmL, anmBack;

    public float letterPause = 0.2f;
    int currentLine = 0;

    private IEnumerator coroutine;

    public string[] currentDialog;

    // Use this for initialization
    void Start () {
        box.SetActive(true);
        ShowText();
    }
	
	// Update is called once per frame
	void Update () {
        if (box.activeSelf)
        {
            if (Input.GetKeyUp(ControlControl.z) || (Input.GetKeyUp(KeyCode.Z)))
                {
                    if (arrow.activeSelf)
                    {
                        if (currentLine >= currentDialog.Length)
                        {
                            StopCoroutine(coroutine);
                            currentDialog = null;
                            currentLine = 0;
                            battle.SetActive(true);
                            textActive = false;
                            box.SetActive(false);
                            arrow.SetActive(false);
                            anmL.SetTrigger("check");
                            anmBack.SetTrigger("check");
                    }
                        else
                        {
                            arrow.SetActive(false);
                            textActive = false;
                            StopCoroutine(coroutine);
                            ShowText();
                        }
                    }
                    else
                    {
                        arrow.SetActive(true);
                        textActive = false;
                        StopCoroutine(coroutine);
                        dText.text = currentDialog[currentLine - 1];
                    }
                }
            }
        
    }

    void ShowText()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        dText.text = "";
        textActive = true;
        coroutine = TypeText(currentDialog[currentLine]);
        StartCoroutine(coroutine);
        currentLine++;
    }

    IEnumerator TypeText(string dialog)
    {
        foreach (char letter in dialog.ToCharArray())
        {
            if (textActive)
            {
                dText.text += letter;
                yield return new WaitForSeconds(letterPause);
            }
            else yield break;
        }

        arrow.SetActive(true);
    }
}
