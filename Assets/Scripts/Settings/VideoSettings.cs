using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    //設定項目オブジェクト
    public Dropdown dropdownScreenResolution;
    public Slider sliderVSync;
    public Toggle toggleFullscreenMode;
    public Dropdown dropdownAntiAliasing;
    public Dropdown dropdownTextureQuality;
    public Slider sliderShadowDistance;
    public Dropdown dropdownShadowResolution;

    //Canvas
    public GameObject canvasAudioSettings;
    public GameObject canvasOtherSettings;

    //スライダー用テキスト
    public Text sliderVSyncText;
    public Text sliderShadowDistanceText;

    //データ保存用クラス
    public FileController.Config Data = new FileController.Config();

    [SerializeField] bool IsComponent = false;

    void Start()
    {
        if (!IsComponent)
        {
            //設定データを表示
            displayPulldownData();

            //他のCanvasを非表示
            canvasAudioSettings.SetActive(false);

            //Configデータから読み取り
            if (!(GetComponent<FileController>().checkExist("config.json")))
            {
                Debug.Log("There is no config.json");
                GetComponent<FileController>().generateNewData("config.json");
                Debug.Log("Generated config.json");
            }
            Data = loadData();

            setDefaultValue();
        }
    }

    void Update()
    {
        if (!IsComponent)
        {
            //スライダーの値表示
            int VSyncRate = (int)sliderVSync.value;
            int ShadowDistance = (int)sliderShadowDistance.value;
            sliderVSyncText.text = VSyncRate.ToString();
            sliderShadowDistanceText.text = ShadowDistance.ToString();
        }
    }

    public void displayPulldownData()
    {
        //Screen Resolution
        dropdownScreenResolution.ClearOptions();
        Resolution[] resolutions = Screen.resolutions;
        List<string> screenResolutionList = new List<string>();
        foreach (Resolution res in resolutions)
        {
            screenResolutionList.Add(res.width + "x" + res.height);
        }
        dropdownScreenResolution.AddOptions(screenResolutionList);

        //AntiAliasing
        dropdownAntiAliasing.ClearOptions();
        List<string> antiAliasingList = new List<string>();
        antiAliasingList.Add("x0");
        antiAliasingList.Add("x2");
        antiAliasingList.Add("x4");
        antiAliasingList.Add("x8");
        dropdownAntiAliasing.AddOptions(antiAliasingList);

        //Texture Quality
        dropdownTextureQuality.ClearOptions();
        List<string> textureQualityList = new List<string>();
        textureQualityList.Add("Normal");
        textureQualityList.Add("Half");
        textureQualityList.Add("Quarter");
        dropdownTextureQuality.AddOptions(textureQualityList);

        //Shadow Resolution
        dropdownShadowResolution.ClearOptions();
        List<string> shadowResolutionList = new List<string>();
        shadowResolutionList.Add("Low");
        shadowResolutionList.Add("Medium");
        shadowResolutionList.Add("High");
        shadowResolutionList.Add("Very High");
        dropdownShadowResolution.AddOptions(shadowResolutionList);
    }

    public void setDefaultValue()
    {
        try
        {
            FileController.Config temp = loadData();

            //Screen Resolution
            Resolution[] resolutions = Screen.resolutions;
            int i = 0;
            foreach (Resolution res in resolutions)
            {
                if ((res.width + "x" + res.height) == temp.ScreenResolution)
                {
                    dropdownScreenResolution.value = i;
                    break;
                }
                i++;
            }

            //VSync
            sliderVSync.value = temp.VSync;

            //Fullscreen Mode
            toggleFullscreenMode.isOn = temp.Fullscreen;

            // Anti-Aliasing
            switch (temp.AntiAliasing)
            {
                case 0:
                    dropdownAntiAliasing.value = 0;
                    break;
                case 2:
                    dropdownAntiAliasing.value = 1;
                    break;
                case 4:
                    dropdownAntiAliasing.value = 2;
                    break;
                case 8:
                    dropdownAntiAliasing.value = 3;
                    break;
                default:
                    dropdownAntiAliasing.value = 0;
                    break;
            }

            // TextureQuality
            dropdownTextureQuality.value = temp.TextureQuality;

            //Shadow Distance
            sliderShadowDistance.value = temp.ShadowDistance;

            //Shadow Resolution
            dropdownShadowResolution.value = temp.ShadowResolution;
        }
        catch (Exception e)
        {
            Debug.Log("Cannot set default value");
            Debug.Log(e.Message);
        }
    }

    public void applyVideoSettings()
    {
        FileController.Config temp = loadData();

        //Screen Resolution
        Resolution[] resolutions = Screen.resolutions;
        int i = 0;
        foreach (Resolution res in resolutions)
        {
            if (i == dropdownScreenResolution.value)
            {
                temp.ScreenResolution = res.width + "x" + res.height;
                break;
            }
            i++;
        }

        //FullscreenMode
        temp.Fullscreen = toggleFullscreenMode.isOn;

        //VSync
        temp.VSync = (int)sliderVSync.value;

        //Anti-aliasing
        switch (dropdownAntiAliasing.value)
        {
            case 0:
                temp.AntiAliasing = 0;
                break;
            case 1:
                temp.AntiAliasing = 2;
                break;
            case 2:
                temp.AntiAliasing = 4;
                break;
            case 3:
                temp.AntiAliasing = 8;
                break;
        }

        //TextureQuality
        temp.TextureQuality = dropdownTextureQuality.value;

        //ShadowDistance
        temp.ShadowDistance = (int)sliderShadowDistance.value;

        //ShadowResolution
        temp.ShadowResolution = dropdownShadowResolution.value;

        applyAllOptions(temp);
        saveData(temp);
    }

    public void applyAllOptions(FileController.Config temp)
    {
        //ScreenResolution and Fullscreen
        Screen.fullScreen = temp.Fullscreen;
        int Center = temp.ScreenResolution.IndexOf("x");
        int Front = int.Parse(temp.ScreenResolution.Substring(0, Center));
        int Rear = int.Parse(temp.ScreenResolution.Substring(Center + 1));
        Screen.SetResolution(Front, Rear, temp.Fullscreen);

        //VSync
        QualitySettings.vSyncCount = temp.VSync;

        //Anti-aliasing
        QualitySettings.antiAliasing = temp.AntiAliasing;

        //Texture Quality
        QualitySettings.masterTextureLimit = temp.TextureQuality;

        //Shadow Distance
        QualitySettings.shadowDistance = temp.ShadowDistance;

        //Shadow Resolution
        switch (temp.ShadowResolution)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                break;
        }
    }

    public FileController.Config defaultValue()
    {
        return GetComponent<FileController>().resetData();
    }

    public void resetAllData()
    {
        saveData(defaultValue());
        setDefaultValue();
    }

    public bool saveData(FileController.Config data)
    {
        return GetComponent<FileController>().saveData("config.json", data);
    }

    public FileController.Config loadData()
    {
        return GetComponent<FileController>().loadData("config.json");
    }
}
