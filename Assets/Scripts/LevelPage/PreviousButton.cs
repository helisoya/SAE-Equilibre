using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousButton : MonoBehaviour
{
    public void PrevPage()
    {
        SceneManager.LoadScene("ParticipantsScene");
    }
}
