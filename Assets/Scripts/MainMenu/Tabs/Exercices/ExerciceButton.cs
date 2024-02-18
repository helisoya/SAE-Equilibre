using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Represents a button used in the exercices tab to select an exercice
/// </summary>
public class ExerciceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exerciceNameText;
    [SerializeField] private Image colorBand;
    private ExercicesTab tab;
    private Exercice exercice;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="exercice">The linked exercice</param>
    /// <param name="tab">The root tab</param>
    public void Init(Exercice exercice, ExercicesTab tab)
    {
        this.exercice = exercice;
        this.tab = tab;

        exerciceNameText.text = exercice.exerciceName;
        colorBand.color = exercice.exerciceColor;
    }

    /// <summary>
    /// OnClick event
    /// </summary>
    public void Click()
    {
        tab.Click_ChooseExercice(exercice);
    }

    /// <summary>
    /// OnMouseEnter event
    /// </summary>
    public void Enter()
    {
        tab.ShowExerciceInfo(exercice);
    }
}
