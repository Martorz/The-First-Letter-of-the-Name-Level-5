using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBKickingScript : MonoBehaviour {

    public GameObject cut, lord, skip;

	// Use this for initialization
	void Start () {
        lord.SetActive(true);
        cut.SetActive(false);
        skip.SetActive(false);
	}
}
