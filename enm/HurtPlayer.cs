using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour {

    bool loaded = false;
    public string exitPoint;
    Animator transition;

    // Use this for initialization
    void Start () {
        transition = GameObject.Find("transitionName").GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (loaded == false)
        {
            if (other.gameObject.name == "player")
            {
                if (BattleInter.StartBattle && !PControl.pControl.forHurtPlayer) {
                    InfoControl.info.youCanOpenI = false;
                    PControl.pControl.attackedBy = gameObject.name;
                    PControl.pControl.startPoint = exitPoint;
                    PControl.pControl.startPointForBattle = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                    DialogController.theDC.dActive = true;
                    StartCoroutine(OpenLoadScreen());
                    //LoadNewArea.loadNA.UnloadBattle();
                    loaded = true;
                }
            }
        }
    }

    IEnumerator OpenLoadScreen()
    {
        transition.SetTrigger("batt");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        DialogController.theDC.dActive = false;
        LoadNewArea.loadNA.UnloadScene(LoadNewArea.lastLvlLoaded);
        LoadNewArea.lastLvlLoaded = "Battle";
    }
}
