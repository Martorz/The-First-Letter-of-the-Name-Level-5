using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolemSpawner : MonoBehaviour {

    public GameObject enemy;
    float randX, randY;
    Vector2 whereToSpawn;
    public float spawnRate = 2f, sPosX, sPosY;
    float nextSpawn = 0.0f;
    public int generateRange;

    // Use this for initialization
    void Start () {
        sPosX = transform.position.x;
        sPosY = transform.position.y;
        nextSpawn = Random.Range(spawnRate / 2, spawnRate);
    }
	
	// Update is called once per frame
	void Update () {
        if (nextSpawn <= 0)
        {
            if (GameObject.Find("golem(Clone)") == null)
            {
                nextSpawn = spawnRate;
                int i = Random.Range(1, generateRange);
                if (i == 1)
                {
                    randX = Random.Range(sPosX - 1f, sPosX);
                    randY = Random.Range(sPosY - 1f, sPosY);
                    whereToSpawn = new Vector2(randX, randY);
                    SceneManager.MoveGameObjectToScene(Instantiate(enemy, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Scene1"));
                }
            }
        }
        else nextSpawn -= Time.deltaTime;
    }
}
