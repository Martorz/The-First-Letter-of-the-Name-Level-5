using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurter : MonoBehaviour {

    public float atk;
    private float waitingTime = 0;
    public bool destroyer = false;

    bool isIn, isHat;

    // Update is called once per frame
	void Update () {
        if (isIn && !isHat)
        {
            if (waitingTime <= 0)
            {
                PStats.pStats.HurtPlayer(atk);
                waitingTime = 0.3f;
            }
            else waitingTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act" && !other.gameObject.GetComponent<BulletHell>().invis)
        {
            isIn = true;
            waitingTime = 0;
        }
        if (other.gameObject.name == "hat")
        {
            if (destroyer) Destroy(gameObject);
            else isHat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bi_act") isIn = false;
        if (other.gameObject.name == "hat") isHat = false;
    }
}
