using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Data")]
    [SerializeField] private UsersDataHandler usersDataHandler;
    [SerializeField] private ExercicesDataHandler exercicesDataHandler;
    private Dictionary<string, Movement> movements;


    private List<User> _participants;
    public List<User> participants
    {
        get
        {
            return _participants;
        }
        set
        {
            _participants = value;
        }
    }

    private Exercice _currentExercice;
    public Exercice currentExercice
    {
        get
        {
            return _currentExercice;
        }
        set
        {
            _currentExercice = value;
        }
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadMovements();

            usersDataHandler.Load();
            exercicesDataHandler.Load();
            _currentExercice = exercicesDataHandler.GetExercice(0);

            participants = new List<User>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Movement GetMovement(string moveName)
    {
        if (movements.ContainsKey(moveName))
        {
            return movements[moveName];
        }
        return null;
    }


    void LoadMovements()
    {
        movements = new Dictionary<string, Movement>();

        Movement[] loadedMoves = Resources.LoadAll<Movement>("Movements");

        foreach (Movement loadedMove in loadedMoves)
        {
            movements.Add(loadedMove.ID, loadedMove);
        }
    }



    public List<Exercice> GetAllExercices()
    {
        return exercicesDataHandler.GetExercices();
    }

    public List<User> GetAllUsers()
    {
        return usersDataHandler.GetUsers();
    }



}
