using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoSettingsButton : MonoBehaviour
{
    [SerializeField] string ButtonMode;
    [SerializeField] GameObject VideoSettings;

    public GameObject canvasVideoSettings;
    public GameObject canvasAudioSettings;
    public GameObject canvasOtherSettings;

    public GameObject sceneLoader;

    public void OnClick()
    {
        switch (ButtonMode)
        {
            case "back":
                BackButton();
                break;
            case "apply":
                ApplyButton();
                break;
            case "default":
                DefaultButton();
                break;
            case "video":
                VideoButton();
                break;
            case "music":
                MusicButton();
                break;
            case "other":
                OtherButton();
                break;
            case "none":
                break;
        }
    }

    public void BackButton()
    {
        VideoSettings.GetComponent<VideoSettings>().applyVideoSettings();
        string oldscene = Back.getOldSceneName();
        sceneLoader.GetComponent<Back>().LoadScene(oldscene);
        //Debug.LogWarning(oldscene);
    }

    public void ApplyButton()
    {
        VideoSettings.GetComponent<VideoSettings>().applyVideoSettings();
    }

    public void DefaultButton()
    {
        VideoSettings.GetComponent<VideoSettings>().resetAllData();
    }

    public void VideoButton()
    {
        VideoSettings.GetComponent<VideoSettings>().applyVideoSettings();
        canvasVideoSettings.SetActive(true);
        canvasAudioSettings.SetActive(false);
        canvasOtherSettings.SetActive(false);
    }

    public void MusicButton()
    {
        VideoSettings.GetComponent<VideoSettings>().applyVideoSettings();
        canvasVideoSettings.SetActive(false);
        canvasAudioSettings.SetActive(true);
        canvasOtherSettings.SetActive(false);
    }

    public void OtherButton()
    {
        VideoSettings.GetComponent<VideoSettings>().applyVideoSettings();
        canvasVideoSettings.SetActive(false);
        canvasAudioSettings.SetActive(false);
        canvasOtherSettings.SetActive(true);
    }
}
