using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStartPoint : MonoBehaviour {

    private PControl theP;
    private Good2DController theC;

    public string pointName;
    public PStartPoint otherPoints;

	// Use this for initialization
	void Start () {
        theP = FindObjectOfType<PControl>();
        theC = FindObjectOfType<Good2DController>();
        if (PControl.leavingBattle)
        {
            theP.transform.position = PControl.pControl.startPointForBattle;
            theC.transform.position = new Vector3(PControl.pControl.startPointForBattle.x, PControl.pControl.startPointForBattle.y, theC.transform.position.z);
        }
        if (theP.startPoint != pointName) enabled = false;
    }

    private void Update()
    {
        if (PControl.leavingBattle)
        {
            theP.transform.position = PControl.pControl.startPointForBattle;
            theC.transform.position = new Vector3(PControl.pControl.startPointForBattle.x, PControl.pControl.startPointForBattle.y, theC.transform.position.z);
            enabled = false;
        }
        else if (!LoadNewArea.loadNA.lsd.activeSelf)
        {
            if (PStats.pStats.ifLoading)
            {
                PStats.pStats.ifLoading = false;
                theP.transform.position = PStats.pStats.startPointForLoading;
                theC.transform.position = new Vector3(PStats.pStats.startPointForLoading.x, PStats.pStats.startPointForLoading.y, theC.transform.position.z);
                enabled = false;
            }
            else if (theP.startPoint == pointName && !PStats.pStats.ifLoading)
            {
                theP.transform.position = transform.position;
                theC.transform.position = new Vector3(transform.position.x, transform.position.y, theC.transform.position.z);
                enabled = false;
            }
        }
    }
}
