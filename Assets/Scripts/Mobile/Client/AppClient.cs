using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Threading.Tasks;


/// <summary>
/// Client side of the android app
/// </summary>
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

    /// <summary>
    /// Try the connection to an IP
    /// </summary>
    /// <param name="ip">The IP to try to connect to</param>
    public async void TryConnectionToIP(string ip)
    {
        if (debug)
        {
            gui.OpenConnectedTab("0.0.0.0");
            return;
        }

        if (client != null) return;



        IPAddress ipAddress = NetworkGateway();
        IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, AppServer.serverPort);

        TcpClient clientTesting = new TcpClient(ipLocalEndPoint)
        {
            SendTimeout = 4000,
            ReceiveTimeout = 4000
        };

        print("[RESULT] Created TcpClient at " + ipAddress.ToString() + ":" + AppServer.serverPort + ", connecting to " + ip + ":" + AppServer.serverPort);
        try
        {
            await clientTesting.ConnectAsync(ip, AppServer.serverPort);
            print("[RESULT] Handshaked " + ip);
        }
        catch (Exception e)
        {
            print("[RESULT] Connection failed to " + ip + ":" + AppServer.serverPort);
            print("[RESULT] Error : " + e);
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

    /// <summary>
    /// Asks the server to pause the game
    /// </summary>
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

    /// <summary>
    /// Asks the server for the form
    /// </summary>
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

    /// <summary>
    /// Sends the completed form to the server
    /// </summary>
    /// <param name="form">The completed form</param>
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

            gui.OpenConnectedTab(distantIP);

            if (!split[0].Equals("OK"))
            {
                print("[Result] Error : " + response);
            }
        }
    }

    /// <summary>
    /// Finds the server
    /// </summary>
    public void FindServer()
    {
        if (findingServer) return;

        findingServer = true;

        string gate_ip = NetworkGateway().ToString();
        print("[IP] : " + gate_ip);
        string[] array = gate_ip.Split('.');

        for (int i = 2; i <= 255; i++)
        {
            if (client != null) break;
            if (array[3] == i.ToString()) continue;

            string ping_var = array[0] + "." + array[1] + "." + array[2] + "." + i;


            print("[PING] Connecting to " + ping_var);

            TryConnectionToIP(ping_var);
        }

        findingServer = false;
    }

    /// <summary>
    /// Gets the current network gateway IP
    /// </summary>
    /// <returns>The network gateway's IP</returns>
    static IPAddress NetworkGateway()
    {

        foreach (NetworkInterface f in NetworkInterface.GetAllNetworkInterfaces())
        {
            print("[INTERFACE] Found : " + f.Name);

            foreach (UnicastIPAddressInformation d in f.GetIPProperties().UnicastAddresses)
            {
                if (d.Address.AddressFamily == AddressFamily.InterNetwork &&
                    d.Address.ToString() != "127.0.0.1")
                {
                    print("[INTERFACE] IP : " + d.Address.ToString());
                    return d.Address;
                }

            }


        }
        return null;
    }

    /// <summary>
    /// Closes the client when the application is closed
    /// </summary>
    void OnApplicationQuit()
    {
        client?.Close();
    }
}
