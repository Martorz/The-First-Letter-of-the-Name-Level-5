using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public GameObject dBox, arrow;
    public Text dText;
    public Image icon;
    public bool dActive = false; //стопает мир
    public bool dialogActive = false, textActive = false; //проверяет активность диалог-окна
    
    public static DialogController theDC;
    public float letterPause = 0.2f;
    int currentLine = 0;

    private IEnumerator coroutine;

    string[] currentDialog;

    // Use this for initialization
    void Start () {
        theDC = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!ShopControl.thing.shopActive && dialogActive)
        {
            if (currentDialog == null)
            {
                currentLine = 0;
                textActive = false;
                dBox.SetActive(false);
                dialogActive = false;
                arrow.SetActive(false);
                dActive = false;
            }

            if (dialogActive)
            {

                if (Input.GetKeyUp(ControlControl.z))
                {
                    if (arrow.activeSelf)
                    {
                        if (currentLine >= currentDialog.Length)
                        {
                            StopCoroutine(coroutine);
                            currentDialog = null;
                            currentLine = 0;
                            textActive = false;
                            dBox.SetActive(false);
                            dialogActive = false;
                            arrow.SetActive(false);
                            dActive = false;
                            InfoControl.info.youCanOpenI = true;
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

    }

    public void ShowBox(string[] dialog, Sprite meh)
    {
        icon.sprite = meh;
        currentDialog = dialog;
        dActive = true;
        dialogActive = true;
        dBox.SetActive(true);
        ShowText();
    }

    public void ShowTextText(string dialog)
    {
        dActive = true;
        dialogActive = true;
        dBox.SetActive(true);
        if (coroutine != null) StopCoroutine(coroutine);
        dText.text = "";
        textActive = true;
        coroutine = TypeText(dialog);
        StartCoroutine(coroutine);
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
        if (!ShopControl.thing.shopActive) arrow.SetActive(true);
    }
}
