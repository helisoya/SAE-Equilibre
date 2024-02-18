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
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource musicSource;

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

        musicSource.clip = await GameManager.instance.GetMusic(music);
        print("found clip");
        musicSource.Play();
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

            playerAnimator.speed = move.isContinuous ? 1 : move.animationLength / sequence.animationInSeconds;

            RefreshCams(move.cameraPosition);
            playerAnimator.CrossFade(move.animationTriggerName, 0.1f);

            yield return new WaitForSeconds(
                move.isContinuous ?
                sequence.movementTime :
                (float)(sequence.movementTime) * sequence.animationInSeconds
            );
            playerAnimator.speed = 1;
            playerAnimator.CrossFade("Idle", 0.1f);
            RefreshCams(CameraPosition.FRONT);

            yield return new WaitForSeconds(1);
        }

        GameGUI.instance.ShowEndScreen();
    }
}
