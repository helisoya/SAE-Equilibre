using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents the edit exercice tab, where the user can edit an exercice
/// </summary>
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

    /// <summary>
    /// Destroys all the created prefabs in the tab
    /// </summary>
    void ClearAllPrefabs()
    {
        Utils.DestroyChildren(currentMovesRoot);
        Utils.DestroyChildren(availableMovesRoot);
    }

    public override void Close()
    {
        base.Close();
        ClearAllPrefabs();
        GameManager.instance.SaveExercices();
    }

    /// <summary>
    /// Event for changing the color of the exercice
    /// </summary>
    public void Event_ChangeColor()
    {
        exercice.exerciceColor = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value);
        colorImg.color = exercice.exerciceColor;
    }

    /// <summary>
    /// Event for changing the name of the exercice
    /// </summary>
    public void Event_ChangeName()
    {
        exercice.exerciceName = exerciceName.text;
    }

    /// <summary>
    /// Event Click for changing if the color picker is shown or not 
    /// </summary>
    /// <param name="value">Is the colour picker shown ?</param>
    public void Click_SetColorPickerActive(bool value)
    {
        colorPickerObj.SetActive(value);
    }

    /// <summary>
    /// Event click for adding a new movement to the exercice
    /// </summary>
    /// <param name="move">The new movement</param>
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

    /// <summary>
    /// Moves a sequence in the list
    /// </summary>
    /// <param name="sequence">The sequence to move</param>
    /// <param name="side">Where should the movement be moved</param>
    public void Click_MoveSequence(Sequence sequence, int side)
    {
        int indexSeq = exercice.sequences.IndexOf(sequence);

        if (indexSeq == -1 || (indexSeq == 0 && side == -1) || (indexSeq == exercice.sequences.Count - 1 && side == 1)) return;

        exercice.sequences[indexSeq] = exercice.sequences[indexSeq + side];
        exercice.sequences[indexSeq + side] = sequence;

        currentMovesRoot.GetChild(indexSeq).SetSiblingIndex(indexSeq + side);
    }

    /// <summary>
    /// Changes the preview
    /// </summary>
    /// <param name="triggerName">The animation's trigger</param>
    /// <param name="animationSpeed">The animation's speed</param>
    public void SetPreview(string triggerName, float animationSpeed)
    {
        demoAnimator.speed = animationSpeed;
        demoAnimator.CrossFade(triggerName, 0.1f);
    }

    /// <summary>
    /// Deletes a sequence from an exercice
    /// </summary>
    /// <param name="sequence">The sequence</param>
    /// <param name="component">Component corresponding to the sequence</param>
    public void DeleteSequence(Sequence sequence, GameObject component)
    {
        Destroy(component);
        exercice.sequences.Remove(sequence);

        currentMovesRoot.GetComponent<RectTransform>().sizeDelta -= new Vector2(
            0,
            currentMovePrefab.GetComponent<RectTransform>().sizeDelta.y + 5
        );
    }

    /// <summary>
    /// Click event for opening the edit exercice list tab
    /// </summary>
    public void Click_ToExercicesList()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.editExercicesListTab);
    }

    /// <summary>
    /// Click event for deleting the current exercice
    /// </summary>
    public void Click_DeleteExercice()
    {
        GameManager.instance.RemoveExerice(exercice);
        Click_ToExercicesList();
    }
}
