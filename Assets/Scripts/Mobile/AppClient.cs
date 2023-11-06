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

    TcpClient client;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;

    public void TryConnectionToIP(string ip)
    {
        TcpClient clientTesting = new TcpClient();
        clientTesting.Connect(ip, AppServer.serverPort);

        print("Connection success to " + ip + ":" + AppServer.serverPort + " : " + clientTesting.Connected);

        if (clientTesting.Connected)
        {
            client = clientTesting;
            stream = client.GetStream();
            sr = new System.IO.StreamReader(stream);
            sw = new System.IO.StreamWriter(stream);
            sw.AutoFlush = true;

            gui.OpenConnectedTab(ip);
        }
    }


    public async void AskForForm()
    {
        if (client.Connected)
        {
            print("Sending Data");
            await sw.WriteLineAsync("FORM_ASK|");
            string response = await sr.ReadLineAsync();

            gui.SetServerResponse(response);
        }
    }


    void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }
    }
}
