using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotToBeVanishingInHills2 : MonoBehaviour {

    public GameObject borderA, borderB;
    private SpriteRenderer render;
    private GameObject gg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gg = other.gameObject;
        render = gg.GetComponent<SpriteRenderer>();
        if ((other.gameObject.tag == "movingThings" || (other.gameObject.tag == "Player")) && (render.sortingLayerName == "ForHills"))
        {
            render.sortingLayerName = "All";
            if (other.gameObject.tag == "Player")
            {
                borderA.SetActive(true);
                borderB.SetActive(false);
            }
        }
    }
    }
