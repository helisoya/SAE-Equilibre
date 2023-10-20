using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class DataHandler
{
    [SerializeField] protected string dataSavePath;

    public abstract void Save();


    public abstract void Load();
}
