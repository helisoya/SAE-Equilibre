using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalButton : MonoBehaviour
{
    public void SetDifficultyToNormal()
    {
        DifficultyManager.currentDifficulty = 2;
        Debug.Log("currentDifficultyChosen = " + DifficultyManager.currentDifficulty);
    }
}
