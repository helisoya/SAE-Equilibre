using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

/// <summary>
/// Repreesnts a movement
/// </summary>
[CreateAssetMenu(fileName = "Movement", order = 0, menuName = "SAE/Movement")]
public class Movement : ScriptableObject
{
    [Header("Critical Informations (Modify with caution)")]
    public string ID;
    public string animationTriggerName;
    public bool isContinuous;
    public float animationLength;

    [Header("Other informations")]
    public string movementName;
    public Sprite animationIcon;
    public CameraPosition cameraPosition;
}

/// <summary>
/// Represents the possible camera positions
/// </summary>
public enum CameraPosition
{
    FRONT,
    LEFT,
    RIGHT
}
