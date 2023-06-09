﻿using ServerApp.Networking;
using ServerApp.Networking.IO;
using System.Net;
using System.Net.Sockets;
namespace ServerApp
{
    class Program
    {
        static List<Client> _users;
        static TcpListener _listener;
        static Socket socketForServer;
        static TcpClient _tcpClient;
        private static Thread th_RunServer;
        static void Main(string[] args)
        {
            StartListen();
        }

        private static void StartListen()
        {
            var port = 11111;
            _users = new List<Client>();
            _listener = new TcpListener(System.Net.IPAddress.Any, port);
            _listener.Start();
            Console.WriteLine("server is started: \n" + "IP: " + System.Net.IPAddress.Any.ToString() + " Port: " + port);
            while (true)
            {
                if (_users.Count == 0)
                {
                    var client = new Client(_listener.AcceptTcpClient());
                    _users.Add(client);
                }
                
            }
        }

    }
}