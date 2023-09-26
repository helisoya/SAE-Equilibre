using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;


public class Read : MonoBehaviour
{

    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    string query;

    public GameObject addedUserInfoContainer;
    public GameObject addedUserInfoTemplate;
    GameObject gobj;

    List<string> infos = new List<string>();
    

    public void connection()
    {
        connectionString = "Server = localhost; Database = participants-unity; User = root; Password = ; Charset = utf8;";

        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();
    }

    void Start()
    {
        var dropdown = transform.GetComponent<TMP_Dropdown>();

        query = "SELECT * FROM users";

        connection();

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Reader = MS_Command.ExecuteReader();

        while (MS_Reader.Read())
        {
            infos.Add(MS_Reader[0].ToString()/* + "   -   Nombre de sessions : " + MS_Reader[1].ToString());
            pseudosOnly.Add(MS_Reader[0].ToString()*/);
        }
        MS_Reader.Close();

        foreach (var info in infos)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = info });
        }

    }

    public void refreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DropdownItemSelected (TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        if(infos.Count() > 0)
        {
            gobj = (GameObject)Instantiate(addedUserInfoTemplate);
            gobj.transform.SetParent(addedUserInfoContainer.transform);
            gobj.GetComponent<UserAddedInfo>().pseudo.text = dropdown.options[index].text;
        }

    }

}