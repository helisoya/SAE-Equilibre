using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

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
            currentExercice = exercicesDataHandler.GetExercice(0);

            participants = new List<User>();
            currentExercice = null;
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


    public void ConvertSQLToJSON()
    {
        try
        {

            MySqlConnection connection = new MySqlConnection("Server = localhost; Database = participants-unity; User = root; Password = ; Charset = utf8;");
            connection.Open();


            MySqlCommand command = new MySqlCommand("SELECT * FROM users", connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                print("Loaded " + reader.GetString("pseudo"));
                usersDataHandler.AddUser(new User(reader.GetString("pseudo")));
            }
            reader.Close();

            usersDataHandler.Save();
        }
        catch
        {
            print("Error while connecting to database");
        }
    }
}
