using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ExercicesData
{
    public List<Exercice> exercices;

    public ExercicesData()
    {
        exercices = new List<Exercice>();
    }
}



[System.Serializable]
public class Exercice
{
    public string exerciceName;
    public List<Sequence> sequences;

    public Exercice()
    {
        sequences = new List<Sequence>();
    }
}


[System.Serializable]
public class Sequence
{
    public string idMovement;
    public int movementTime;

    public Sequence() : this("NONE", 0) { }

    public Sequence(string id, int time)
    {
        idMovement = id;
        movementTime = time;
    }
}