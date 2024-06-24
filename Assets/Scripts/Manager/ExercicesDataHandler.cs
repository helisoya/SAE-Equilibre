using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the exercices data file
/// </summary>
[System.Serializable]
public class ExercicesDataHandler : DataHandler<ExercicesData>
{

    /// <summary>
    /// Returns the loaded exercices
    /// </summary>
    /// <returns>The exercices</returns>
    public List<Exercice> GetExercices()
    {
        return data.exercices;
    }

    /// <summary>
    /// Returns a specific exercice
    /// </summary>
    /// <param name="index">The index of the exercice</param>
    /// <returns>The exercice</returns>
    public Exercice GetExercice(int index)
    {
        if (index < 0 || index >= data.exercices.Count) return null;
        return data.exercices[index];
    }

    /// <summary>
    /// Returns a specific exercice
    /// </summary>
    /// <param name="index">The name of the exercice</param>
    /// <returns>The exercice</returns>
    public Exercice GetExercice(string name)
    {

        foreach (Exercice exercice in data.exercices)
        {
            if (exercice.exerciceName.Equals(name))
            {
                return exercice;
            }
        }
        return null;
    }

    /// <summary>
    /// Register a new exercice
    /// </summary>
    /// <param name="exercice">The new exercice</param>
    public void AddExercice(Exercice exercice)
    {
        data.exercices.Add(exercice);
    }

    /// <summary>
    /// Unregister an existing exercice
    /// </summary>
    /// <param name="exercice">The exercice to unregister</param>
    public void RemoveExercice(Exercice exercice)
    {
        data.exercices.Remove(exercice);
    }

    protected override void CreateDefault()
    {
        data = new ExercicesData();

        Exercice exercice = new Exercice();
        exercice.exerciceName = "Session par défaut";
        exercice.exerciceColor = Color.red;

        exercice.sequences.Add(new Sequence("MARCHE1", 2, 1));
        exercice.sequences.Add(new Sequence("SQUAT1", 3, 3));
        exercice.sequences.Add(new Sequence("BOXEVAR2", 3, 3));
        exercice.sequences.Add(new Sequence("EQUILIBREJAMBES", 8, 1));
        exercice.sequences.Add(new Sequence("BRASHAUT3", 5, 3));
        exercice.sequences.Add(new Sequence("POINTEPIED1", 6, 1));
        exercice.sequences.Add(new Sequence("TALLONFESSES", 7, 2));
        data.exercices.Add(exercice);
    }
}