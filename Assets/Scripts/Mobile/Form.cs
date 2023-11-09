using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Form
{
    public List<FormRow> rows;
    public List<string> columns;

    public Form(List<User> users, List<string> columnsName)
    {
        columns = new List<string>(columnsName);

        rows = new List<FormRow>();
        foreach (User user in users)
        {
            rows.Add(new FormRow(user.username, user.username, columns.Count));
        }
    }
}

[System.Serializable]
public class FormRow
{
    public string userId;
    public string username;

    public bool[] columnsSucceded;

    public FormRow(string id, string name, int numberRows)
    {
        userId = id;
        username = name;
        columnsSucceded = new bool[numberRows];
    }
}