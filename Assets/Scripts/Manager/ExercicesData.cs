using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the data file containing the exercices
/// </summary>
[System.Serializable]
public class ExercicesData : DataFile
{
    public List<Exercice> exercices;

    public ExercicesData()
    {
        exercices = new List<Exercice>();
    }
}


/// <summary>
/// Represents an exercice
/// </summary>
[System.Serializable]
public class Exercice
{
    public string exerciceName;
    public Color exerciceColor;
    public List<Sequence> sequences;

    public Exercice()
    {
        sequences = new List<Sequence>();
    }
}

/// <summary>
/// Represents a sequence of the exercice
/// </summary>
[System.Serializable]
public class Sequence
{
    public string idMovement;
    public int movementTime;
    public float animationInSeconds;

    public Sequence() : this("NONE", 0, 0) { }

    public Sequence(string id, int time, float timeSeconds)
    {
        idMovement = id;
        movementTime = time;
        animationInSeconds = timeSeconds;
    }
}