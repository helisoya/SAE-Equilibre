using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;



public class AppClient : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] private MobileGUI gui;
    [SerializeField] private bool debug;

    TcpClient client;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;

    private string distantIP;
    private Form form;

    public void TryConnectionToIP(string ip)
    {
        if (debug)
        {
            gui.OpenConnectedTab("0.0.0.0");
            return;
        }

        TcpClient clientTesting = new TcpClient();
        clientTesting.Connect(ip, AppServer.serverPort);

        print("Connection success to " + ip + ":" + AppServer.serverPort + " : " + clientTesting.Connected);

        if (clientTesting.Connected)
        {
            distantIP = ip;
            client = clientTesting;
            stream = client.GetStream();
            sr = new System.IO.StreamReader(stream);
            sw = new System.IO.StreamWriter(stream)
            {
                AutoFlush = true
            };

            gui.OpenConnectedTab(distantIP);
        }
    }


    public async void AskForForm()
    {
        if (debug)
        {
            Form form = new Form(GameManager.instance.GetAllUsers(), new List<string>(new string[] { "ex1", "ex2", "ex3" }));
            gui.InitializeForm(form);
            return;
        }

        if (client.Connected)
        {
            print("Sending Data");
            await sw.WriteLineAsync("FORM_ASK|");
            string response = await sr.ReadLineAsync();

            string[] split = response.Split("|");

            if (split[0].Equals("FORM"))
            {
                Debug.Log("Form : " + split[1]);
                form = JsonUtility.FromJson<Form>(split[1]);
                Debug.Log("Parsed Form : " + form);
                gui.InitializeForm(form);
            }
            else
            {
                print("Error : " + response);
            }
        }
    }


    public async void SendForm(Form form)
    {
        this.form = form;

        if (client.Connected)
        {
            print("Sending Data");
            string json = JsonUtility.ToJson(form);

            print("Sending : " + json);
            await sw.WriteLineAsync("FORM|" + json);
            string response = await sr.ReadLineAsync();

            string[] split = response.Split("|");

            if (split[0].Equals("OK"))
            {
                gui.OpenConnectedTab(distantIP);
            }
            else
            {
                print("Error : " + response);
            }
        }
    }


    void OnApplicationQuit()
    {
        client?.Close();
    }
}
