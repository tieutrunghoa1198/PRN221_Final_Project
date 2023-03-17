using ServerAppWPF.Networking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
namespace ServerAppWPF.Networking
{
    class Client
    {
        public string username { get; set; }
        public Guid UID { get; set; }
        public TcpClient _clientSocket { get; set; }
        PacketReader _packetReader;
        public Client(TcpClient clientSocket)
        {
            _clientSocket = clientSocket;
            UID = Guid.NewGuid();
            _packetReader = new PacketReader(_clientSocket.GetStream());
            var opcode = _packetReader.ReadByte();
            username= _packetReader.readMessage();
            Console.WriteLine(opcode+ " " + username);
            var broadPacket = new PacketWriter();
            broadPacket.writeOpCode(1);
            broadPacket.writeMessage(username);
            _clientSocket.Client.Send(broadPacket.getPacketBytes());
            Console.WriteLine($"{DateTime.Now}: Client has connected with user name: {username} ");
            Task.Run( () => Process());
        }
        
        public void Process() 
        { 
            while(true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();
                    Console.WriteLine(opcode);
                    switch (opcode)
                    {
                        case 0:
                            Console.WriteLine("Cool");
                            Console.WriteLine(_packetReader.readMessage());
                            break;
                        case 1:
                            Console.WriteLine("Cool 2");
                            Console.WriteLine(_packetReader.readMessage());
                            var asd = Console.ReadLine() ?? "empty";
                            var broadPacket = new PacketWriter();
                            byte a = Convert.ToByte(asd.Split(" ")[0]);
                            string message = asd.Split(" ")[1];
                            broadPacket.writeOpCode(a);
                            broadPacket.writeMessage(message);
                            _clientSocket.Client.Send(broadPacket.getPacketBytes());
                            break;
                        case 2:
                            var msg = _packetReader.readMessage();
                            Console.WriteLine($"{DateTime.Now} message received: {msg}");
                            //Program.broadCastMessage($"[{DateTime.Now}] : [{username}] : {msg}");
                            break;
                        case 4:
                            Console.WriteLine("asd");
                            _packetReader.byteArrayToImage();
                            
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
