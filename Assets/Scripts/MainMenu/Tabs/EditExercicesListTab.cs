using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditExercicesListTab : ExercicesTab
{
    public override void Click_ChooseExercice(Exercice chosenExercice)
    {
        MainMenuManager.instance.editedExercice = chosenExercice;
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.editExerciceTab);
    }

    public void Click_AddNewExercice()
    {
        Exercice exercice = new Exercice();
        exercice.exerciceColor = Color.blue;
        exercice.exerciceName = "Session";

        GameManager.instance.AddExercice(exercice);
        Click_ChooseExercice(exercice);
    }
}
