using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class OptionsTab : MainMenuTab
{
    [Header("Options Tab")]
    [SerializeField] private TMP_Dropdown dropdownServer;
    [SerializeField] private TitleTab titleTab;

    private int startIndex;

    public override void Open()
    {
        base.Open();

        dropdownServer.ClearOptions();
        startIndex = -1;
        List<string> options = new List<string>();


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
    }


    public void Click_ToTitle()
    {
        ValidChange();
        MainMenuManager.instance.StartTransition(this, titleTab);
    }

    public void Click_Valid()
    {
        ValidChange();
    }

    public void ValidChange()
    {
        int ipSelected = dropdownServer.value;
        if (startIndex != ipSelected)
        {
            startIndex = ipSelected;
            GameManager.instance.ChangeServerIP(GameManager.instance.possibleAddresses.Keys.ElementAt(dropdownServer.value));
        }
    }

}
