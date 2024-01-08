using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                    : sequence.movementTime + " " + move.movementName + " de " + sequence.animationInSeconds);

                exerciceLenth += move.isContinuous ? sequence.movementTime : sequence.movementTime * sequence.animationInSeconds;
                exerciceLenth += 1;
            }

            exerciceLengthText.text = "Temps total : " + exerciceLenth + " secondes";
        }
    }

    public void Click_ToTitle()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.titleTab);
    }

    public virtual void Click_ChooseExercice(Exercice chosenExercice)
    {
        GameManager.instance.currentExercice = chosenExercice;

        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.participantsTab);
    }
}
