using PRN221_Final_client.Networking.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System;
using System.Windows;

namespace ClientApp.Networking
{
    class Server
    {
        private TcpClient _client;
        public PacketReader packetReader;
        public event Action connectedEvent;
        public event Action msgReceivedEvent;
        public TcpClient Client { get { return _client; } }
        public Server()
        {
            _client = new TcpClient();
        }
        public void connectToServer()
        {
            string username = "asd";
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 11111);
                packetReader = new PacketReader(_client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var _packetWriter = new PacketWriter();
                    _packetWriter.writeOpCode(1);
                    _packetWriter.writeMessage(username);
                    _client.Client.Send(_packetWriter.getPacketBytes());
                }
                ReadPackets();
            }
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var _packetWriter = new PacketWriter();
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 1:
                            Console.WriteLine(opcode);
                            Console.WriteLine(packetReader.readMessage());
                            _packetWriter.writeOpCode(1);
                            _packetWriter.writeMessage("asd case 1");
                            _client.Client.Send(_packetWriter.getPacketBytes());
                            break;
                        case 2:
                            Console.WriteLine(opcode);
                            Console.WriteLine(packetReader.readMessage());
                            Console.WriteLine("Send Message");
                            var _packetWriter2 = new PacketWriter();
                            _packetWriter2.writeOpCode(4);
                            _packetWriter2.WriteImage(TakeScreenShot());
                            
                            _client.Client.Send(_packetWriter2.getPacketBytes());
                            _client.Client.Close();
                            
                            break;
                        case 3:
                            Console.WriteLine(opcode);
                            Console.WriteLine("cool 3");
                            
                            _packetWriter.writeOpCode(3);
                            _packetWriter.writeMessage("asd 3");
                            _client.Client.Send(_packetWriter.getPacketBytes());
                            
                            break;
                        default:
                            Console.WriteLine(opcode);
                            Console.WriteLine(packetReader.readMessage());
                            Console.WriteLine("FPT Hola");
                            break;
                    }
                }
            });
        }
        public void sendMessageToServer(string message)
        {
            var messagePacket = new PacketWriter();
            messagePacket.writeOpCode(2);
            messagePacket.writeMessage(message);
            _client.Client.Send(messagePacket.getPacketBytes());
        }

        public Image TakeScreenShot()
        {
            int screenLeft = (int)SystemParameters.VirtualScreenLeft;
            int screenTop = (int)SystemParameters.VirtualScreenTop;
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;

            Bitmap bitmapScreen = new Bitmap(screenWidth, screenHeight);

            Graphics g = Graphics.FromImage(bitmapScreen);
            g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmapScreen.Size);

            /*bitmapScreen.Save("G:\\" + "anh 123.png");*/
            
            return bitmapScreen;
        }



    }
}
