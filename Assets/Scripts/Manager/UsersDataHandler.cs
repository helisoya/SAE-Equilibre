using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

/// <summary>
/// Data handler for the users data file
/// </summary>
[System.Serializable]
public class UsersDataHandler : DataHandler<UsersData>
{


    /// <summary>
    /// Returns all the available users
    /// </summary>
    /// <returns>The available users</returns>
    public List<User> GetUsers()
    {
        return data.users;
    }

    /// <summary>
    /// Returns a user
    /// </summary>
    /// <param name="index">The user's index</param>
    /// <returns>The user</returns>
    public User GetUser(int index)
    {
        if (index < 0 || index >= data.users.Count) return null;
        return data.users[index];
    }

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="user">The user to register</param>
    public void AddUser(User user)
    {
        user.id = data.nextID;
        data.nextID++;
        data.users.Add(user);
    }

    /// <summary>
    /// Unregister an user
    /// </summary>
    /// <param name="user">The user</param>
    public void RemoveUser(User user)
    {
        data.users.Remove(user);
    }

    /// <summary>
    /// Adds a session to a user
    /// </summary>
    /// <param name="id">The user's ID</param>
    /// <param name="session">The session to add</param>
    public void AddSessionToUser(int id, Session session)
    {
        data.users.Find(user => user.id == id)?.sessions.Add(session);
    }

    /// <summary>
    /// [DEPRECATED]
    /// Converts the existing SQL database to a JSON database.
    /// Do not use if no SQL database exists.
    /// </summary>
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
                AddUser(new User(reader.GetString("pseudo"), 0));
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
