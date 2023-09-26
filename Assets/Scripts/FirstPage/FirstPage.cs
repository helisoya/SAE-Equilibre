using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPage : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("2ndScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
