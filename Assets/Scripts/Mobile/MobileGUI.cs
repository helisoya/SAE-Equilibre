using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MobileGUI : MonoBehaviour
{
    [SerializeField] private AppClient client;

    [Header("Unconnected")]
    [SerializeField] private GameObject unconnectedRoot;
    [SerializeField] private Button tryConnectionButton;
    [SerializeField] private TMP_InputField ipInputField;

    [Header("Connected")]
    [SerializeField] private GameObject connectedRoot;
    [SerializeField] private TextMeshProUGUI connectedIpText;
    [SerializeField] private TextMeshProUGUI serverResponseText;


    public void Click_TryConnection()
    {
        client.TryConnectionToIP(ipInputField.text);
    }


    public void OpenConnectedTab(string ip)
    {
        unconnectedRoot.SetActive(false);
        connectedRoot.SetActive(true);
        connectedIpText.text = "Connected to : " + ip;
    }


    public void Click_AskForForm()
    {
        client.AskForForm();
    }

    public void SetServerResponse(string response)
    {
        serverResponseText.text = "Server Response : " + response;
    }
}
