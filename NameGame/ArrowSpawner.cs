using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowSpawner : MonoBehaviour {

    public GameObject up, down, left, right;
    public string sceneName = "Battle";
    public int spawnType; //0 - верх, 1 - низ, 2 - право, 3 - лево

	public void Spawn()
    {
        if (GameObject.FindObjectOfType<BattleInter>() != null)
        {
            Vector2 whereToSpawn = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            switch (spawnType)
            {
                case 0:
                    SceneManager.MoveGameObjectToScene(Instantiate(up, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    break;

                case 1:
                    SceneManager.MoveGameObjectToScene(Instantiate(down, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    break;

                case 2:
                    SceneManager.MoveGameObjectToScene(Instantiate(right, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    break;

                case 3:
                    SceneManager.MoveGameObjectToScene(Instantiate(left, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    break;
            }
        }
        else if (GameObject.FindObjectOfType<Dancefloor>() != null)
        {
            Vector2 whereToSpawn = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            string name = "";
            switch (spawnType)
            {
                case 0:
                    SceneManager.MoveGameObjectToScene(Instantiate(up, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    name = "UpA(Clone)";
                    break;

                case 1:
                    SceneManager.MoveGameObjectToScene(Instantiate(down, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    name = "DownA(Clone)";
                    break;

                case 2:
                    SceneManager.MoveGameObjectToScene(Instantiate(right, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    name = "RightA(Clone)";
                    break;

                case 3:
                    SceneManager.MoveGameObjectToScene(Instantiate(left, whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName(sceneName));
                    name = "LeftA(Clone)";
                    break;
            }
            GameObject.Find(name).transform.SetParent(GameObject.Find("Canvas").transform);
            GameObject.Find(name).transform.localScale = new Vector3(1.1962f, 1.1962f, 1.1962f);
        }
    }
}
