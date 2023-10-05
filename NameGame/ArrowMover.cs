using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour {

    public float speed;
    public int howMany;

    private void Start()
    {
        speed = Random.Range(GameInter.inter.minS, GameInter.inter.maxS);
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - speed, gameObject.transform.position.y);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "plus") howMany++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "plus") howMany--;
    }
}
