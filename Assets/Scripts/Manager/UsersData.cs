using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UsersData
{
    public List<User> users;

    public UsersData()
    {
        users = new List<User>();
    }
}

[System.Serializable]
public class User
{
    public string username;
    public int numberSessions;

    public User(string name)
    {
        username = name;
    }
}
