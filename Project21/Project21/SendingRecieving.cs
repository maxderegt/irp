using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Project21
{
    class SendingRecieving
    {
        public static string ReadMessage(TcpClient client)
        {
            if (client.Connected)
            {
                try
                {
                    byte[] sizeinfo = new byte[4];
                    int totalread = 0, currentread = 0;
                    NetworkStream stream = client.GetStream();
                    currentread = totalread = stream.Read(sizeinfo, 0, sizeinfo.Length);
                    while (totalread < sizeinfo.Length && currentread > 0)
                    {
                        int size = sizeinfo.Length - totalread;
                        currentread = stream.Read(sizeinfo,
                            totalread,
                            size);
                        totalread += currentread;
                    }
                    
                    int messagesize = BitConverter.ToInt32(sizeinfo, 0);
                    byte[] data = new byte[messagesize];
                    totalread = 0;
                    currentread = totalread = stream.Read(data, totalread, messagesize);
                    return Encoding.ASCII.GetString(data, 0, totalread);
                }
                catch
                {
                    return EncryptDecrypt.EncryptString("no data","groepa4");
                }
            }
            else
            {
                Thread.Sleep(500);
                return EncryptDecrypt.EncryptString("No Connection","groepa4");
            }
        }

        public static void SendMessage(TcpClient client, string message)
        {
            if (client.Connected)
            {
                Byte[] array = Encoding.ASCII.GetBytes(message);
                Byte[] length = BitConverter.GetBytes(array.Length);
                client.GetStream().Write(length, 0, length.Length);
                client.GetStream().Write(array, 0, array.Length);
            }
            else
            {
                Console.WriteLine("No Connection");
            }         
        }
    }
}
