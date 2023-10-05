using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L_At_1 : MonoBehaviour {

    public float time;
    public GameObject bul;
    float timeToStop;

    // Update is called once per frame
    void Update()
    {
        if (timeToStop <= 0f)
        {
            timeToStop = time;
            Vector2 whereToSpawn = transform.position;
            SceneManager.MoveGameObjectToScene(Instantiate(bul, whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
        }
        else timeToStop -= Time.unscaledDeltaTime;
    }
}
