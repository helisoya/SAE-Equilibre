using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Data")]
    [SerializeField] private UsersDataHandler usersDataHandler;
    [SerializeField] private ExercicesDataHandler exercicesDataHandler;
    [SerializeField] private AppServer server;
    private Dictionary<string, Movement> movements;
    private AudioManager audioManager;


    public IPAddress ipAddress
    {
        get
        {
            return server.address;
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

    public Dictionary<string, IPAddress> possibleAddresses
    {
        get
        {
            return server.addresses;
        }
    }

    public void ChangeServerIP(string key)
    {
        server.SetCurrentAddress(key);
        server.RestartServer();
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

    public List<Movement> GetAllMovements()
    {
        return new List<Movement>(movements.Values);
    }


    public List<Exercice> GetAllExercices()
    {
        return exercicesDataHandler.GetExercices();
    }

    public List<User> GetAllUsers()
    {
        return usersDataHandler.GetUsers();
    }

    public void SaveUsers()
    {
        usersDataHandler.Save();
    }

    public void SaveExercices()
    {
        exercicesDataHandler.Save();
    }

    public void AddSessionToUser(int id, Session session)
    {
        usersDataHandler.AddSessionToUser(id, session);
    }

    public List<string> GetAllMusics()
    {
        return audioManager.GetAvailableMusics();
    }

    public async Task<AudioClip> GetMusic(string musicName)
    {
        return await audioManager.GetClip(musicName);
    }

    public void AddUser(User user)
    {
        usersDataHandler.AddUser(user);
        usersDataHandler.Save();
    }

    public void AddExercice(Exercice exercice)
    {
        exercicesDataHandler.AddExercice(exercice);
        exercicesDataHandler.Save();
    }

    public void RemoveExerice(Exercice exercice)
    {
        exercicesDataHandler.RemoveExercice(exercice);
        exercicesDataHandler.Save();
    }

    public void RemoveUser(User user)
    {
        usersDataHandler.RemoveUser(user);
        usersDataHandler.Save();
    }

}
