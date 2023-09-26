using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton3 : MonoBehaviour
{
    public void NextPage()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
