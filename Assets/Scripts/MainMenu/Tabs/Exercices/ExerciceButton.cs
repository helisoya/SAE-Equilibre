using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExerciceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exerciceNameText;
    private ExercicesTab tab;
    private Exercice exercice;

    public void Init(Exercice exercice, ExercicesTab tab)
    {
        this.exercice = exercice;
        this.tab = tab;

        exerciceNameText.text = exercice.exerciceName;
    }

    public void Click()
    {
        tab.Click_ChooseExercice(exercice);
    }
}
