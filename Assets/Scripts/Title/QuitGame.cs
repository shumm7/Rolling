using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    public void OnClick()
    {
        Quit();
    }
}
