using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyButton : MonoBehaviour
{
    public void SetDifficultyToEasy()
    {
        DifficultyManager.currentDifficulty = 1;
        Debug.Log("currentDifficultyChosen = " + DifficultyManager.currentDifficulty);
    }
}
