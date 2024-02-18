using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a cell of the grid that only shows text
/// </summary>
public class FormDescriptionCase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    /// <summary>
    /// Initialize the cell
    /// </summary>
    /// <param name="text">The cell's label</param>
    public void Init(string text)
    {
        label.text = text;
    }
}
