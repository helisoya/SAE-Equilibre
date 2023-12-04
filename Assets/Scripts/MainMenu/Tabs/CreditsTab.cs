using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTab : MainMenuTab
{
    public void Click_ToTitle()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }
}
