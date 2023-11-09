using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Data")]
    [SerializeField] private UsersDataHandler usersDataHandler;
    [SerializeField] private ExercicesDataHandler exercicesDataHandler;
    [SerializeField] private AppServer server;
    private Dictionary<string, Movement> movements;
    private AudioManager audioManager;


    public string ipAddress
    {
        get
        {
            return server.GetLocalIPAddress().ToString();
        }
    }

    private string _currentMusic;
    public string currentMusic
    {
        get
        {
            return _currentMusic;
        }
        set
        {
            _currentMusic = value;
        }
    }

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

            if (audioManager == null)
            {
                audioManager = new AudioManager();
            }

            server.InitServer();
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


    public List<string> GetAllMusics()
    {
        return audioManager.GetAvailableMusics();
    }

    public async Task<AudioClip> GetMusic(string musicName)
    {
        return await audioManager.GetClip(musicName);
    }

}
