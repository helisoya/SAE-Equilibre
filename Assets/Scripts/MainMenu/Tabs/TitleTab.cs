using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTab : MainMenuTab
{
    [Header("Title Tab")]
    [SerializeField] private ExercicesTab exerciceTab;
    public void Click_ToExerciceTab()
    {
        Close();
        exerciceTab.Open();
    }
}
