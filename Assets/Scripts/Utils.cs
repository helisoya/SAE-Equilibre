using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Utilitary class
/// </summary>
public class Utils
{
    /// <summary>
    /// Destroys the children of a given parent
    /// </summary>
    /// <param name="parent">The parent</param>
    public static void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
