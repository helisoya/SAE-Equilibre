using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExerciceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exerciceNameText;
    [SerializeField] private Image colorBand;
    private ExercicesTab tab;
    private Exercice exercice;

    public void Init(Exercice exercice, ExercicesTab tab)
    {
        this.exercice = exercice;
        this.tab = tab;

        exerciceNameText.text = exercice.exerciceName;
        colorBand.color = exercice.exerciceColor;
    }

    public void Click()
    {
        tab.Click_ChooseExercice(exercice);
    }
}
