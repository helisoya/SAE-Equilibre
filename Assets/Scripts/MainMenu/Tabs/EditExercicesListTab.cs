using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the Edit Exercice List tab, a variation of the Exercices tab. 
/// The only difference is that this tab links to the edit exercice tab instead of the participants tab
/// You can also create a new exercice from this tab
/// </summary>
public class EditExercicesListTab : ExercicesTab
{
    /// <summary>
    /// Click event for choosing an exercice to edit
    /// </summary>
    /// <param name="chosenExercice">The chosen exercice</param>
    public override void Click_ChooseExercice(Exercice chosenExercice)
    {
        MainMenuManager.instance.editedExercice = chosenExercice;
    }

    /// <summary>
    /// Click event for adding a new exercice
    /// </summary>
    public void Click_AddNewExercice()
    {
        Exercice exercice = new Exercice
        {
            exerciceColor = Color.blue,
            exerciceName = "Session"
        };

        GameManager.instance.AddExercice(exercice);
        Click_ChooseExercice(exercice);
    }
}
