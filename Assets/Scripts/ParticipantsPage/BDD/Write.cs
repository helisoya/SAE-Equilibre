using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using MySql.Data.MySqlClient;

public class Write : MonoBehaviour
{
    public TextMeshProUGUI pseudo;
    public TMP_InputField eraseField;
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;
    int id = 0;

    public void connection()
    {

        connectionString = "Server = localhost; Database = participants-unity; User = root; Password = ; Charset = utf8;";

        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

    }

    public void sendInfo() {

        if (pseudo.text.Count() > 2)
        {
            connection();

            query = "insert into users(pseudo, session_id) values( '" + pseudo.text + "' , '" + 0 + "');";

            MS_Command = new MySqlCommand(query, MS_Connection);

            MS_Command.ExecuteNonQuery();

            MS_Connection.Close();

            eraseField.text = "";
        }
    }

}
