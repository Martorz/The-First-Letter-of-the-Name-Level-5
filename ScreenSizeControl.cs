using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeControl : MonoBehaviour {

    private int h, w;

    private void Start()
    {
        if (!Screen.fullScreen)
        {
            h = Screen.height;
            w = Screen.width;
        }
        else
        {
            h = (int)(Mathf.Ceil(Screen.currentResolution.height * 0.6f));
            w = (int)(Mathf.Ceil(Screen.currentResolution.width * 0.6f));
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (!Screen.fullScreen) Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            else Screen.SetResolution(w, h, false);
        }

    }
}
