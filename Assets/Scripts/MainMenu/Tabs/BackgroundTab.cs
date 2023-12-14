using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void DestroyExistingButtons()
    {
        foreach (Transform child in backgroundsRoot)
        {
            Destroy(child.gameObject);
        }
    }


    public void Click_ChooseBackground(string sceneName)
    {
        MainMenuManager.instance.chosenScene = sceneName;
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.summaryTab);
    }

    public void Click_ToMusics()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.musicsTab);
    }
}
