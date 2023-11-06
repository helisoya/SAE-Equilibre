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


    void Awake()
    {
        InitServer();
    }

    void InitServer()
    {
        new Thread(delegate ()
        {
            address = GetLocalIPAddress();
            print("Starting Server On : " + address.ToString() + " : " + serverPort);
            listener = new TcpListener(address, serverPort);
            listener.Start();

            socket = listener.AcceptSocket();

            print("A Device Has Connected To The Server");

            stream = new NetworkStream(socket);
            sr = new System.IO.StreamReader(stream);
            sw = new System.IO.StreamWriter(stream);
            sw.AutoFlush = true;

            ServerLoop();

        }).Start();
    }

    async void ServerLoop()
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
                    await sw.WriteLineAsync("FORM|Nope");
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
        if (socket != null)
        {
            socket.Close();
        }

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
