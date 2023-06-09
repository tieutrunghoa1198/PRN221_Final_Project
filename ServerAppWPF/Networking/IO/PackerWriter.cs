﻿using System;
using System.Text;
using System.IO;
namespace ServerAppWPF.Networking.IO
{
    class PacketWriter
    {
        MemoryStream _ms;
        public PacketWriter()
        {
            _ms = new MemoryStream();
        }
        public void writeOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }
        public void writeMessage(string msg)
        {
            var msgLength = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLength));
            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }
        public byte[] getPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
