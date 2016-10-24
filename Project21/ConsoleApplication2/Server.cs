using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using static TCPServer.SendingRecieving;
using static TCPServer.EncryptDecrypt;

namespace TCPServer
{
    class Server
    {
        private List<Connection> connections = new List<Connection>();

        private const int MaxConnections = 10;
        
        private Thread mainServerThread;

        public Server()
        {
            mainServerThread = new Thread(Main);
            mainServerThread.Start();
        }


        private void clientTerminated()
        {
            Thread.Sleep(1000);
            //Console.WriteLine("Event function called");
        }

        public void Main()
        {
            Console.WriteLine("Server Started");
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            //IPAddress localhost = IPAddress.Parse("192.168.1.144");
            TcpListener listener = new System.Net.Sockets.TcpListener(localhost, 1330);
            // Starts listening for incoming connection requests in the listener-thread:
            listener.Start();
            


            while (true)
            {
                if (connections.Count < MaxConnections)
                {
                    Console.WriteLine("connecties : " + connections.Count);
                    Console.WriteLine("Server waiting for connection");
                    TcpClient client = listener.AcceptTcpClient();
                    Connection connectie = new Connection(client, connections, clientTerminated);
                    connections.Add(connectie);
                    Console.WriteLine("New client connected");
                }          
            }
        }
    }

    class Connection
    {
        private ServerData _data;
        private TcpClient client;
        private List<string> _uploadQeue = new List<string>();
        private List<string> _downloaded = new List<string>();
        private List<string> send = new List<string>();
        private List<Connection> Connections;

        public delegate void NotifyExit();
        public event NotifyExit ClientTerminatedEvent;

        private Thread downloadThread;
        private Thread uploadThread;
        private Thread processThread;

        private bool running = true;
        private string clientName;

        public Connection(TcpClient client, List<Connection> connections, NotifyExit exitFunction)
        {
            _data = new ServerData();
            this.client = client;
            Connections = connections;

            //Notify event handler
            ClientTerminatedEvent += exit;
            ClientTerminatedEvent += exitFunction;


            //Start threads for server management
            downloadThread = new Thread(Downloader);
            downloadThread.Start();
            uploadThread = new Thread(Uploader);
            uploadThread.Start();
            processThread = new Thread(Process);
            processThread.Start(client);

        }

        public void dowork()
        {
            
        }

        private void exit()
        {
            Console.WriteLine("Client disconnected...");
            Console.WriteLine("Terminating client threads: ");
            Console.WriteLine("Sending docters/client a message that a client is gone...");
            if(_data.Doctor)
            {
                foreach (Connection con in Connections)
                {
                    if (!con._data.Doctor)
                    {
                        con._data.Messages.Add("message_Dokter_" + "Dokter " + _data.Name + " has been disconnected");
                        con._uploadQeue.Add("message_Dokter_" + "Dokter " + _data.Name + " has been disconnected");
                    }
                }
            }
            else
            {
                foreach (Connection con in Connections)
                {
                    if (con._data.Doctor)
                    {
                        con._data.Messages.Add("message_Dokter_" + "Client " + _data.Name + " has been disconnected");
                        con._uploadQeue.Add("message_Dokter_" + "Client " + _data.Name + " has been disconnected");
                    }
                }
            }
            
                
            running = false;
            Thread.Sleep(500);
            Console.WriteLine("Threads terminated, Removing connections");
            Connections.Remove(this);
        }

