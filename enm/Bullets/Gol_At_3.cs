using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gol_At_3 : MonoBehaviour {

    public GameObject[] letters, letters2;
    public bool meh = true, wweh = true;

    bool wasFirst;
    Vector2 whereToSpawn = new Vector2(4.47f, -2.27f), whereToSpawn2 = new Vector2(4.47f, 5.61f);
    int currentLetter1 = 1, currentLetter2 = 1;

	// Use this for initialization
	void Start () {
        SceneManager.MoveGameObjectToScene(Instantiate(letters[0], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
        GameObject.Find("bullets_9(Clone)").GetComponent<GolAt3Bullet>().isFirst = true;
        SceneManager.MoveGameObjectToScene(Instantiate(letters2[0], whereToSpawn2, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
        GameObject.Find("bullets_18(Clone)").GetComponent<GolAt3Bullet>().isFirst = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (BattleInter.bulletHell || Bullet_Spawner_Pack.bsp.start)
        {
            if (wweh)
            {
                SceneManager.MoveGameObjectToScene(Instantiate(letters2[currentLetter2], whereToSpawn2, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
                currentLetter2++;
                if (currentLetter2 >= 5) currentLetter2 = 0;
                wweh = false;
            }

            if (meh)
            {
                SceneManager.MoveGameObjectToScene(Instantiate(letters[currentLetter1], whereToSpawn, Quaternion.identity), SceneManager.GetSceneByName("Battle"));
                currentLetter1++;
                if (currentLetter1 >= 5) currentLetter1 = 0;
                meh = false;
            }
        }
        else Destroy(gameObject);
    }
}
