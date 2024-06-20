using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;

/// <summary>
/// Persistent class handling the various datas of the software
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Data")]
    [SerializeField] private UsersDataHandler usersDataHandler;
    [SerializeField] private ExercicesDataHandler exercicesDataHandler;
    [SerializeField] private AppServer server;
    private Dictionary<string, Movement> movements;
    [SerializeField] private AudioManager audioManager;


    public IPAddress ipAddress
    {
        get
        {
            return server.address;
        }
    }

    public string currentMusic { get; set; }

    public List<User> participants { get; set; }

    public Exercice currentExercice { get; set; }

    public Dictionary<string, IPAddress> possibleAddresses
    {
        get
        {
            return server.addresses;
        }
    }

    /// <summary>
    /// Changes the current IP address in use
    /// </summary>
    /// <param name="key">The key to the new IP address/param>
    public void ChangeServerIP(string key)
    {
        server.SetCurrentAddress(key);
        server.RestartServer();
    }

    /// <summary>
    /// Initialize the GameManager, or delete it if one has already done so
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadMovements();

            usersDataHandler.Load();
            exercicesDataHandler.Load();
            currentExercice = exercicesDataHandler.GetExercice(0);

            participants = new List<User>();

            server.InitServer();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Returns the Audio Manager
    /// </summary>
    /// <returns>The Audio Manager</returns>
    public AudioManager GetAudioManager()
    {
        return audioManager;
    }

    /// <summary>
    /// Returns a movement
    /// </summary>
    /// <param name="moveName">The movement's name</param>
    /// <returns>The movement</returns>
    public Movement GetMovement(string moveName)
    {
        if (movements.ContainsKey(moveName))
        {
            return movements[moveName];
        }
        return null;
    }

    /// <summary>
    /// Loads the available movements
    /// </summary>
    void LoadMovements()
    {
        movements = new Dictionary<string, Movement>();

        Movement[] loadedMoves = Resources.LoadAll<Movement>("Movements");

        foreach (Movement loadedMove in loadedMoves)
        {
            movements.Add(loadedMove.ID, loadedMove);
        }
    }

    /// <summary>
    /// Returns all the available movements
    /// </summary>
    /// <returns>The available movements</returns>
    public List<Movement> GetAllMovements()
    {
        return new List<Movement>(movements.Values);
    }

    /// <summary>
    /// Returns all the available exercices
    /// </summary>
    /// <returns>The available exercices</returns>
    public List<Exercice> GetAllExercices()
    {
        return exercicesDataHandler.GetExercices();
    }

    /// <summary>
    /// Returns all the available users
    /// </summary>
    /// <returns>The available users</returns>
    public List<User> GetAllUsers()
    {
        return usersDataHandler.GetUsers();
    }

    /// <summary>
    /// Saves the current users
    /// </summary>
    public void SaveUsers()
    {
        usersDataHandler.Save();
    }

    /// <summary>
    /// Saves the current exercices
    /// </summary>
    public void SaveExercices()
    {
        exercicesDataHandler.Save();
    }

    /// <summary>
    /// Adds a session to a user
    /// </summary>
    /// <param name="id">The user's ID</param>
    /// <param name="session">The session to add</param>
    public void AddSessionToUser(int id, Session session)
    {
        usersDataHandler.AddSessionToUser(id, session);
    }

    /// <summary>
    /// Returns all the available musics name
    /// </summary>
    /// <returns>The available musics name</returns>
    public List<string> GetAllMusics()
    {
        return audioManager.GetAvailableMusics();
    }

    /// <summary>
    /// Returns a music
    /// </summary>
    /// <param name="musicName">The music's name</param>
    /// <returns>The music's clip</returns>
    public async Task<AudioClip> GetMusic(string musicName)
    {
        return await audioManager.GetClip(musicName);
    }

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="user">The user to register</param>
    public void AddUser(User user)
    {
        usersDataHandler.AddUser(user);
        usersDataHandler.Save();
    }

    /// <summary>
    /// Registers an exercice
    /// </summary>
    /// <param name="exercice">The new exercice</param>
    public void AddExercice(Exercice exercice)
    {
        exercicesDataHandler.AddExercice(exercice);
        exercicesDataHandler.Save();
    }

    /// <summary>
    /// Unregister an exercice
    /// </summary>
    /// <param name="exercice">The exercice</param>
    public void RemoveExerice(Exercice exercice)
    {
        exercicesDataHandler.RemoveExercice(exercice);
        exercicesDataHandler.Save();
    }

    /// <summary>
    /// Unregister an user
    /// </summary>
    /// <param name="user">The user</param>
    public void RemoveUser(User user)
    {
        usersDataHandler.RemoveUser(user);
        usersDataHandler.Save();
    }

}
