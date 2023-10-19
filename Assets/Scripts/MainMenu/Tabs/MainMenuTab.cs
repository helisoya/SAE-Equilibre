using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTab : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] protected GameObject root;


    public virtual void Open()
    {
        root.SetActive(true);
    }


    public virtual void Close()
    {
        root.SetActive(false);
    }
}
