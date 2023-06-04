using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public sceneParam[] scenes;
    public void Awake()
    {
        if (!instance || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string index)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if(scenes[i].index != index)
                continue;
            GameObject anim = GameObject.Find("SceneUnLoadedTransition");
            if (anim)
            {
                anim.GetComponent<Animation>().Play();
            }
            StartCoroutine(loadScene(scenes[i].sceneName, 0.15f));
        }
    }

    IEnumerator loadScene(string ind, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(ind);
    }
}

[System.Serializable]
public struct sceneParam
{
    public string index;
    public string sceneName;
}
