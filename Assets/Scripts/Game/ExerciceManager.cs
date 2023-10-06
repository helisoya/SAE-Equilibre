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
        yield return new WaitForSeconds(1);

        Movement move;
        Sequence sequence;
        Sequence nextSequence;

        for (int i = 0; i < exercice.sequences.Count; i++)
        {
            sequence = exercice.sequences[i];
            print(sequence.idMovement + " " + sequence.movementTime);
            move = GameManager.instance.GetMovement(sequence.idMovement);


            GameGUI.instance.SetCurrentExericeImg(sequence.movementTime.ToString(), move.animationIcon);

            if (i < exercice.sequences.Count - 1)
            {
                nextSequence = exercice.sequences[i + 1];
                GameGUI.instance.SetNext1ExericeImg(nextSequence.movementTime.ToString(), GameManager.instance.GetMovement(nextSequence.idMovement).animationIcon);
            }
            else
            {
                GameGUI.instance.SetNext1ExericeImg("", defaultMovementIcon);
            }

            if (i < exercice.sequences.Count - 2)
            {
                nextSequence = exercice.sequences[i + 2];
                GameGUI.instance.SetNext2ExericeImg(nextSequence.movementTime.ToString(), GameManager.instance.GetMovement(nextSequence.idMovement).animationIcon);
            }
            else
            {
                GameGUI.instance.SetNext2ExericeImg("", defaultMovementIcon);
            }



            playerAnimator.SetTrigger(move.animationTriggerName);

            yield return new WaitForSeconds(
                move.isContinuous ?
                sequence.movementTime :
                sequence.movementTime * move.animationLength
            );


            yield return new WaitForEndOfFrame();
        }

        GameGUI.instance.SetCurrentExericeImg("", defaultMovementIcon);
        GameGUI.instance.SetNext1ExericeImg("", defaultMovementIcon);
        GameGUI.instance.SetNext2ExericeImg("", defaultMovementIcon);
        playerAnimator.SetTrigger("Win");

    }
}
