using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the title tab, where you can choose to go to other views
/// </summary>
public class TitleTab : MainMenuTab
{

    public override void Open()
    {
        base.Open();

        GameManager.instance.PlayVocalAssistantSFX(GameManager.instance.GetVocalAssistantData().titleScreenClip);
    }

    /// <summary>
    /// Click event for opening the exercice tab
    /// </summary>
    public void Click_ToExerciceTab()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.exercicesTab);

    }

    /// <summary>
    /// Click event for opening the options tab
    /// </summary>
    public void Click_ToOptionsTab()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.optionsTab);
    }

    /// <summary>
    /// Click event for exiting the software
    /// </summary>
    public void Click_Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Click event for opening the users tab
    /// </summary>
    public void Click_ToUsers()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.usersTab);
    }

    /// <summary>
    /// Click event for opening the exercice editing tab
    /// </summary>
    public void Click_ToEditExerices()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.editExercicesListTab);
    }

    /// <summary>
    /// Click event for opening the credits tab
    /// </summary>
    public void Click_ToCredits()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.creditsTab);
    }
}
