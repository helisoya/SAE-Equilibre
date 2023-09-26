using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public static int selectedPosture;
    public int posture;

    public void ToDemoDetail()
    {
        selectedPosture = posture;
        SceneManager.LoadScene("DemoDetail");
    }

}
