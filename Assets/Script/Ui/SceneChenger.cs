using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChenger : MonoBehaviour
{
    public void From_TitleScene_To_NewGameScene()
    {
        SceneManager.LoadScene("NewGameScene");
    }

    public void From_NewGameScene_To_InGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}