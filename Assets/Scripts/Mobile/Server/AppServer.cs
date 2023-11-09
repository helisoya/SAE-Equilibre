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

public class AppServer : MonoBehaviour
{
    public const int serverPort = 4242;
    private TcpListener listener;
    private IPAddress address;
    private Socket socket;
    private System.IO.StreamReader sr;
    private System.IO.StreamWriter sw;
    private NetworkStream stream;


    private Thread serverThread;


    public void InitServer()
    {
        serverThread = new Thread(ServerThreadInit);
        serverThread.Start();
    }

    void ServerThreadInit()
    {
        address = GetLocalIPAddress();
        print("Starting Server On : " + address.ToString() + " : " + serverPort);
        listener = new TcpListener(address, serverPort);
        listener.Start();

        socket = listener.AcceptSocket();

        print("A Device Has Connected To The Server");

        stream = new NetworkStream(socket);
        sr = new System.IO.StreamReader(stream);
        sw = new System.IO.StreamWriter(stream)
        {
            AutoFlush = true
        };

        ServerThreadLoop();
    }

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
                case "FORM_ASK":
                    print("Phone Asking for Form");
                    List<string> columns = new List<string>();

                    Movement move;
                    foreach (Sequence sequence in GameManager.instance.currentExercice.sequences)
                    {
                        print(sequence.idMovement);
                        move = GameManager.instance.GetMovement(sequence.idMovement);
                        print("cd ..");
                        print(move.movementName);
                        print(sequence.movementTime);
                        columns.Add(move.movementName + "(" + sequence.movementTime + ")");
                    }

                    print("Creating Form");
                    Form form = new Form(GameManager.instance.participants, columns);

                    print("Parsing to JSON");
                    string json = JsonUtility.ToJson(form);
                    print("Sending : " + json);
                    await sw.WriteLineAsync("FORM|" + json);
                    break;
                case "FORM_GIVE":
                    // split[1] is the form
                    break;
            }
        }
    }

    public IPAddress GetLocalIPAddress()
    {

        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip;
            }
        }
        return null;
    }


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
