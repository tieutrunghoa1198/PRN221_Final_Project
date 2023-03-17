using ServerApp.Networking;
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
            var port = 7777;
            _users = new List<Client>();
            _listener = new TcpListener(System.Net.IPAddress.Any, port);
            _listener.Start();
            Console.WriteLine("server is started: " + port);
            while (true)
            {
                socketForServer = _listener.AcceptSocket();
                Console.WriteLine(socketForServer);
                _tcpClient = _listener.AcceptTcpClient();
                th_RunServer = new Thread(new ThreadStart(HandleClientConnection));
                th_RunServer.Start();
            }
        }

        private static void HandleClientConnection()
        {

        }
    }
}