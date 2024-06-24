using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

/// <summary>
/// Represents the data file for the users
/// </summary>
[System.Serializable]
public class UsersData : DataFile
{
    public int nextID;
    public List<User> users;

    public UsersData()
    {
        nextID = 1;
        users = new List<User>();
    }
}

/// <summary>
/// Represents a user
/// </summary>
[System.Serializable]
public class User
{
    public int id;
    public string username;
    public int age;
    public List<Session> sessions;

    public User() : this("", 0)
    {
    }

    public User(string name, int age)
    {
        id = -1;
        username = name;
        sessions = new List<Session>();
        this.age = age;
    }
}

/// <summary>
/// Represents a session
/// </summary>
[System.Serializable]
public class Session
{
    public SessionMovement[] movements;

    [System.Serializable]
    public class SessionMovement
    {
        public string moveID;
        public bool wasCorrectlyDone;

        public SessionMovement(string moveID, bool wasCorrectlyDone)
        {
            this.moveID = moveID;
            this.wasCorrectlyDone = wasCorrectlyDone;
        }
    }
}

