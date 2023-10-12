using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    [SerializeField] private ExerciceGUIIcon exerciceImg_current;
    [SerializeField] private ExerciceGUIIcon exerciceImg_next1;
    [SerializeField] private ExerciceGUIIcon exerciceImg_next2;


    [Header("Movements")]
    [SerializeField] private Transform movementsRoot;
    [SerializeField] private GameObject movementIconPrefab;
    [SerializeField] private GameObject movementLinePrefab;
    private float stopAtX = 400;
    private float startSpawnAt = 850;
    private float oneSecondEqualsInUnits = 50;

    private bool _startedExercice = false;


    public float timeBetweenFirstIconAndStop
    {
        get
        {
            return (startSpawnAt - stopAtX) / oneSecondEqualsInUnits;
        }
    }


    public bool startedExercice
    {
        get { return _startedExercice; }
    }


    public static GameGUI instance;

    void Awake()
    {
        _startedExercice = false;
        instance = this;
    }

    public void SetStartedExerice(bool value)
    {
        _startedExercice = value;
    }

    public void InitializeMovementsUI(Exercice exercice)
    {
        float currentX = startSpawnAt;
        Movement move;
        GameExerciceIcon icon;
        float distanceToNext;
        foreach (Sequence sequence in exercice.sequences)
        {
            move = GameManager.instance.GetMovement(sequence.idMovement);


            if (move.isContinuous)
            {
                icon = Instantiate(movementIconPrefab, movementsRoot).GetComponent<GameExerciceIcon>();
                icon.Init(oneSecondEqualsInUnits, stopAtX, move.animationIcon, currentX);

                distanceToNext = (float)(sequence.movementTime) * oneSecondEqualsInUnits;
                icon.InitTrail(distanceToNext, sequence.movementTime);

                currentX += distanceToNext + oneSecondEqualsInUnits;
            }
            else
            {
                for (int i = 0; i < sequence.movementTime; i++)
                {
                    icon = Instantiate(movementIconPrefab, movementsRoot).GetComponent<GameExerciceIcon>();
                    icon.Init(oneSecondEqualsInUnits, stopAtX, move.animationIcon, currentX);

                    distanceToNext = move.animationLength * oneSecondEqualsInUnits;
                    currentX += distanceToNext + (i == sequence.movementTime - 1 ? oneSecondEqualsInUnits : 0);
                }
            }
        }
    }

    public void SetCurrentExericeImg(string time, Sprite sprite)
    {
        exerciceImg_current.Refresh(sprite, time);
    }

    public void SetNext1ExericeImg(string time, Sprite sprite)
    {
        exerciceImg_next1.Refresh(sprite, time);
    }

    public void SetNext2ExericeImg(string time, Sprite sprite)
    {
        exerciceImg_next2.Refresh(sprite, time);
    }
}
