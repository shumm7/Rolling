using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] string SceneName = "Stage1";

    public void OnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}
