using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExercicesDataHandler : DataHandler
{
    [SerializeField] private ExercicesData data;

    public override void Save()
    {
        FileManager.SaveJSON(FileManager.savPath + dataSavePath, data);
    }


    public override void Load()
    {
        if (System.IO.File.Exists(FileManager.savPath + dataSavePath))
        {
            data = FileManager.LoadJSON<ExercicesData>(FileManager.savPath + dataSavePath);
        }
        else
        {
            Save();
        }
    }


    public List<Exercice> GetExercices()
    {
        return data.exercices;
    }

    public Exercice GetExercice(int index)
    {
        return data.exercices[index];
    }

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

    public void AddExercice(Exercice exercice)
    {
        data.exercices.Add(exercice);
    }

    public void RemoveExercice(Exercice exercice)
    {
        data.exercices.Remove(exercice);
    }
}
