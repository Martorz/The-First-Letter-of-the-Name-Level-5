using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {
    public GameObject[] arms;
	// Use this for initialization
	void Start () {
        if (!ShopControl.thing.sellItemID.Contains(8)) arms[0].SetActive(false);
        if (!ShopControl.thing.sellItemID.Contains(9)) arms[1].SetActive(false);
        if (!ShopControl.thing.sellItemID.Contains(10)) arms[2].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (arms[0].activeSelf || arms[1].activeSelf || arms[2].activeSelf)
        {
            if (!ShopControl.thing.sellItemID.Contains(8)) arms[0].SetActive(false);
            if (!ShopControl.thing.sellItemID.Contains(9)) arms[1].SetActive(false);
            if (!ShopControl.thing.sellItemID.Contains(10)) arms[2].SetActive(false);
        }
    }
}
