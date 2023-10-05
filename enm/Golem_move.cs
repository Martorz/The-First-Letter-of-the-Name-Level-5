using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_move : MonoBehaviour {

    public float speed;
    private Transform target;
    bool check = false;
    public bool kickGameObject = false;
    Animator anm;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anm = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("golemSpawner").GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
        {
            
            if (check)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
            else { }
        }
        else
        {
            if (!anm.GetBool("kill")) anm.SetBool("kill", true);
            if (kickGameObject) Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            check = false;
        }
    }
}
