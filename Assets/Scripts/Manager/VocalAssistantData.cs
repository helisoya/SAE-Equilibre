using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio data for the vocal assistant
/// </summary>
[CreateAssetMenu(fileName = "VocalAssistantData", order = 1, menuName = "SAE/VocalAssistantData")]
public class VocalAssistantData : ScriptableObject
{
    public AudioClip turnOnClip;
    public AudioClip turnOffClip;
    public AudioClip titleScreenClip;
    public AudioClip optionsClip;
    public AudioClip creditsClip;
    public AudioClip exerciceListClip;
    public AudioClip editExerciceClip;
    public AudioClip userListClip;
    public AudioClip participantClip;
    public AudioClip backgroundClip;
    public AudioClip musicListClip;
    public AudioClip summaryClip;
    public AudioClip startExerciceClip;
    public AudioClip endExerciceClip;
    public AudioClip exerciceMusicClip;
}
