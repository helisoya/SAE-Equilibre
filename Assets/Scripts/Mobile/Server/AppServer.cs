using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Unity.VisualScripting;
using System;
using System.Text;
using System.Linq;
using System.Data;

/// <summary>
/// Server side of the android app
/// </summary>
public class AppServer : MonoBehaviour
{
    public const int serverPort = 17224;
    private TcpListener listener;

    private TcpClient socket;
    private System.IO.StreamReader sr;
    private System.IO.StreamWriter sw;
    private NetworkStream stream;
    private Thread serverThread;

    private Dictionary<string, IPAddress> _addresses;
    public Dictionary<string, IPAddress> addresses
    {
        get
        {
            return _addresses;
        }
    }

    private IPAddress _address;
    public IPAddress address
    {
        get
        {
            return _address;
        }
    }

    private bool _wantToPause;


    void Update()
    {
        if (_wantToPause && GameGUI.instance != null)
        {
            _wantToPause = false;
            GameGUI.instance.SetPauseMenuActive(!GameGUI.instance.paused);
        }
    }


    void Awake()
    {
        RefreshIps();
    }

    /// <summary>
    /// Finds the potential ips for the network
    /// </summary>
    public void RefreshIps()
    {
        _addresses = new Dictionary<string, IPAddress>();
        FindIPs();
        SetCurrentAddress(addresses.Keys.ElementAt(0));
    }

    /// <summary>
    /// Changes the current IP address
    /// </summary>
    /// <param name="key">The key for new IP</param>
    public void SetCurrentAddress(string key)
    {
        _address = addresses[key];
    }

    /// <summary>
    /// Find the IPs of the computer
    /// </summary>
    void FindIPs()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface inter in interfaces)
        {
            if ((inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
            inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
            inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit) &&
            inter.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in inter.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        print(inter.Name + " " + ip.Address.ToString());
                        addresses.Add(inter.Name, ip.Address);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Initialize the server
    /// </summary>
    public void InitServer()
    {
        serverThread = new Thread(ServerThreadInit);
        serverThread.Start();
    }

    /// <summary>
    /// Initialize the server (call this only on a thread)
    /// </summary>
    async void ServerThreadInit()
    {
        print("Starting Server On : " + _address.ToString() + " : " + serverPort);
        listener = new TcpListener(_address, serverPort);
        listener.Start();

        socket = await listener.AcceptTcpClientAsync();

        print("A Device Has Connected To The Server");

        stream = socket.GetStream();
        sr = new System.IO.StreamReader(stream);
        sw = new System.IO.StreamWriter(stream)
        {
            AutoFlush = true
        };

        ServerThreadLoop();
    }

    /// <summary>
    /// Main Server loop (call this only on a thread)
    /// </summary>
    async void ServerThreadLoop()
    {
        while (socket.Connected)
        {
            print("Waiting for Request...");
            string request = await sr.ReadLineAsync();
            print("Request From Phone : " + request);
            string[] split = request.Split("|");

            switch (split[0])
            {
                case "Pause":
                    print("Phone asking to pause");
                    _wantToPause = true;
                    await sw.WriteLineAsync("OK|");
                    break;
                case "FORM_ASK":
                    print("Phone Asking for Form");
                    List<string> columns = new List<string>();
                    List<string> columnsID = new List<string>();

                    Movement move;
                    foreach (Sequence sequence in GameManager.instance.currentExercice.sequences)
                    {
                        move = GameManager.instance.GetMovement(sequence.idMovement);
                        columnsID.Add(move.ID);
                        columns.Add(move.movementName + "(" + sequence.movementTime + ")");
                    }

                    print("Creating Form");
                    Form form = new Form(GameManager.instance.participants, columns, columnsID);

                    print("Parsing to JSON");
                    string json = JsonUtility.ToJson(form);
                    print("Sending : " + json);
                    await sw.WriteLineAsync("FORM|" + json);
                    break;
                case "FORM":
                    Form decodedForm = JsonUtility.FromJson<Form>(split[1]);

                    foreach (FormRow row in decodedForm.rows)
                    {
                        Session session = new Session();

                        session.movements = new Session.SessionMovement[row.columnsSucceded.Length];

                        for (int i = 0; i < row.columnsSucceded.Length; i++)
                        {
                            session.movements[i] = new Session.SessionMovement(decodedForm.columnsID[i], row.columnsSucceded[i]);
                        }

                        GameManager.instance.AddSessionToUser(row.userId, session);
                    }

                    GameManager.instance.SaveUsers();

                    await sw.WriteLineAsync("OK|");
                    break;
            }
        }
    }

    /// <summary>
    /// Restarts the server
    /// </summary>
    public void RestartServer()
    {
        OnApplicationQuit();
        InitServer();
    }

    /// <summary>
    /// Closes the server when the game is closed
    /// </summary>
    void OnApplicationQuit()
    {
        print("Stopping Server");

        serverThread.Abort();

        socket?.Close();

        try
        {
            listener.Server.Shutdown(SocketShutdown.Both);
        }
        catch (Exception ex)
        {
            print("Shutdown Error : " + ex);
        }
        finally
        {
            listener.Server.Close(0);
            listener.Stop();
        }
    }
}
