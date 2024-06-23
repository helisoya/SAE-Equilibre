using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the exercices tab, where you can select an exercice to play
/// </summary>
public class ExercicesTab : MainMenuTab
{
    [Header("Exercices Tab")]
    [SerializeField] private Transform exercicesRoot;
    [SerializeField] private GameObject exercicePrefab;

    [Header("Exercice Info")]
    [SerializeField] private GameObject exerciceInfoRoot;
    [SerializeField] private Image bandImage;
    [SerializeField] private TextMeshProUGUI exerciceNameText;
    [SerializeField] private Transform exerciceMovesRoot;
    [SerializeField] private GameObject prefabMoveText;
    [SerializeField] private TextMeshProUGUI exerciceLengthText;


    public override void Open()
    {
        base.Open();
        Utils.DestroyChildren(exercicesRoot);


        List<Exercice> exercices = GameManager.instance.GetAllExercices();
        foreach (Exercice exercice in exercices)
        {
            Instantiate(exercicePrefab, exercicesRoot).GetComponent<ExerciceButton>().Init(exercice, this);
        }

        exercicesRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            exercicesRoot.GetComponent<RectTransform>().sizeDelta.x,
            (exercicePrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * exercices.Count
        );

        ShowExerciceInfo(null);
    }

    public override void Close()
    {
        base.Close();
        Utils.DestroyChildren(exercicesRoot);
    }

    /// <summary>
    /// Shows informations about an exercice
    /// </summary>
    /// <param name="exercice">The exercice</param>
    public void ShowExerciceInfo(Exercice exercice)
    {
        if (exercice == null)
        {
            exerciceInfoRoot.SetActive(false);
        }
        else
        {
            exerciceInfoRoot.SetActive(true);
            bandImage.color = exercice.exerciceColor;
            exerciceNameText.text = exercice.exerciceName;
            Utils.DestroyChildren(exerciceMovesRoot);

            Movement move;
            float exerciceLenth = 1;
            foreach (Sequence sequence in exercice.sequences)
            {
                move = GameManager.instance.GetMovement(sequence.idMovement);
                Instantiate(prefabMoveText, exerciceMovesRoot).GetComponent<TextMeshProUGUI>().text =
                    "- " + (move.isContinuous ? move.movementName + " pendant " + sequence.movementTime + " secondes"
                    : sequence.movementTime + " " + move.movementName + " de " + sequence.animationInSeconds + " secondes");

                exerciceLenth += move.isContinuous ? sequence.movementTime : sequence.movementTime * sequence.animationInSeconds;
                exerciceLenth += 1;
            }

            exerciceMovesRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
                exerciceMovesRoot.GetComponent<RectTransform>().sizeDelta.x,
                (prefabMoveText.GetComponent<RectTransform>().sizeDelta.y + 5) * exercice.sequences.Count
            );

            exerciceLengthText.text = "Temps total : " + exerciceLenth + " secondes";
        }
    }

    /// <summary>
    /// Click event for the button that opens the title tab
    /// </summary>
    public void Click_ToTitle()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }

    /// <summary>
    /// Click event for the button that opens the participants tab
    /// </summary>
    /// <param name="chosenExercice">The chosen exercice</param>
    public virtual void Click_ChooseExercice(Exercice chosenExercice)
    {
        GameManager.instance.currentExercice = chosenExercice;

        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.participantsTab);
    }
}
