using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    private BoxCollider2D bx;

	// Use this for initialization
	void Start () {
        bx = GetComponent<BoxCollider2D>();
        Good2DController.forBattleInter.SetBounds(bx);
	}
}
