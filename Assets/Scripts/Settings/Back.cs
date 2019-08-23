using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    [SerializeField] bool isComponent = false;

    public static string OldSceneName;

    public class Data
    {
        public readonly static Data Instance = new Data();
        public string referer = string.Empty;
    }

    void Start()
    {
        if (isComponent==false)
        {
            SceneManager.activeSceneChanged += SceneLoaded;

            Data.Instance.referer = SceneManager.GetActiveScene().name;
            OldSceneName = SceneManager.GetActiveScene().name;
            DontDestroyOnLoad(this);
        }
    }

    void SceneLoaded(Scene i_oldScene, Scene i_loadedScene)
    {
        if (isComponent==false)
        {
            OldSceneName = Data.Instance.referer;
            Data.Instance.referer = i_loadedScene.name;
            Debug.Log(OldSceneName);
        }
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public static string getOldSceneName()
    {
        return OldSceneName;
    }
}


