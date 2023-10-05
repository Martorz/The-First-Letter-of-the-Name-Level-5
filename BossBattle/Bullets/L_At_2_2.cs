using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L_At_2_2 : MonoBehaviour {

    public float time, time1;
    public GameObject bul;
    float timeToStop, timeToStop1;
    bool check = true;
    int count = 0;

    // Update is called once per frame
    void Update()
    {
        if (timeToStop <= 0f)
        {
            if (check)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Rotate(0, 0, Random.Range(-20, -160));
                check = false;
            }

            if (timeToStop1 <= 0f && count != 5)
            {
                count++;
                timeToStop1 = time1;
                Vector2 whereToSpawn = transform.position;
                SceneManager.MoveGameObjectToScene(Instantiate(bul, whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
                transform.Rotate(0, 0, -5);
                SceneManager.MoveGameObjectToScene(Instantiate(bul, whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
                transform.Rotate(0, 0, 10);
                SceneManager.MoveGameObjectToScene(Instantiate(bul, whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
                transform.Rotate(0, 0, -5);
            }
            else if (count == 5)
            {
                timeToStop = time;
                count = 0;
                check = true;
            }
            else timeToStop1 -= Time.unscaledDeltaTime;
        }
        else timeToStop -= Time.unscaledDeltaTime;
    }
}
