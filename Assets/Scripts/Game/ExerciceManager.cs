using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Manages the process of an exercice
/// </summary>
public class ExerciceManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator[] playerAnimators;

    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera frontCam;
    [SerializeField] private CinemachineVirtualCamera leftCam;
    [SerializeField] private CinemachineVirtualCamera rightCam;

    /// <summary>
    /// Starts the selected exercice
    /// </summary>
    public void StartExercice()
    {
        StartCoroutine(Routine_Exercice(GameManager.instance.currentExercice));
    }

    /// <summary>
    /// Loads the selected music
    /// </summary>
    /// <param name="music">The music's name</param>
    async void LoadMusic(string music)
    {
        if (music == null) return;

        AudioClip clip = await GameManager.instance.GetMusic(music);
        print("found clip");
        GameManager.instance.GetAudioManager().PlayBGM(clip);
    }

    /// <summary>
    /// Refreshes the camera's position
    /// </summary>
    /// <param name="position">The camera's position</param>
    void RefreshCams(CameraPosition position)
    {
        frontCam.Priority = position == CameraPosition.FRONT ? 20 : 10;
        leftCam.Priority = position == CameraPosition.LEFT ? 20 : 10;
        rightCam.Priority = position == CameraPosition.RIGHT ? 20 : 10;
    }

    /// <summary>
    /// Routine for the exercice
    /// </summary>
    /// <param name="exercice">The current exercice</param>
    /// <returns>IEnumerator</returns>
    IEnumerator Routine_Exercice(Exercice exercice)
    {
        RefreshCams(CameraPosition.FRONT);
        LoadMusic(GameManager.instance.currentMusic);
        GameGUI.instance.SetStartedExerice(true);
        yield return new WaitForSeconds(GameGUI.instance.timeBetweenFirstIconAndStop);

        Movement move;
        Sequence sequence;

        for (int i = 0; i < exercice.sequences.Count; i++)
        {
            sequence = exercice.sequences[i];
            print(sequence.idMovement + " " + sequence.movementTime);
            move = GameManager.instance.GetMovement(sequence.idMovement);

            SetAnimatorsSpeed(move.isContinuous ? 1 : move.animationLength / sequence.animationInSeconds);

            RefreshCams(move.cameraPosition);
            CrossFadeAnimators(move.animationTriggerName);

            yield return new WaitForSeconds(
                move.isContinuous ?
                sequence.movementTime :
                sequence.movementTime * sequence.animationInSeconds
            );
            SetAnimatorsSpeed(1);
            CrossFadeAnimators("Idle");
            RefreshCams(CameraPosition.FRONT);

            yield return new WaitForSeconds(1);
        }

        GameGUI.instance.ShowEndScreen();
    }

    /// <summary>
    /// Sets the animation speed of the animators
    /// </summary>
    /// <param name="speed">The new speed</param>
    private void SetAnimatorsSpeed(float speed)
    {
        foreach (Animator animator in playerAnimators)
        {
            animator.speed = speed;
        }
    }

    /// <summary>
    /// Crossfades the animators to a new state (animation)
    /// </summary>
    /// <param name="stateName">The state's name</param>
    /// <param name="normalizedTransitionTime">The normalized transition time (0.1f by default)</param>
    private void CrossFadeAnimators(string stateName, float normalizedTransitionTime = 0.1f)
    {
        foreach (Animator animator in playerAnimators)
        {
            animator.CrossFade(stateName, normalizedTransitionTime);
        }
    }
}
