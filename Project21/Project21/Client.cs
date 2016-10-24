using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Project21
{
    public class Client
    {
        public string userName { get; }
        private TcpClient client;
        public List<string> UploadQeue { get; } = new List<string>();
        public List<string> Downloaded { get; } = new List<string>();

        private bool running = false;

        public Client(string id)
        {
            userName = id;
        }

        public void Downloader()
        {
            while (running)
            {
                string received = EncryptDecrypt.DecryptString(SendingRecieving.ReadMessage(client), "groepa4");
                if (!received.Equals("no data"))
                {
                    Console.WriteLine("Client {1} Received: {0}", received, userName);
                    Downloaded.Add(received);
                }
            }
        }

        public void Uploader()
        {
            while (running)
            {
                if (UploadQeue.Count >= 1)
                {
                    SendingRecieving.SendMessage(client, EncryptDecrypt.EncryptString(UploadQeue[0], "groepa4"));
                    UploadQeue.Remove(UploadQeue[0]);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        public bool Start()
        {
            bool succesfullConnect = true;
            try
            {
                client = new TcpClient("127.0.0.1", 1330);
            }
            catch(SocketException e)
            {
                succesfullConnect = false;
            }

            if(succesfullConnect)
            {
                running = true;
                Thread thread = new Thread(Downloader);
                thread.Start();
                Thread thread2 = new Thread(Uploader);
                thread2.Start();
            }
            return succesfullConnect;
        }

        public void Stop()
        {
            running = false;
            Thread.Sleep(100);
            UploadQeue.Clear();
            Downloaded.Clear();
            client.Close();
            client = null;
        }
    }
}

