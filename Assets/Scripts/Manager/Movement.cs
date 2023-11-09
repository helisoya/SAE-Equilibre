using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Movement", order = 0, menuName = "SAE/Movement")]
public class Movement : ScriptableObject
{
    public string ID;
    public string movementName;
    public string animationTriggerName;
    public bool isContinuous;
    public float animationLength;
    public Sprite animationIcon;
    public CameraPosition cameraPosition;
}

public enum CameraPosition
{
    FRONT,
    LEFT,
    RIGHT
}
