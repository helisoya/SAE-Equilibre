using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the background tab, where you can select the background where the exercice will take place
/// </summary>
public class BackgroundTab : MainMenuTab
{
    [Header("Background Tab")]
    [SerializeField] private Transform backgroundsRoot;
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Background[] backgrounds;

    public override void Open()
    {
        base.Open();
        DestroyExistingButtons();

        foreach (Background background in backgrounds)
        {
            Instantiate(backgroundPrefab, backgroundsRoot).GetComponent<BackgroundButton>().Init(background, this);
        }
    }

    public override void Close()
    {
        base.Close();
        DestroyExistingButtons();
    }

    /// <summary>
    /// Destroys the existing buttons
    /// </summary>
    void DestroyExistingButtons()
    {
        Utils.DestroyChildren(backgroundsRoot);
    }

    /// <summary>
    /// Event click for choosing a background and opening the summary tab
    /// </summary>
    /// <param name="sceneName">Selected background's name</param>
    public void Click_ChooseBackground(string sceneName)
    {
        MainMenuManager.instance.chosenScene = sceneName;
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.summaryTab);
    }

    /// <summary>
    /// Event click for opening the musics tab
    /// </summary>
    public void Click_ToMusics()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.musicsTab);
    }
}
