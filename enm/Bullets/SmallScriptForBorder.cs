using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallScriptForBorder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpecName") collision.GetComponentInParent<War_At_2>().moveCount++;
    }
}
