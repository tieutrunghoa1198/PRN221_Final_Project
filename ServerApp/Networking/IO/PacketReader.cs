﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Networking.IO
{
    class PacketReader : BinaryReader
    {
        private NetworkStream _ns;
        public PacketReader(NetworkStream ns) : base(ns)
        { 
            _ns = ns;
        }
        public System.Drawing.Image byteArrayToImage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            _ns.Read(msgBuffer, 0, length);
            MemoryStream ms = new MemoryStream(msgBuffer);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            returnImage.Save("G:\\" + "file_transfered.png");
            Console.Write(returnImage.ToString());
            return returnImage;
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
