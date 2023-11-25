using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

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

[System.Serializable]
public class User
{
    public int id;
    public string username;
    public int age;
    public List<Session> sessions;

    public User(string name, int age)
    {
        id = -1;
        username = name;
        sessions = new List<Session>();
        this.age = age;
    }
}


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

