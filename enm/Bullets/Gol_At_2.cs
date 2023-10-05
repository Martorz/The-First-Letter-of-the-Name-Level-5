using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gol_At_2 : MonoBehaviour {

    public float time;
    public GameObject[] bul;
    float timeToStop;
    int i = 0;

    // Update is called once per frame
	void Update ()
    {
        if (timeToStop <= 0f)
        {
            timeToStop = time;
            Vector2 whereToSpawn = transform.position;
            SceneManager.MoveGameObjectToScene(Instantiate(bul[i], whereToSpawn, transform.rotation), SceneManager.GetSceneByName("Battle"));
            i++;
            if (i >= 3) i = 0;
        }
        else timeToStop -= Time.unscaledDeltaTime;
    }
}
