using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotToBeVanishingInHills : MonoBehaviour {

    public GameObject borderA, borderB;
    private SpriteRenderer render;
    private GameObject gg;

    // Use this for initialization
    void Start () {
        borderA.SetActive(false);
        if (PStats.pStats.lastSortingLayer == "ForHills")
        {
            borderA.SetActive(true);
            borderB.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "movingThings") || (other.gameObject.tag == "Player"))
        {
            gg = other.gameObject;
            render = gg.GetComponent<SpriteRenderer>();
            render.sortingLayerName = "ForHills";
            if (other.gameObject.tag == "Player")
            {
                borderA.SetActive(true);
                borderB.SetActive(false);
            }
        }
    }
}
