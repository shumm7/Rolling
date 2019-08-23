using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    //設定項目オブジェクト
    public Slider sliderBGM;
    public Slider sliderSE;
    public Toggle toggleMute;

    //スライダー用テキスト
    public Text sliderBGMText;
    public Text sliderSEText;

    //Canvas
    public GameObject canvasVideoSettings;
    public GameObject canvasOtherSettings;

    //サウンドミキサー
    public AudioMixer AudioMixer;
    private float GainBGM;
    private float GainSE;

    //データ保存用クラス
    public FileController.Config Data = new FileController.Config();

    [SerializeField] bool IsComponent = false;


    void Start()
    {
        if (!IsComponent)
        {
            setDefaultValue();
        }
    }

    void Update()
    {
        if (!IsComponent)
        {
            //スライダーの値表示
            sliderBGMText.text = ((int)sliderBGM.value).ToString() + " %";
            sliderSEText.text = ((int)sliderSE.value).ToString() + " %";

            Data.BGM = (int)sliderBGM.value;
            Data.SE = (int)sliderSE.value;
            Data.Mute = toggleMute.isOn;
            applyAllOptions(Data);
        }
    }

    public void setDefaultValue()
    {
        try
        {
            FileController.Config temp = loadData();

            sliderBGM.value = temp.BGM;
            sliderSE.value = temp.SE;
            toggleMute.isOn = temp.Mute;
        }
        catch (Exception e)
        {
            Debug.Log("Cannot set default value");
            Debug.Log(e.Message);
        }
    }

    public void applyAudioSettings()
    {
        FileController.Config temp = loadData();

        temp.BGM = (int)sliderBGM.value;
        temp.SE = (int)sliderSE.value;
        temp.Mute = toggleMute.isOn;

        applyAllOptions(temp);
        saveData(temp);
    }

    public void applyAllOptions(FileController.Config temp)
    {
        if (!temp.Mute)
        {
            GainBGM = (float)(20f * Math.Log(temp.BGM * 0.01f, 10));
            GainSE = (float)(20f * Math.Log(temp.SE * 0.01f, 10));
            if (temp.BGM == 0)
                GainBGM = -80;
            if (temp.SE == 0)
                GainSE = -80;
            AudioMixer.SetFloat("BGMVolume", GainBGM);
            AudioMixer.SetFloat("SEVolume", GainSE);

        }
        else
        {
            AudioMixer.SetFloat("BGMVolume", -80);
            AudioMixer.SetFloat("SEVolume", -80);
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
