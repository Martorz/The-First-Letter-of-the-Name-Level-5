using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Good2DController : MonoBehaviour {
    public Good2D camHelp;
    public float moveSpeed = 5;
    public static GameObject justCamera;
    public GameObject a;
    public Camera cam;

    public GameObject followTarget;
    
    Vector3 targetPos;

    public static Good2DController forBattleInter;
    public BoxCollider2D boundBox;
    private Vector3 minBounds, maxBounds;
    private float halfHeight, halfWidth;

    bool cutIsActive = false;
    
    void Start () {
        forBattleInter = this;
        cam = gameObject.GetComponent<Camera>();
        SceneManager.sceneLoaded += OnSceneLoaded; //запускает корректировку камеры при битве

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }


	void Update () {
        /*
        //делает камеру красиво
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        Vector3 moveDir = targetPos - camHelp.GetCameraPos();
        camHelp.Move(moveDir * moveSpeed * Time.deltaTime);*/ //больше не делает
        if (!cutIsActive)
        {
            transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

            if (boundBox == null && LoadNewArea.lastLvlLoaded != null)
            {
                if (FindObjectOfType<Bounds>() != null)
                {
                    boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
                    minBounds = boundBox.bounds.min;
                    maxBounds = boundBox.bounds.max;
                }
            }
            if (boundBox != null)
            {
                float clX, clY;
                if (boundBox.size.x <= halfWidth * 2) clX = 0;
                else clX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
                if (boundBox.size.y <= halfHeight * 2) clY = 0;
                else clY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
                transform.position = new Vector3(clX, clY, transform.position.z);
            }
        }
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //корректирует размер камеры при битве. другие способы все ломают
    {
        
        if (LoadNewArea.lastLvlLoaded == "Battle") cam.orthographicSize = 7.55f;
        else if (LoadNewArea.lastLvlLoaded != "Battle") cam.orthographicSize = 7;
        
    }

    public void SetBounds(BoxCollider2D bx)
    {
        boundBox = bx;
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    public void SetCameraForCutscene()
    {
        cutIsActive = true;
        transform.position = new Vector3(100, -30, transform.position.z);
    }
}
