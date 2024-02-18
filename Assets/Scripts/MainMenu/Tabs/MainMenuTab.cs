using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a tab/view of the main menu
/// </summary>
public class MainMenuTab : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] protected GameObject root;

    /// <summary>
    /// Opens the tab
    /// </summary>
    public virtual void Open()
    {
        root.SetActive(true);
    }

    /// <summary>
    /// Closes the tab
    /// </summary>
    public virtual void Close()
    {
        root.SetActive(false);
    }
}
