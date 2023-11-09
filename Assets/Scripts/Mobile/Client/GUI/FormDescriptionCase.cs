using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FormDescriptionCase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void Init(string text)
    {
        label.text = text;
    }
}
