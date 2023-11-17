using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExercicesTab : MainMenuTab
{
    [Header("Exercices Tab")]
    [SerializeField] private Transform exercicesRoot;
    [SerializeField] private GameObject exercicePrefab;
    [SerializeField] private TitleTab titleTab;
    [SerializeField] private ParticipantsTab participantsTab;


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
        MainMenuManager.instance.StartTransition(this, titleTab);
    }

    public void Click_ChooseExercice(Exercice chosenExercice)
    {
        GameManager.instance.currentExercice = chosenExercice;

        MainMenuManager.instance.StartTransition(this, participantsTab);
    }
}
