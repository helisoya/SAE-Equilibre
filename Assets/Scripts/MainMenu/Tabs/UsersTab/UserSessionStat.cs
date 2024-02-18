using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a user's stats regarding to a specific movement
/// </summary>
public class UserSessionStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    [SerializeField] private Image imageFill;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="stats">The user's stats</param>
    public void Init(UserMoveStats stats)
    {
        moveNameText.text = stats.moveName;
        imageFill.fillAmount = (float)(stats.success) / (float)(stats.total);
    }
}
