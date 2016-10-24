using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TCPClientServer.SendingRecieving;
using static TCPClientServer.EncryptDecrypt;

namespace TCPClientServer
{
    public class Client
    {
        private string id { get; }
        private TcpClient client;
        public List<string> UploadQeue { get;} = new List<string>();
        public List<string> Downloaded { get;}= new List<string>();

        public Client(string id)
        {
            this.id = id;
            client = new TcpClient("127.0.0.1", 1330);
            UploadQeue.Add("account_max de regt_password_ll_false");
        }

        public void Downloader()
        {
            while (true)
            {
                string received = DecryptString(ReadMessage(client),"groepa4");
                if (!received.Equals("no data"))
                {
                    Console.WriteLine("Client {1} Received: {0}", received,id);
                    Downloaded.Add(received);
                }
            }
        }

        public void Uploader()
        {
            while (true)
            {
                if (UploadQeue.Count >= 1)
                {
                    SendMessage(client, EncryptString(UploadQeue[0], "groepa4"));
                    UploadQeue.Remove(UploadQeue[0]);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public void Main()
        {
            Thread thread = new Thread(Downloader);
            thread.Start();
            Thread thread2 = new Thread(Uploader);
            thread2.Start();
            while (true)
            {
                if (Downloaded.Count >= 1)
                {
                    string command = Downloaded[0];
                    char[] seperatingchar = new char[1];
                    string cha = "_";
                    seperatingchar[0] = System.Convert.ToChar(cha);
                    string[] words = command.Split(seperatingchar);
                    //handle your incoming shit
                    if (words[0].Equals("account"))
                    {
                        switch (words[1])
                        {
                            case "Login Succesfull":
                                //add form shit joey
                                break;
                            case "Created":
                                //add form shit joey
                                break;
                            case "Incorrect Credentials ":
                                //add form shit joey
                                break;
                        }
                    }
                    Downloaded.Remove(Downloaded[0]);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
