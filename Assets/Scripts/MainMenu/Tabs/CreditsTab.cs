using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the credits tab, where you can see the credits
/// </summary>
public class CreditsTab : MainMenuTab
{
    public void Click_ToTitle()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }
}
