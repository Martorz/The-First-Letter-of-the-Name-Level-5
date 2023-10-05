using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class l_At_1_2 : MonoBehaviour {

    public float time;
    public GameObject[] bul;
    float timeToStop;
    bool j = false;

    // Update is called once per frame
    void Update()
    {
        if ((BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start))
        {
            if (timeToStop <= 0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                timeToStop = time;
                Vector2 whereToSpawn = transform.position;
                for (int i = 0; i < 12; i++)
                {
                    if (j) SceneManager.MoveGameObjectToScene(Instantiate(bul[0], whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
                    else SceneManager.MoveGameObjectToScene(Instantiate(bul[1], whereToSpawn, transform.rotation), SceneManager.GetSceneByName("BossBattle"));
                    transform.Rotate(0, 0, 30);
                }
                j = !j;
            }
            else timeToStop -= Time.unscaledDeltaTime;
        }
        else Destroy(gameObject);
    }
}
