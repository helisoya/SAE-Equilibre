using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the options tab, where you can customize the options of the softare
/// </summary>
public class OptionsTab : MainMenuTab
{
    [Header("Options Tab")]
    [SerializeField] private TMP_Dropdown dropdownServer;
    [SerializeField] private Toggle toggleVocalAssistant;

    private int startIndex;

    public override void Open()
    {
        base.Open();

        dropdownServer.ClearOptions();
        startIndex = -1;
        List<string> options = new List<string>();

        GameManager.instance.PlayVocalAssistantSFX(GameManager.instance.GetVocalAssistantData().optionsClip);


        Dictionary<string, IPAddress> addresses = GameManager.instance.possibleAddresses;
        IPAddress currentAddr = GameManager.instance.ipAddress;
        string key;
        for (int i = 0; i < addresses.Keys.Count; i++)
        {
            key = addresses.Keys.ElementAt(i);
            options.Add(key);
            if (startIndex == -1 && addresses[key].Equals(currentAddr))
            {
                startIndex = i;
            }
        }

        dropdownServer.AddOptions(options);
        dropdownServer.SetValueWithoutNotify(startIndex);

        toggleVocalAssistant.SetIsOnWithoutNotify(GameManager.instance.vocalAssistant);
    }

    /// <summary>
    /// Click event for the button that validate the changes
    /// </summary>
    public void Click_Valid()
    {
        ValidChange();
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }

    /// <summary>
    /// Saves the changes to the software's options
    /// </summary>
    public void ValidChange()
    {
        int ipSelected = dropdownServer.value;
        if (startIndex != ipSelected)
        {
            startIndex = ipSelected;
            GameManager.instance.ChangeServerIP(GameManager.instance.possibleAddresses.Keys.ElementAt(dropdownServer.value));
        }

        GameManager.instance.vocalAssistant = toggleVocalAssistant.isOn;
    }

    /// <summary>
    /// Event for changing the value of the vocal assistant
    /// </summary>
    /// <param name="val">Is the vocal assistant active ?</param>
    public void Event_ChangeToggleVocalAssistant(bool val)
    {
        GameManager.instance.PlayVocalAssistantSFX(
            val ? GameManager.instance.GetVocalAssistantData().turnOnClip :
            GameManager.instance.GetVocalAssistantData().turnOffClip
            , true);
    }
}
