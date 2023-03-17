using ServerAppWPF.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Client> _users;
        static TcpListener _listener;
        public MainWindow()
        {
            InitializeComponent();
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
