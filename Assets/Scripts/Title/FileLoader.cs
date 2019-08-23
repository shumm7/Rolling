using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileLoader : MonoBehaviour
{
    public Text Credits;

    void Start()
    {
        bool exist = GetComponent<FileController>().checkExist("config.json");
        if (!exist)
        {
            Debug.Log("There is no config.json");
            GetComponent<FileController>().generateNewData("config.json");
            Debug.Log("Generated config.json");
        }
        FileController.Config temp = GetComponent<VideoSettings>().loadData();
        GetComponent<VideoSettings>().applyAllOptions(temp);
        GetComponent<AudioSettings>().applyAllOptions(temp);

        Credits.text = string.Format("Unity {0}\n Rolling!! {1}", UnityEngine.Application.unityVersion, Application.version);
    }
}
