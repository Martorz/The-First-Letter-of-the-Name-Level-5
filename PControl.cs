using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PControl : MonoBehaviour {

    public float moveSpeed;
    public bool whenMMisClosed = false, forHurtPlayer = false;
    private Animator anm;
    public Rigidbody2D rigidbodyA;
    public static PControl pControl;
    public string whatPositionNow; //для DialogHolder'a

    private bool PlayerMoving;
    private Vector2 LastMove;

    public string attackedBy;
    public static bool leavingBattle;

    public string startPoint;
    public Vector3 startPointForBattle;

	// Use this for initialization
	void Start () {
        pControl = this;
        anm = GetComponent<Animator>();
        rigidbodyA = GetComponent<Rigidbody2D>();
        SceneManager.sceneLoaded += OnSceneLoaded; //запускает включение/выключение гг при битве
    }
	
	// Update is called once per frame
	void Update () {
        if (whenMMisClosed)
        {
            //все ниже - движение
            PlayerMoving = false;

            if (!DialogController.theDC.dActive)
            {
                if (rigidbodyA.bodyType != RigidbodyType2D.Static)
                { //чтобы не повторяло миллион раз предупреждение о выключенном rigidbody
                    if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
                    {
                        rigidbodyA.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rigidbodyA.velocity.y);
                        PlayerMoving = true;
                        LastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                    }

                    if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
                    {
                        rigidbodyA.velocity = new Vector2(rigidbodyA.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                        PlayerMoving = true;
                        LastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                    }

                    if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
                    {
                        rigidbodyA.velocity = new Vector2(0f, rigidbodyA.velocity.y);
                    }

                    if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
                    {
                        rigidbodyA.velocity = new Vector2(rigidbodyA.velocity.x, 0f);
                    }

                    anm.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                    anm.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                    anm.SetBool("PlayerMoving", PlayerMoving);
                    anm.SetFloat("LastMoveX", LastMove.x);
                    anm.SetFloat("LastMoveY", LastMove.y);

                    if (Input.GetAxisRaw("Vertical") > 0.5f) whatPositionNow = "up";
                    if (Input.GetAxisRaw("Vertical") < -0.5f) whatPositionNow = "down";
                    if (Input.GetAxisRaw("Horizontal") > 0.5f) whatPositionNow = "right";
                    if (Input.GetAxisRaw("Horizontal") < -0.5f) whatPositionNow = "left";
                }
            }
            else rigidbodyA.velocity = Vector2.zero;
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //выключает, когда битва, и включает вне нее
        if (LoadNewArea.lastLvlLoaded == "Battle" )
        {
            PStats.pStats.lastSortingLayer = GetComponent<SpriteRenderer>().sortingLayerName;
            GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            rigidbodyA.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (scene.name != "Battle" && scene.name != "BossBattle" && rigidbodyA.bodyType == RigidbodyType2D.Static)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = PStats.pStats.lastSortingLayer;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            rigidbodyA.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    
}
