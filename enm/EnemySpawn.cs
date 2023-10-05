using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour {

    public GameObject enemy;
    float randX, randY;
    Vector2 whereToSpawn;
    public float spawnRate = 2f, sPosX, sPosY;
    public int howMany;
    public int howManyNow = 0;
    float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
        sPosX = transform.position.x;
        sPosY = transform.position.y;
        nextSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (howManyNow < howMany)
        {
            if (nextSpawn <= 0f)
            {
                nextSpawn = spawnRate;
                howManyNow++;
                randX = Random.Range(sPosX - 1f, sPosX);
                randY = Random.Range(sPosY - 1f, sPosY);
                whereToSpawn = new Vector2(randX, randY);
                SceneManager.MoveGameObjectToScene(Instantiate(enemy, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Scene1"));
            }
            else nextSpawn -= Time.deltaTime;
        }
    }

    /*int Counting(string name)
    {
        int output;
        switch (name)
        {
            case "eye":
                output = FindObjectsOfType<eye_movement>().Length;
                break;

            case "ghost":
                output = FindObjectsOfType<ghost_move>().Length;
                break;

            case "warrior":
                output = FindObjectsOfType<war_move>().Length;
                break;

            default:
                output = -1;
                break;
        }
        return output;
    }*/
}
