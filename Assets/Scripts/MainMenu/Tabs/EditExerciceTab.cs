using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class EditExerciceTab : MainMenuTab
{
    [Header("Edit Exercice Color")]
    [SerializeField] private Image colorImg;
    [SerializeField] private Slider sliderRed;
    [SerializeField] private Slider sliderGreen;
    [SerializeField] private Slider sliderBlue;
    [SerializeField] private GameObject colorPickerObj;


    [Header("Edit Exercice Tab")]
    [SerializeField] private TMP_InputField exerciceName;
    [SerializeField] private Transform currentMovesRoot;
    [SerializeField] private GameObject currentMovePrefab;
    [SerializeField] private Transform availableMovesRoot;
    [SerializeField] private GameObject availableMovePrefab;
    [SerializeField] private Animator demoAnimator;

    private Exercice exercice;


    public override void Open()
    {
        base.Open();
        ClearAllPrefabs();

        exercice = MainMenuManager.instance.editedExercice;

        exerciceName.SetTextWithoutNotify(exercice.exerciceName);

        colorImg.color = exercice.exerciceColor;
        sliderRed.SetValueWithoutNotify(exercice.exerciceColor.r);
        sliderGreen.SetValueWithoutNotify(exercice.exerciceColor.g);
        sliderBlue.SetValueWithoutNotify(exercice.exerciceColor.b);

        List<Movement> moves = GameManager.instance.GetAllMovements();

        foreach (Movement move in moves)
        {
            Instantiate(availableMovePrefab, availableMovesRoot).GetComponent<MoveButton>().Init(move, this);
        }

        availableMovesRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            availableMovesRoot.GetComponent<RectTransform>().sizeDelta.x,
            (availableMovePrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * moves.Count
        );


        foreach (Sequence sequence in exercice.sequences)
        {
            Instantiate(currentMovePrefab, currentMovesRoot).GetComponent<CurrentMoveComponent>().Init(
                sequence,
                GameManager.instance.GetMovement(sequence.idMovement),
                this);
        }

        currentMovesRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
        currentMovesRoot.GetComponent<RectTransform>().sizeDelta.x,
        (currentMovePrefab.GetComponent<RectTransform>().sizeDelta.y + 5) * exercice.sequences.Count
        );
    }


    void ClearAllPrefabs()
    {
        foreach (Transform child in currentMovesRoot)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in availableMovesRoot)
        {
            Destroy(child.gameObject);
        }
    }

    public override void Close()
    {
        base.Close();
        ClearAllPrefabs();
        GameManager.instance.SaveExercices();
    }

    public void Event_ChangeColor()
    {
        exercice.exerciceColor = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);
        colorImg.color = exercice.exerciceColor;
    }

    public void Event_ChangeName()
    {
        exercice.exerciceName = exerciceName.text;
    }

    public void Click_SetColorPickerActive(bool value)
    {
        colorPickerObj.SetActive(value);
    }

    public void Click_AddMovement(Movement move)
    {

        Sequence sequence = new Sequence(move.ID, 1, 1);
        exercice.sequences.Add(sequence);

        Instantiate(currentMovePrefab, currentMovesRoot).GetComponent<CurrentMoveComponent>().Init(sequence, move, this);

        currentMovesRoot.GetComponent<RectTransform>().sizeDelta += new Vector2(
            0,
            currentMovePrefab.GetComponent<RectTransform>().sizeDelta.y + 5
        );
    }

    public void Click_ToExercicesList()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.editExercicesListTab);
    }

    public void Click_DeleteExercice()
    {
        GameManager.instance.RemoveExerice(exercice);
        Click_ToExercicesList();
    }

    public void SetPreview(string triggerName, float animationSpeed)
    {
        demoAnimator.speed = animationSpeed;
        demoAnimator.CrossFade(triggerName, 0.1f);
    }

    public void DeleteSequence(Sequence sequence, GameObject component)
    {
        Destroy(component);
        exercice.sequences.Remove(sequence);

        currentMovesRoot.GetComponent<RectTransform>().sizeDelta -= new Vector2(
            0,
            currentMovePrefab.GetComponent<RectTransform>().sizeDelta.y + 5
        );
    }

}
