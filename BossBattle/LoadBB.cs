using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBB : MonoBehaviour {

    bool loaded = false;
    public Animator transition;
    public GameObject cut;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (loaded == false)
        {
            if (other.gameObject.name == "player")
                {
                    if (BattleInter.StartBattle)
                    {
                        InfoControl.info.youCanOpenI = false;
                        PControl.pControl.attackedBy = "lord";
                        PControl.pControl.startPoint = "ehh";
                        PControl.pControl.startPointForBattle = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                        LoadNewArea.loadNA.LoadBB(transition);
                        loaded = true;
                    }
                }
            }
        
    }
}
