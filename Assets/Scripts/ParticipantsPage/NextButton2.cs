using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton2 : MonoBehaviour
{
    public void NextPage()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
