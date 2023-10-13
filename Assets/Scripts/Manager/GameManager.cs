using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UsersData usersData;
    [SerializeField] private ExercicesData exercicesData;
    private Dictionary<string, Movement> movements;
    public Exercice currentExercice;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadMovements();

            Load_UsersData();
            Load_ExercicesData();
            currentExercice = exercicesData.exercices[0];
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


    public void ConvertSQLToJSON()
    {
        try
        {

            MySqlConnection connection = new MySqlConnection("Server = localhost; Database = participants-unity; User = root; Password = ; Charset = utf8;");
            connection.Open();


            MySqlCommand command = new MySqlCommand("SELECT * FROM users", connection);

            MySqlDataReader reader = command.ExecuteReader();

            usersData.users = new List<User>();

            while (reader.Read())
            {
                print("Loaded " + reader.GetString("pseudo"));
                AddUser(reader.GetString("pseudo"));
            }
            reader.Close();


            Save_UsersData();
        }
        catch
        {
            print("Error while connecting to database");
        }
    }

    public void AddUser(string username)
    {
        usersData.users.Add(new User(username));
        Save_UsersData();
    }

    public void RemoveUser(User user)
    {
        usersData.users.Remove(user);
        Save_UsersData();
    }


    public void AddExercice(Exercice exercice)
    {
        exercicesData.exercices.Add(exercice);
    }

    public void RemoveExercice(Exercice exercice)
    {
        exercicesData.exercices.Remove(exercice);
    }


    public void Save_UsersData()
    {
        FileManager.SaveJSON(FileManager.savPath + "users.sav", usersData);
    }

    public void Load_UsersData()
    {
        if (System.IO.File.Exists(FileManager.savPath + "users.sav"))
        {
            usersData = FileManager.LoadJSON<UsersData>(FileManager.savPath + "users.sav");
        }
        else
        {
            Save_UsersData();
        }
    }


    public void Save_ExercicesData()
    {
        FileManager.SaveJSON(FileManager.savPath + "exercices.sav", exercicesData);
    }

    public void Load_ExercicesData()
    {
        if (System.IO.File.Exists(FileManager.savPath + "exercices.sav"))
        {
            exercicesData = FileManager.LoadJSON<ExercicesData>(FileManager.savPath + "exercices.sav");
        }
        else
        {
            Save_ExercicesData();
        }
    }

}
