using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye_movement : MonoBehaviour {

    public float moveSpeed, timeBetweenMove, timeToMove, waitToReload;
    private bool moving, reloading, ifOut = false;
    private Vector3 moveDirection, lastMoveDirection;
    private float timeToMoveCounter, timeBetweenMoveCounter, timeToChase, timeToGetBack;
    private Rigidbody2D rgb;
    bool check = false;
    private Transform target;

    GameObject theirSpawner;

    // Use this for initialization
    void Start () {
        rgb = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        theirSpawner = GameObject.Find("eyeSpawner");
    }
	
	// Update is called once per frame
	void Update () {
        if (!DialogController.theDC.dActive)
        {
            if (check && timeToChase > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                timeToChase -= Time.deltaTime;
            }
            else
            {
                if (moving)
                {
                    timeToMoveCounter -= Time.deltaTime;
                    rgb.velocity = moveDirection;
                    if (timeToMoveCounter < 0f)
                    {
                        moving = false;
                        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                    }

                    if (ifOut) //пока не истечет этот таймер, монстр должен вернуться "домой". Если он не успевает, он уничтожается
                    {
                        timeToGetBack -= Time.deltaTime;
                        if (timeToGetBack < 0f)
                        {
                            if (!theirSpawner.GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<CircleCollider2D>()))
                            {
                                theirSpawner.GetComponent<EnemySpawn>().howManyNow--;
                                Destroy(gameObject);
                            }
                            else timeToGetBack = 5;
                        }
                    }
                }
                else
                {
                    timeBetweenMoveCounter -= Time.deltaTime;
                    rgb.velocity = Vector2.zero;
                    if (timeBetweenMoveCounter < 0f)
                    {
                        moving = true;
                        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                        //не дает уйти за пределы определенной территории
                        if (theirSpawner.GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<CircleCollider2D>()) == false)
                        {
                            ifOut = true;
                        }
                        else { ifOut = false; }
                        if (ifOut)
                        {
                            if (Vector2.Distance(GameObject.Find("BackPoint1").GetComponent<Transform>().position, gameObject.transform.position) < Vector2.Distance(GameObject.Find("BackPoint2").GetComponent<Transform>().position, gameObject.transform.position))
                            {
                                moveDirection = new Vector3(-0.5f * moveSpeed, -1f * moveSpeed, 0f);
                            }
                            else
                            {
                                moveDirection = new Vector3(-1f * moveSpeed, -1f * moveSpeed, 0f);
                            }
                        }
                        else
                        {
                            moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                            if (timeToGetBack != 5) timeToGetBack = 5;
                        }
                    }
                }
            }
        }
        else rgb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            check = true;
            timeToChase = 5;
            rgb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            check = false;
            rgb.velocity = moveDirection;
        }
    }
}
