using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {
    bool check;
    public int type, type2;
    GameObject catcher;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Provirka())
        {
            switch (type)
            {
                case 0:
                    if (Input.GetKeyDown(ControlControl.up))
                    {
                        if (check)
                        {
                            GameInter.inter.counter+= 0.5f;
                            if (catcher.GetComponent<ArrowMover>().howMany == 2)
                            {
                                GameInter.inter.counter += 0.5f;
                                GameInter.inter.perfectCounter += 1;
                            }
                            else GameInter.inter.basicCounter += 1;
                            Destroy(catcher);
                            check = false;
                        }
                    }
                    break;

                case 1:
                    if (Input.GetKeyDown(ControlControl.down))
                    {
                        if (check)
                        {
                            GameInter.inter.counter += 0.5f;
                            if (catcher.GetComponent<ArrowMover>().howMany == 2)
                            {
                                GameInter.inter.counter += 0.5f;
                                GameInter.inter.perfectCounter += 1;
                            }
                            else GameInter.inter.basicCounter += 1;
                            Destroy(catcher);
                            check = false;
                        }
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(ControlControl.right))
                    {
                        if (check)
                        {
                            GameInter.inter.counter += 0.5f;
                            if (catcher.GetComponent<ArrowMover>().howMany == 2)
                            {
                                GameInter.inter.counter += 0.5f;
                                GameInter.inter.perfectCounter += 1;
                            }
                            else GameInter.inter.basicCounter += 1;
                            Destroy(catcher);
                            check = false;
                        }
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(ControlControl.left))
                    {
                        if (check)
                        {
                            GameInter.inter.counter += 0.5f;
                            if (catcher.GetComponent<ArrowMover>().howMany == 2)
                            {
                                GameInter.inter.counter += 0.5f;
                                GameInter.inter.perfectCounter += 1;
                            }
                            else GameInter.inter.basicCounter += 1;
                            Destroy(catcher);
                            check = false;
                        }
                    }
                    break;
            }
            
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "arrow")
        {
            check = true;
            catcher = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "arrow") check = false;
    }

    bool Provirka()
    {
        if (GameObject.FindObjectOfType<BattleInter>() != null)
        {
            if (BattleInter.Bi.nameGame) return true;
            else return false;
        }
        else if (GameObject.FindObjectOfType<Dancefloor>() != null)
        {
            if (Dancefloor.thing.scene3IsActive) return true;
            else return false;
        }
        else return false;
    }
}
