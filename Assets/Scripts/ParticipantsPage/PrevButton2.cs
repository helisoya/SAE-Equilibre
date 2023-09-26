using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevButton2 : MonoBehaviour
{
    public void PrevPage()
    {
        SceneManager.LoadScene("2ndScene");
    }
}
