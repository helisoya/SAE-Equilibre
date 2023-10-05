using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciceManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    void Start()
    {
        StartCoroutine(Routine_Exercice(GameManager.instance.currentExercice));
    }


    IEnumerator Routine_Exercice(Exercice exercice)
    {
        yield return new WaitForSeconds(1);

        Movement move;
        foreach (Sequence sequence in exercice.sequences)
        {
            print(sequence.idMovement + " " + sequence.movementTime);
            move = GameManager.instance.GetMovement(sequence.idMovement);


            playerAnimator.SetTrigger(move.animationTriggerName);

            yield return new WaitForSeconds(
                move.isContinuous ?
                sequence.movementTime :
                sequence.movementTime * move.animationLength
            );


            yield return new WaitForEndOfFrame();
        }

        playerAnimator.SetTrigger("Win");

    }
}
