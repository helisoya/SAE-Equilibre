using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTab : MainMenuTab
{
    [Header("Title Tab")]
    [SerializeField] private ExercicesTab exerciceTab;
    [SerializeField] private OptionsTab optionsTab;
    public void Click_ToExerciceTab()
    {
        MainMenuManager.instance.StartTransition(this, exerciceTab);

    }

    public void Click_ToOptionsTab()
    {
        MainMenuManager.instance.StartTransition(this, optionsTab);
    }
}
