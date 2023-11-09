using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    [Header("Start")]
    [SerializeField] private TextMeshProUGUI serverIpText;
    [SerializeField] private GameObject startRoot;
    [SerializeField] private ExerciceManager manager;


    [Header("Movements")]
    [SerializeField] private Transform movementsRoot;
    [SerializeField] private GameObject movementIconPrefab;
    [SerializeField] private float oneSecondEqualsInUnits = 50;
    private readonly float stopAtX = 400;
    private readonly float startSpawnAt = 850;


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
        serverIpText.text += GameManager.instance.ipAddress;
        InitializeMovementsUI(GameManager.instance.currentExercice);
    }

    public void SetStartedExerice(bool value)
    {
        _startedExercice = value;
    }


    public void Click_Start()
    {
        startRoot.SetActive(false);
        manager.StartExercice();
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

                distanceToNext = sequence.movementTime * oneSecondEqualsInUnits;
                icon.InitTrail(distanceToNext, sequence.movementTime);

                currentX += distanceToNext + oneSecondEqualsInUnits;
            }
            else
            {
                for (int i = 0; i < sequence.movementTime; i++)
                {
                    icon = Instantiate(movementIconPrefab, movementsRoot).GetComponent<GameExerciceIcon>();
                    icon.Init(oneSecondEqualsInUnits, stopAtX, move.animationIcon, currentX);

                    distanceToNext = sequence.animationInSeconds * oneSecondEqualsInUnits;
                    currentX += distanceToNext + (i == sequence.movementTime - 1 ? oneSecondEqualsInUnits : 0);
                }
            }
        }
    }
}
