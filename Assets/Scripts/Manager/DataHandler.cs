using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler for a data file
/// </summary>
[System.Serializable]
public abstract class DataHandler
{
    [SerializeField] protected string dataSavePath;

    /// <summary>
    /// Saves data
    /// </summary>
    public abstract void Save();

    /// <summary>
    /// Loads data
    /// </summary>
    public abstract void Load();
}
