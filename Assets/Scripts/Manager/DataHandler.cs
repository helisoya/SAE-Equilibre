using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler for a data file
/// </summary>
[System.Serializable]
public class DataHandler<T>
{
    [SerializeField] protected string dataSavePath;
    protected T data;

    /// <summary>
    /// Saves data
    /// </summary>
    public void Save()
    {
        FileManager.SaveJSON(FileManager.savPath + dataSavePath, data);
    }

    /// <summary>
    /// Loads data
    /// </summary>
    public void Load()
    {
        if (System.IO.File.Exists(FileManager.savPath + dataSavePath))
        {
            data = FileManager.LoadJSON<T>(FileManager.savPath + dataSavePath);
        }
        else
        {
            CreateDefault();
            Save();
        }
    }

    /// <summary>
    /// Initialize and empty datafile
    /// </summary>
    protected virtual void CreateDefault()
    {
    }
}
