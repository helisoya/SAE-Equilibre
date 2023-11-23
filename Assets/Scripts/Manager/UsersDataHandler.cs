using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

[System.Serializable]
public class UsersDataHandler : DataHandler
{
    [SerializeField] private UsersData data;


    public override void Save()
    {
        FileManager.SaveJSON(FileManager.savPath + dataSavePath, data);
    }

    public override void Load()
    {
        if (System.IO.File.Exists(FileManager.savPath + dataSavePath))
        {
            data = FileManager.LoadJSON<UsersData>(FileManager.savPath + dataSavePath);
        }
        else
        {
            Save();
        }
    }


    public List<User> GetUsers()
    {
        return data.users;
    }

    public User GetUser(int index)
    {
        return data.users[index];
    }

    public void AddUser(User user)
    {
        user.id = data.nextID;
        data.nextID++;
        data.users.Add(user);
    }

    public void RemoveUser(User user)
    {
        data.users.Remove(user);
    }

    public void AddSessionToUser(int id, Session session)
    {
        data.users.Find(user => user.id == id)?.sessions.Add(session);
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
                Debug.Log("Loaded " + reader.GetString("pseudo"));
                AddUser(new User(reader.GetString("pseudo")));
            }
            reader.Close();

            Save();
        }
        catch
        {
            Debug.Log("Error while connecting to database");
        }
    }
}
