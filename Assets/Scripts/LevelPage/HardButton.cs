using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardButton : MonoBehaviour
{
    public void SetDifficultyToHard()
    {
        DifficultyManager.currentDifficulty = 3;
        Debug.Log("currentDifficultyChosen = " + DifficultyManager.currentDifficulty);
    }
}
