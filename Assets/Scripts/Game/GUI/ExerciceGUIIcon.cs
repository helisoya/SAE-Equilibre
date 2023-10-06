using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciceGUIIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private Image icon;

    public void Refresh(Sprite sprite, string text)
    {
        textTime.text = text;
        icon.sprite = sprite;
    }
}