        private void Process(object obj)
        {
            while (running)
            {
                if (!client.Connected)
                {
                    ClientTerminatedEvent();
                    break;
                }

                if (_downloaded.Count >= 1)
                {
                    string command = _downloaded[0];
                    Console.WriteLine(command);
                    char[] seperatingchar = new char[1];
                    string cha = "_";
                    seperatingchar[0] = System.Convert.ToChar(cha);
                    string[] words = command.Split(seperatingchar);
                    if (_data.Name == null && words[0].Equals("account"))
                    {
                        if (!File.Exists(words[1] + ".txt"))
                        {
                            if (words[1].Equals("dokter"))
                            {
                                _data = new ServerData(words[2], words[4], words[5]);
                                SerializeObject(_data, words[2] + ".txt");
                                _uploadQeue.Add("doctor");
                            }
                            else
                            {
                                _data = new ServerData(words[1], words[3]);
                                SerializeObject(_data, words[1] + ".txt");
                            }
                            _uploadQeue.Add("account_Created");
                        }
                        else
                        {
                            ServerData data = DeSerObject<ServerData>(words[1] + ".txt");
                            if (data.Password.Equals(words[3]))
                            {
                                _data = data;
                                _uploadQeue.Add("account_Login Succesfull");
                                if (data.Doctor)
                                {
                                    _uploadQeue.Add("doctor");
                                }
                            }
                            else
                            {
                                _uploadQeue.Add("account_Incorrect Credentials");
                                continue;
                            }
                        }
                        System.Timers.Timer timer = new System.Timers.Timer(1000);
                        timer.Elapsed += Timer_Tick;
                        timer.Start();
                    }
                    else
                    {
                        switch (words[0])
                        {
                            case "message":
                                foreach (var con in Connections)
                                {
                                    if (con._data.Name != null)
                                    {
                                        if (con._data.Name.Equals(words[1]))
                                        {
                                            con._data.Messages.Add(words[1]+"_"+words[2]);
                                        }
                                        else if (words[1].Equals("all"))
                                        {
                                            con._data.Messages.Add("message_Dokter_" + words[2]);
                                            con._uploadQeue.Add("message_Dokter_"+words[2]);
                                        }
                                    }
                                }
                                break;
                           
                            case "bike":
                                foreach (var con in Connections)
                                {
                                    if (con._data.Name != null)
                                        if (con._data.Name.Equals(words[1]))
                                            con._data.Results.Add(words[2]);
                                }

                                break;
                            case "update":
                                foreach (var con in Connections)
                                {
                                    if(con._data.Name != null)
                                    if (con._data.Name.Equals(words[1]))
                                    {
                                        if (con._data.Messages.Count != 0)
                                            con._uploadQeue.Add("message_" + con._data.Messages[con._data.Messages.Count - 1]);
                                    }
                                }
                                break;
                            case "command":
                                foreach (var con in Connections)
                                {
                                    if (con._data.Name != null)
                                        if (con._data.Name.Equals(words[1]))
                                    {
                                        con._uploadQeue.Add("command_"+words[2]);
                                    }
                                }
                                break;
                            case "emergency":
                                foreach (var con in Connections)
                                {
                                    if (con._data.Name != null)
                                        if (con._data.Name.Equals(words[1]))
                                        {
                                            con._uploadQeue.Add("emergency");
                                        }
                                }
                                break;
                            case "get":
                                switch (words[1])
                                {
                                    case "messages":
                                        foreach (var con in Connections)
                                        {
                                            if (con._data.Name != null)
                                                if (words[2].Equals(con._data.Name))
                                                foreach (string msg in con._data.Messages)
                                                {
                                                    _uploadQeue.Add("message_" + msg);
                                                }
                                        }

                                        break;
                                    case "graphdata":
                                        foreach (var con in Connections)
                                        {
                                            if (con._data.Name != null)
                                                if (words[2].Equals(con._data.Name))
                                                {
                                                    foreach (string msg in con._data.Results)
                                                    {
                                                        con._uploadQeue.Add("graphdata_" + msg);
                                                    }
                                                    con._uploadQeue.Add("graphdata_done");
                                                }
                                        }

                                        break;
                                    case "connections":
                                        foreach (var con in Connections)
                                        {
                                            if (con._data.Name != null)
                                                if (!con._data.Doctor)
                                                _uploadQeue.Add("client_" + con._data.Name);
                                        }
                                        break;
                                    case "data":
                                            foreach (var con in Connections)
                                        {
                                            if (con._data.Name != null)
                                                if (words[2].Equals(con._data.Name))
                                                {
                                                    List<string> Messages = con._data.Messages;
                                                    foreach (string msg in Messages)
                                                    {
                                                        if (!send.Contains(("message_" + msg)))
                                                        {
                                                            _uploadQeue.Add("message_" + msg);
                                                            send.Add("message_" + msg);
                                                        }
                                                    }
                                                    string[] Results = new string[con._data.Results.Count];
                                                    con._data.Results.CopyTo(Results);
                                                    foreach (string msg in Results)
                                                    {
                                                        if (!send.Contains(("meting_" + msg)))
                                                        {
                                                            _uploadQeue.Add("meting_" + msg);
                                                            send.Add("meting_" + msg);
                                                        }
                                                    }
                                                }
                                            }
                                        break;
                                    case "alldata":
                                            foreach (var con in Connections)
                                        {
                                            if (con._data.Name != null)
                                                if (words[2].Equals(con._data.Name))
                                                {
                                                    foreach (string msg in con._data.Messages)
                                                {
                                                    _uploadQeue.Add("message_" + msg);
                                                    send.Add("message_" + msg);
                                                }
                                                
                                                    foreach (string msg in con._data.Results)
                                                    {
                                                        _uploadQeue.Add("meting_" + msg);
                                                        send.Add("meting_" + msg);
                                                    }
                                                }
                                            }
                                        break;
                                }
                                break;
                        }

                    }
                    _downloaded.Remove(_downloaded[0]);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("Client processing succesfully terminated...");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SerializeObject(_data, _data.Name + ".txt");
        }

        public void Uploader()
        {
            while (running)
            {
                if (_uploadQeue.Count >= 1)
                {
                    Console.WriteLine(_uploadQeue[0]);
                    SendMessage(client, EncryptString(_uploadQeue[0], "groepa4"));
                    _uploadQeue.Remove(_uploadQeue[0]);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("Client uploading succesfully terminated...");
        }

        public void Downloader()
        {
            while (running)
            {
                string received = DecryptString(ReadMessage(client), "groepa4");
                if (!received.Equals("no data"))
                {
                    _downloaded.Add(received);
                }
            }
            Console.WriteLine("Client downloading succesfully terminated...");
        }

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public T DeSerObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                Console.WriteLine(ex);
            }

            return objectOut;
        }
    }
}
