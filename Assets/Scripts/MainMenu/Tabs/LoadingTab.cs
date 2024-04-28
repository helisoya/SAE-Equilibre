using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the loading tab, where the player has to wait for the loading to finish
/// </summary>
public class LoadingTab : MainMenuTab
{
    [Header("Loading Tab")]
    [SerializeField] private GameObject loadingGuyNormal;
    [SerializeField] private GameObject loadingGuyAssistant;

    public override void Open()
    {
        base.Open();

        bool isAssistantActive = GameManager.instance.vocalAssistant;

        loadingGuyNormal.SetActive(!isAssistantActive);
        loadingGuyAssistant.SetActive(isAssistantActive);
    }
}
