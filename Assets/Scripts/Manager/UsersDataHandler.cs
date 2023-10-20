using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        data.users.Add(user);
    }

    public void RemoveUser(User user)
    {
        data.users.Remove(user);
    }
}
