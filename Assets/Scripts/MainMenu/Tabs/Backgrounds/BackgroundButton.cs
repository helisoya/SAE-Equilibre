using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Represents a button on the Background tab, used to select a background
/// </summary>
public class BackgroundButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI backgroundNameText;
    [SerializeField] private Image backgroundImg;
    private BackgroundTab tab;
    private Background background;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="background">The linked background</param>
    /// <param name="tab">The root tab</param>
    public void Init(Background background, BackgroundTab tab)
    {
        this.background = background;
        this.tab = tab;

        backgroundNameText.text = background.backgroundName;
        backgroundImg.sprite = background.backgroundImg;
    }

    /// <summary>
    /// OnClick event
    /// </summary>
    public void Click()
    {
        tab.Click_ChooseBackground(background.sceneName);
    }
}
