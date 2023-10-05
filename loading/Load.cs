using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour {

    public string level, position, exitPoint;
    bool loaded = false;
    public Animator transition;


    void OnTriggerStay2D(Collider2D other)
    {
        if (loaded == false)
        {
            if ((other.gameObject.name == "player") && (((Input.GetAxisRaw("Vertical") > 0.5f) && (position == "up")) || ((Input.GetAxisRaw("Vertical") < -0.5f) && (position == "down")) || ((Input.GetAxisRaw("Horizontal") > 0.5f) && (position == "right")) || ((Input.GetAxisRaw("Horizontal") < -0.5f) && (position == "left"))))
            {
                PControl.pControl.startPoint = exitPoint;
                LoadNewArea.loadNA.LoadLvl(level, transition);
                loaded = true;
            }
        }
    }
}
