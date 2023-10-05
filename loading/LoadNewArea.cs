using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadNewArea : MonoBehaviour {

    public static LoadNewArea loadNA;
    public static string lastLvlLoaded;
    public GameObject lsd;
    public Slider sldr;
    
    
    bool gameStart = false;

    
    // Use this for initialization
    void Start () {
        loadNA = this;
        if (SceneManager.GetSceneByName("BossBattle").isLoaded) UnloadScene("BossBattle");
    }
	
	public void Starting()
    {
        if (gameStart == false)
        {
            //StartCoroutine(OpenLoadScreen());
            lsd.SetActive(true);
            StartCoroutine(StartLoad("Shop"));
            gameStart = true;
            //lastLvlLoaded = "Scene1";
        }
    }

    public void LoadLvl(string name, Animator transition)
    {
        StartCoroutine(OpenLoadScreen(name, transition));
    }

    public void LoadBB(Animator transition)
    {
        StartCoroutine(OpenBB(transition));
    }

    public void UnloadScene(string level)
    {
        StartCoroutine(Unload(level));
    }


    IEnumerator Unload(string level)
    {
        yield return null;
        SceneManager.UnloadScene(level);
    }

    /*public void UnloadBattle(string level = "Battle")
    {
        if (SceneManager.sceneCount >= 3) StartCoroutine(UnloadB(level));
    }

    IEnumerator UnloadB(string level)
    {
        SceneManager.UnloadScene(level);
        yield return null;
        UnloadBattle();
    }*/

    IEnumerator StartLoad(string level, int counter = 0)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            sldr.value = progress;
            
            yield return null;
        }

        if (operation.progress == 1)
        {
            switch (counter)
            {
                case 0:
                    StartCoroutine(Unload(level));
                    StartCoroutine(StartLoad("Club", counter + 1));
                    break;

                case 1:
                    StartCoroutine(Unload(level));
                    StartCoroutine(StartLoad("Home", counter + 1));
                    break;

                case 2:
                    StartCoroutine(Unload(level));
                    StartCoroutine(StartLoad("Battle", counter + 1));
                    break;

                case 3:
                    StartCoroutine(Unload(level));
                    StartCoroutine(StartLoad("Scene1", counter + 1));
                    break;

                case 4:
                    if (lastLvlLoaded == "Scene1")
                    {
                        lsd.SetActive(false);
                        InfoControl.info.youCanOpenI = true;
                    }
                    else
                    {
                        StartCoroutine(Unload(level));
                        StartCoroutine(StartLoad(lastLvlLoaded, counter + 1));
                    }
                    break;

                default:
                    lsd.SetActive(false);
                    InfoControl.info.youCanOpenI = true;
                    break;
            }
        }
    }

    IEnumerator OpenLoadScreen(string level, Animator transition)
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        StartCoroutine(Unload(lastLvlLoaded));
        lastLvlLoaded = level;
    }

    IEnumerator OpenBB(Animator transition)
    {
        InfoControl.info.youCanOpenI = false;
        string a = lastLvlLoaded;
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync("BossBattle", LoadSceneMode.Additive);
        lastLvlLoaded = "Battle";

        lsd.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            sldr.value = progress;

            yield return null;
        }

        if (operation.progress == 1)
        {
            StartCoroutine(Unload(a));
            lsd.SetActive(false);
        }
        
    }
}
