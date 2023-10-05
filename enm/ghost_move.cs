using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_move : MonoBehaviour {

    public float moveSpeed, timeBetweenMove, timeToMove, waitToReload;
    private bool moving, reloading, ifOut = false;
    private Vector3 moveDirection, lastMoveDirection;
    private Vector2 LastMove;
    private float timeToMoveCounter, timeBetweenMoveCounter, timeToChase, timeToGetBack;
    private Rigidbody2D rgb;
    private Animator anm;
    bool check = false;
    private Transform target;

    public GameObject theirSpawner;

    // Use this for initialization
    void Start () {
        rgb = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        anm = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (Vector2.Distance(GameObject.Find("ghostSpawner").GetComponent<Transform>().position, gameObject.transform.position) < Vector2.Distance(GameObject.Find("ghostSpawner (1)").GetComponent<Transform>().position, gameObject.transform.position))
        {
            theirSpawner = GameObject.Find("ghostSpawner");
        }
        else
        {
            theirSpawner = GameObject.Find("ghostSpawner (1)");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!DialogController.theDC.dActive)
        {
            if (check && timeToChase > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                float a = target.position.y - transform.position.y;
                float b = target.position.x - transform.position.x;
                float c = Mathf.Sqrt(a * a + b * b);
                moveDirection = new Vector3(b / c, a / c, 0);
                LastMove = new Vector2(moveDirection.x, moveDirection.y);
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
                            if (!GameObject.Find("ghostSpawner").GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
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
                        if (GameObject.Find("ghostSpawner").GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()) == false)
                        {
                            ifOut = true;
                        }
                        else { ifOut = false; }

                        if (ifOut)
                        {
                            if (Vector2.Distance(GameObject.Find("BackPoint3").GetComponent<Transform>().position, gameObject.transform.position) < Vector2.Distance(GameObject.Find("BackPoint4").GetComponent<Transform>().position, gameObject.transform.position))
                            {
                                moveDirection = new Vector3(1f * moveSpeed, 0f, 0f);
                            }
                            else
                            {
                                moveDirection = new Vector3(-1f * moveSpeed, -0.5f * moveSpeed, 0f);
                            }

                            LastMove = new Vector2(moveDirection.x, moveDirection.y);
                        }
                        else
                        {
                            if (timeToGetBack != 5) timeToGetBack = 5;
                            moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
                            LastMove = new Vector2(moveDirection.x, moveDirection.y);
                        }
                    }
                }
            }
        }
        else rgb.velocity = Vector2.zero;

        anm.SetFloat("MoveX", moveDirection.x);
        anm.SetFloat("MoveY", moveDirection.y);
        anm.SetBool("MMoving", moving);
        anm.SetFloat("LastMoveX", LastMove.x);
        anm.SetFloat("LastMoveY", LastMove.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            check = true;
            timeToChase = 3;
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
