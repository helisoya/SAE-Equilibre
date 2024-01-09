using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    [Header("Start")]
    [SerializeField] private TextMeshProUGUI serverIpText;
    [SerializeField] private GameObject startRoot;
    [SerializeField] private ExerciceManager manager;

    [Header("Pause")]
    [SerializeField] private GameObject pauseRoot;

    [Header("End")]
    [SerializeField] private GameObject endRoot;

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

    public bool paused
    {
        get
        {
            return pauseRoot.activeInHierarchy;
        }
    }


    public static GameGUI instance;

    void Awake()
    {
        _startedExercice = false;
        instance = this;
        InitializeMovementsUI(GameManager.instance.currentExercice);
        serverIpText.text += GameManager.instance.ipAddress;
    }


    public void SetStartedExerice(bool value)
    {
        _startedExercice = value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseMenuActive(!paused);
        }
    }

    public void SetPauseMenuActive(bool value)
    {
        if (endRoot.activeInHierarchy || !_startedExercice) return;
        pauseRoot.SetActive(value);
        Time.timeScale = value ? 0 : 1;
    }

    public void ShowEndScreen()
    {
        endRoot.SetActive(true);
        pauseRoot.SetActive(false);
    }

    public void Click_End()
    {
        Time.timeScale = 1;
        GameManager.instance.SaveUsers();
        SceneManager.LoadScene("MainMenu");
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


    public void Click_Continue()
    {
        SetPauseMenuActive(false);
    }
}
