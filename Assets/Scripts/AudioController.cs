using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class AudioController : MonoBehaviour
{
    [Serializable]
    public class BGMSelectItem
    {
        public string scene;
        public AudioClip clip;
    }

    [SerializeField] List<BGMSelectItem> BGMList = new List<BGMSelectItem>();
    public AudioSource source;
    public float MasterVolume = 0.6f;
    AudioClip CurrentClip;
    AudioClip NextClip;

    public class Data
    {
        public readonly static Data Instance = new Data();
        public string referer = string.Empty;
    }

    private AudioClip this[string s]
    {
        get
        {
            return BGMList
                .Where(a => a.scene == s)
                .Select(b => b.clip)
                .FirstOrDefault();
        }
    }

    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Data.Instance.referer = SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene i_oldScene, Scene i_loadedScene)
    {
       // Debug.LogFormat("{0} was loaded (Previous: {1})", i_loadedScene.name, Data.Instance.referer);

        foreach (var temp in BGMList)
        {
            if (temp.scene == i_loadedScene.name)
            {
                NextClip = temp.clip;
                break;
            }
            NextClip = null;
        }
        foreach (var temp in BGMList)
        {
            if (temp.scene == Data.Instance.referer)
            {
                CurrentClip = temp.clip;
                break;
            }
            CurrentClip = null;
        }
        //Debug.LogFormat("{0} will be played (Previous: {1})", NextClip, CurrentClip);

        if (CurrentClip != NextClip)
        {
            StartCoroutine(LoadSceneProcess());
        }

        Data.Instance.referer = SceneManager.GetActiveScene().name;
    }

    IEnumerator LoadSceneProcess()
    {
        //フェードアウト
        while (Fade(-0.05f))
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        source.clip = this[SceneManager.GetActiveScene().name];
        source.PlayDelayed(1);
        while (Fade(+0.02f))
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    private bool Fade(float gain)
    {
        if (this.source.volume + gain <= 0 || this.MasterVolume <= this.source.volume + gain)
        {
            return false;
        }

        this.source.volume += gain;
        return true;
    }
}
