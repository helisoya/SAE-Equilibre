using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BackgroundButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI backgroundNameText;
    [SerializeField] private Image backgroundImg;
    private BackgroundTab tab;
    private Background background;

    public void Init(Background background, BackgroundTab tab)
    {
        this.background = background;
        this.tab = tab;

        backgroundNameText.text = background.backgroundName;
        backgroundImg.sprite = background.backgroundImg;
    }

    public void Click()
    {
        tab.Click_ChooseBackground(background.sceneName);
    }
}
