using System;
using System.Net.Sockets;
using System.Text;

using System.IO;

namespace ServerAppWPF.Networking.IO
{
    class PacketReader : BinaryReader
    {
        private NetworkStream _ns;
        public PacketReader(NetworkStream ns) : base(ns)
        { 
            _ns = ns;
        }
        public void byteArrayToImage()
        {
            Console.WriteLine("asd 11112");
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            Console.WriteLine(length.ToString());
            _ns.Read(msgBuffer, 0, length);
            BinaryWriter writer = new BinaryWriter(File.Open("asd.jpeg", FileMode.Create));
            Console.WriteLine("receive message");
            writer.Write(msgBuffer);
            writer.Flush();
            writer.Close();
        }
        public string readMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            _ns.Read(msgBuffer, 0, length);
            var msg = Encoding.ASCII.GetString(msgBuffer);
            return msg;
        }
    }
}
