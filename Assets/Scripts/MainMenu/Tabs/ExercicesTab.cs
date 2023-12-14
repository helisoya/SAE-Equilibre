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
    [SerializeField] private Image bandImage;
    [SerializeField] private TextMeshProUGUI exerciceNameText;
    [SerializeField] private Transform exerciceMovesRoot;
    [SerializeField] private GameObject prefabMoveText;
    [SerializeField] private TextMeshProUGUI exerciceLengthText;


    void DestroyExistingButtons()
    {
        foreach (Transform child in exercicesRoot)
        {
            Destroy(child.gameObject);
        }
    }


    public override void Open()
    {
        base.Open();
        DestroyExistingButtons();


        List<Exercice> exercices = GameManager.instance.GetAllExercices();
        foreach (Exercice exercice in exercices)
        {
            Instantiate(exercicePrefab, exercicesRoot).GetComponent<ExerciceButton>().Init(exercice, this);
        }

        exercicesRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            exercicesRoot.GetComponent<RectTransform>().sizeDelta.x,
            (exercicePrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * exercices.Count
        );
    }

    public override void Close()
    {
        base.Close();
        DestroyExistingButtons();
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
