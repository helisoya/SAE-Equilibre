using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;



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
    private bool findingServer;

    public async void TryConnectionToIP(string ip)
    {
        if (debug)
        {
            gui.OpenConnectedTab("0.0.0.0");
            return;
        }

        if (client != null) return;



        IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, AppServer.serverPort);

        TcpClient clientTesting = new TcpClient(ipLocalEndPoint)
        {
            SendTimeout = 2000,
            ReceiveTimeout = 2000
        };

        print("[RESULT] Created TcpClient at " + ipAddress.ToString() + ":" + AppServer.serverPort + ", connecting...");
        try
        {
            await clientTesting.ConnectAsync(ip, AppServer.serverPort);
        }
        catch
        {
            print("[RESULT] Connection failed to " + ip + ":" + AppServer.serverPort);
            return;
        }


        print("[RESULT] Connection success to " + ip + ":" + AppServer.serverPort + " : " + clientTesting.Connected);

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


    public async void PauseApp()
    {
        if (debug) return;

        if (client.Connected)
        {
            print("[Result] Asking to pause");

            await sw.WriteLineAsync("Pause|");
            string response = await sr.ReadLineAsync();

            print("[Result] Received : " + response);

            string[] split = response.Split("|");

            if (!split[0].Equals("OK"))
            {
                print("[Result] Error : " + response);
            }
        }
    }


    public async void AskForForm()
    {
        if (debug)
        {
            Form form = new Form(GameManager.instance.GetAllUsers(), new List<string>(new string[] { "ex1", "ex2", "ex3" }), new List<string>(new string[] { "MARCHE1", "MARCHE2", "SQUAT1" }));
            gui.InitializeForm(form);
            return;
        }

        if (client.Connected)
        {
            print("[Result] Asking for form");
            await sw.WriteLineAsync("FORM_ASK|");
            string response = await sr.ReadLineAsync();

            print("[Result] Received : " + response);
            string[] split = response.Split("|");

            if (split[0].Equals("FORM"))
            {
                Debug.Log("[Result] Form : " + split[1]);
                form = JsonUtility.FromJson<Form>(split[1]);
                Debug.Log("[Result] Parsed Form : " + form);
                gui.InitializeForm(form);
            }
            else
            {
                print("[Result] Error : " + response);
            }
        }
    }


    public async void SendForm(Form form)
    {
        this.form = form;

        if (client.Connected)
        {
            print("[Result] Sending Form");
            string json = JsonUtility.ToJson(form);

            print("[Result] Sending : " + json);
            await sw.WriteLineAsync("FORM|" + json);
            string response = await sr.ReadLineAsync();

            print("[Result] Received : " + response);
            string[] split = response.Split("|");

            if (split[0].Equals("OK"))
            {
                gui.OpenConnectedTab(distantIP);
            }
            else
            {
                print("[Result] Error : " + response);
            }
        }
    }


    public void FindServer()
    {
        if (findingServer) return;

        findingServer = true;

        string gate_ip = NetworkGateway();
        print("[IP] : " + gate_ip);
        string[] array = gate_ip.Split('.');

        for (int i = 2; i <= 255; i++)
        {
            if (client != null) break;
            string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + i;


            print("[PING] Connecting to " + ping_var);

            TryConnectionToIP(ping_var);
        }

        findingServer = false;
    }


    static string NetworkGateway()
    {

        foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
        {
            print("[INTERFACE] Found : " + f.Name);
            if (!f.Name.Contains("wlan")) continue;

            foreach (UnicastIPAddressInformation d in f.GetIPProperties().UnicastAddresses)
            {
                if (d.Address.AddressFamily == AddressFamily.InterNetwork &&
                    d.Address.ToString() != "127.0.0.1")
                {
                    print("[INTERFACE] IP : " + d.Address.ToString());
                    return d.Address.ToString();
                }

            }


        }
        return null;
    }

    void OnApplicationQuit()
    {
        client?.Close();
    }
}
