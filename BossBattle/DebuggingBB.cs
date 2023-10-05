using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingBB : MonoBehaviour {

    public GameObject battle, lord;

	// Use this for initialization
	void Start () {
		if (!Console.cmd.cutscene)
        {
            lord.SetActive(true);
            battle.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
