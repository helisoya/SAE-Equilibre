using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Sprite defaultMovementIcon;

    void Start()
    {
        StartCoroutine(Routine_Exercice(GameManager.instance.currentExercice));
    }


    IEnumerator Routine_Exercice(Exercice exercice)
    {

        GameGUI.instance.InitializeMovementsUI(exercice);
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

            playerAnimator.SetTrigger(move.animationTriggerName);

            yield return new WaitForSeconds(
                move.isContinuous ?
                sequence.movementTime :
                (float)(sequence.movementTime) * sequence.animationInSeconds
            );
            playerAnimator.speed = 1;
            playerAnimator.SetTrigger("Idle");
            yield return new WaitForSeconds(1);
        }



    }
}
