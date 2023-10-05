using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolAt2Bullet : MonoBehaviour {

    Rigidbody2D rig;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = transform.up * moveSpeed * BattleInter.Bi.mSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (!(transform.position.x > -4.9f && transform.position.y > -2.6f && transform.position.x < 3.57f && transform.position.y < 5.84f)) Destroy(gameObject);

    }
}
