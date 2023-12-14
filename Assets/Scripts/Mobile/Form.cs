using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Form
{
    public List<FormRow> rows;
    public List<string> columns;
    public List<string> columnsID;

    public Form(List<User> users, List<string> columnsName, List<string> columnsIDs)
    {
        columns = new List<string>(columnsName);
        columnsID = columnsIDs;

        rows = new List<FormRow>();
        foreach (User user in users)
        {
            rows.Add(new FormRow(user.id, user.username, columns.Count));
        }
    }
}

[System.Serializable]
public class FormRow
{
    public int userId;
    public string username;

    public bool[] columnsSucceded;

    public FormRow(int id, string name, int numberRows)
    {
        userId = id;
        username = name;
        columnsSucceded = new bool[numberRows];

        for (int i = 0; i < numberRows; i++)
        {
            columnsSucceded[i] = true;
        }
    }
}