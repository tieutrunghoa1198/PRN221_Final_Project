using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Final_client.Networking.IO
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

        public void WriteImage(System.Drawing.Image image)
        {
            
            Console.WriteLine(image.Width);
            _ms.Write(BitConverter.GetBytes(imgToByteArray(image).Length));
            _ms.Write(imgToByteArray(image));
            
            Console.WriteLine("Send Message write");
        }

        public byte[] imgToByteArray(System.Drawing.Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, ImageFormat.Jpeg);
                Console.WriteLine(mStream.ToArray().Length + " leng ");
                return mStream.ToArray();
            }
        }

        public byte[] getPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}
