using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSettingsButton : MonoBehaviour
{
    [SerializeField] string ButtonMode;
    [SerializeField] GameObject AudioSettings;

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
        AudioSettings.GetComponent<AudioSettings>().applyAudioSettings();
        string oldscene =Back.getOldSceneName();
        sceneLoader.GetComponent<Back>().LoadScene(oldscene);
    }

    public void ApplyButton()
    {
        AudioSettings.GetComponent<AudioSettings>().applyAudioSettings();
    }

    public void DefaultButton()
    {
        AudioSettings.GetComponent<AudioSettings>().resetAllData();
    }

    public void VideoButton()
    {
        AudioSettings.GetComponent<AudioSettings>().applyAudioSettings();
        canvasVideoSettings.SetActive(true);
        canvasAudioSettings.SetActive(false);
        canvasOtherSettings.SetActive(false);
    }

    public void MusicButton()
    {
        AudioSettings.GetComponent<AudioSettings>().applyAudioSettings();
        canvasVideoSettings.SetActive(false);
        canvasAudioSettings.SetActive(true);
        canvasOtherSettings.SetActive(false);
    }

    public void OtherButton()
    {
        AudioSettings.GetComponent<AudioSettings>().applyAudioSettings();
        canvasVideoSettings.SetActive(false);
        canvasAudioSettings.SetActive(false);
        canvasOtherSettings.SetActive(true);
    }
}
