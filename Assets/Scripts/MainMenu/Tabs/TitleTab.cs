using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTab : MainMenuTab
{
    public void Click_ToExerciceTab()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.exercicesTab);

    }

    public void Click_ToOptionsTab()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.optionsTab);
    }

    public void Click_Exit()
    {
        Application.Quit();
    }

    public void Click_ToUsers()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.usersTab);
    }

    public void Click_ToEditExerices()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.editExercicesListTab);
    }

    public void Click_ToCredits()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.creditsTab);
    }
}
