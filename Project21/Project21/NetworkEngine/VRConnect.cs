using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project21;
using System.Collections.Generic;

namespace TCPVRConnect
{
    public class VRConnect
    {
        enum states { disconnected, connected, setup, ingame };
        private states currentstate = states.disconnected;
        private const string IP = @"145.48.6.10";
        private const int port = 6666;
        public string SessionID = "";
        private string RouteID = "";
        private string NodeID = "";
        private string SceneID = "";
        private string PanelID = "";
        private string EmergencyID = "";
        public bool finished = false;
        private string TerrainId;

        private string CameraId;
        private string HeadId;
        public string Key = "groepa4";
        public string Name = "Camiel";
        public bool Connected = false;
        public bool setup = false;
        private bool canContinue = false;
        Thread listenThread, tempThread;
        private double time = 8.0;

        private TcpClient client;
        private Byte[] bigBuffer = new Byte[0];

        private string userName;
        private clientGui guiReference;
        int prevSec = -1;

        public VRConnect(string userName, clientGui guiReference)
        {
            //Set username to know which client to connect to
            this.userName = userName;
            this.guiReference = guiReference;

            //Run the sim.bat file from the network engine folder
            Utilities.startNetworkEngine();
            Thread.Sleep(3000);

            //Start main thread to setup connection with simulator
            new Thread(mainThread).Start();
        }

        private void mainThread()
        {
            //Connect to Network engine server
            client = new TcpClient();
            Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Console.WriteLine($"Connection opening at IP: {IP} at Port: {port}");
            client.Connect(IP, port);
            Console.WriteLine("TCPClient succesfully connected");

            //Start listenthread to listen to data coming from the network engine
            listenThread = new Thread(ListenToServer);
            listenThread.Start();

            tempThread = new Thread(temp);
            tempThread.Start();

            Console.WriteLine("Creating the first JSON data");

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);


            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("id");
                writer.WriteValue("session/list");
                writer.WriteEndObject();
            }

            string jsonString = sb.ToString();
            //Console.WriteLine($"JSON String: {jsonString}");
            Byte[] msg = Encoding.ASCII.GetBytes(jsonString);
            Byte[] prependMsg = BitConverter.GetBytes(msg.Length);
            Console.WriteLine("Client connected: " + client.Connected);

            Console.WriteLine("Sending the first JSON data");
            NetworkStream stream = client.GetStream();

            stream.Write(prependMsg, 0, prependMsg.Length);
            stream.Write(msg, 0, msg.Length);

            Console.WriteLine("Succesfully sent, listening for response...");

            while (true)
            {
                string timecommand;
                string command;
                switch (currentstate)
                {
                    case states.disconnected:
                        if (Connected == false && SessionID != "")
                        {
                            string connect = "{\"id\" : \"tunnel/create\",\"data\" : {\"session\" : \"" + SessionID +
                                             "\", \"key\" : \"" + Key + "\"}}";
                            SendMessage(connect);
                            Connected = true;
                            currentstate = states.connected;
                        }
                        break;
                    case states.setup:

                        //add your commands that need to happen once like setting a time/adding 3d models etc



                        if (!setup)
                        {
                            String mainCommand;


                            createTerrain(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Hmap.png");

                            string texture = "  {\"id\" : \"scene/node/addlayer\", \"data\" : {\"id\" : \"" + TerrainId + "\", \"diffuse\" : \"data/NetworkEngine/textures/terrain/grass_green_d.jpg\", \"normal\" : \"data/NetworkEngine/textures/terrain/grass_green_d.jpg\"," +
                               " \"minHeight\" : 0, \"maxHeight\" : 255, \"fadeDist\" : 1 }} ";

                            //send texture data
                            sendCommand(texture);


                            String getcommand = "{ \"id\" : \"scene/get\"}";
                            sendCommand(getcommand);

                            String deletecommand = "{\"id\" : \"scene/node/delete\",\"data\" :{\"id\" : \"" + SceneID +
                                                   "\"}}";

                            sendCommand(deletecommand);

                            //add your commands that need to happen once like setting a time/adding 3d models etc
                            sendCommand(TimeCommand(12));

                            sendCommand(RouteAddCommand());

                            sendCommand(RoadRouteCommand());

                            makeEnvironment();

                            sendCommand(BicycleCommand());

                            sendCommand(addHeadToCarCommand());

                            sendCommand(ModelOverRouteCommand(10));

                            sendCommand(makePanelCommand());


                            // testing emergency stop
                            //makeEmergencyPanelCommand();

                            //copypaste the 2 strings above for your setup command below this line
                            setup = true;
                            finished = true;

                        }
                        if (RouteID != "")
                        {
                            currentstate = states.ingame;
                        }
                        break;

                    case states.ingame:
                        Bike bike = guiReference.getBike();
                        Console.WriteLine("Results are being updated");
                        //pulse, rpm, kmh, distance, time
                        int pulse, min, sec, rpm;
                        double distance, currentSpeed;
                        if (guiReference.Results.Count > 0)
                        {
                            pulse = Int32.Parse(guiReference.Results[guiReference.Results.Count - 1][3]);
                            rpm = Int32.Parse(guiReference.Results[guiReference.Results.Count - 1][4]);
                            currentSpeed = double.Parse(guiReference.Results[guiReference.Results.Count - 1][5]) / 10.0;
                            distance = double.Parse(guiReference.Results[guiReference.Results.Count - 1][6]) / 10.0;
                            min = Int32.Parse(guiReference.Results[guiReference.Results.Count - 1][9]);
                            sec = Int32.Parse(guiReference.Results[guiReference.Results.Count - 1][10]);
                        }
                        else
                        {
                            pulse = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].pulse;
                            rpm = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].rpm;
                            currentSpeed = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].kmh / 10.0;
                            distance = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].distance / 10.0;
                            min = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].minutes;
                            sec = guiReference.bike.bikeCom.BikeList[guiReference.bike.bikeCom.BikeList.Count - 1].seconds;
                        }
                        if (!sec.Equals(prevSec))
                        {
                            prevSec = sec;
                            sendCommand(clearPanelCommand());
                            sendCommand(writePanelTextCommandOnCoord(5, 50, "Name: " + userName));
                            sendCommand(writePanelTextCommandOnCoord(300, 50, "Pulse:" + pulse));
                            sendCommand(writePanelTextCommandOnCoord(5, 100, "Speed: " + currentSpeed + " km/h"));
                            sendCommand(writePanelTextCommandOnCoord(300, 100, "RPM: " + rpm));
                            sendCommand(writePanelTextCommandOnCoord(5, 150, "Distance: " + distance + " km"));
                            sendCommand(writePanelTextCommandOnCoord(300, 150, "Time: " + (min / 10) + "" + (min % 10) + ":" + (sec / 10) + "" + (sec % 10)));
                            sendCommand(swapCommand());
                            sendCommand(adjustSpeed((int)Math.Round(currentSpeed/10)));
                        }
                        break;
                }
                //Console.WriteLine("Bytelist length " + byteList.Length);
                time += 0.1;
            }
//            // Close everything.
//            stream.Close();
//            client.Close();
//
//            Console.WriteLine("Press any key to enter");
//            Console.ReadLine();
        }

        //sends commands
        private void sendCommand(string command)
        {

            command = "{\"id\" : \"tunnel/send\",\"data\" :{\"dest\" : \"" + SessionID +
                        "\",\"data\" : " + command + " }}";
            //Console.WriteLine("command send: " + command);
            SendMessage(command);
            while (!canContinue) { }
            canContinue = !canContinue;

        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private void createTerrain(string imagePath)
        {
            Bitmap bitmap = new Bitmap(imagePath);

            StringBuilder sb = new StringBuilder();

            int mapX = bitmap.Width;
            int mapZ = bitmap.Height;

            int index = 0;

            //get pixel color from bitmap
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapZ; j++)
                {
                    sb.Append((bitmap.GetPixel(i, j).R /2));


                    index++;

                    if (index != (mapX * mapZ))
                    {
                        sb.Append(",");
                    }

                }
            }

            //create terrain command
            ///////////////////////// 
            string terrain = "{\"id\" : \"scene/terrain/add\",  \"data\" : {\"size\" : [ " + mapX + ", " + mapZ + " ], \"heights\" : [" + sb + "]}}";


            sendCommand(terrain);

            //create node command
            /////////////////////
            string node;
            node = "{\"id\" : \"scene/node/add\", \"data\" : {\"name\" : \"terrain\" ,\"components\" : { \"transform\" :  {" +
                "\"position\" : [ " + -(mapX / 2) + " , 0,  " + -(mapZ / 2) + "  ],\"scale\" : 1,\"rotation\" : [ 0, 0, 0 ] }," +
            "\"terrain\" :  { } } }}";
            sendCommand(node);

            //ask node information
            String mainCommand = " {\"id\" : \"scene/node/find\", \"data\" : {\"name\" : \"terrain\"}}";
            sendCommand(mainCommand);


        }

        //add a parameter which determines to location/time/size/etc for usabilty
        private string TimeCommand(double value)
        {
            return "{\"id\" : \"scene/skybox/settime\",\"data\" : {\"time\" : " + value + "}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string BicycleCommand()
        {
            string filePath = "data/NetworkEngine/models/bike/bike_anim.fbx";
            return "{\"id\":\"scene/node/add\",\"data\":{\"name\":\"bicycle\",\"components\":{\"transform\":{\"position\":[0,0,0],\"scale\":0.01,\"rotation\":[0,0,90]},\"model\":{\"file\":\""
                + filePath + "\",\"cullbackfaces\":true,\"animated\":true,\"animation\":\"Armature|Fietsen\"}}}}";
        }

        
        private string HouseCommand()
        {
            //string filePath = "data/NetworkEngine/models/bike/bike_anim.fbx";
            string filePath = "data/NetworkEngine/models/houses/house13.obj";
            return "{\"id\":\"scene/node/add\",\"data\":{\"name\":\"bicycle\",\"components\":{\"transform\":{\"position\":[0,0,0],\"scale\":0.01,\"rotation\":[0,0,90]},\"model\":{\"file\":\"" + filePath + "\"}}}}";
        }

        //add a parameter which determines to location/time/size/etc for usabilty
        private string ModelCommand()
        {
            string filePath = "data/NetworkEngine/models/cars/white/car_white.obj";
            return "{\"id\":\"scene/node/add\",\"data\":{\"name\":\"carwhite\",\"components\":{\"transform\":{\"position\":[0,0,0],\"scale\":0.01,\"rotation\":[0,0,0]},\"model\":{\"file\":\"" + filePath + "\"}}}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string RouteAddCommand()
        {
            return "{\"id\" : \"route/add\",\"data\" :{\"nodes\" :[ {\"pos\" : [ 0, 0, 0  ],\"dir\" :[ 5, 0, 5]} ,{\"pos\" : [ 50, 0, 0  ],\"dir\" :[ 5, 0, 5]	},{\"pos\" : [ 50, 0, 20  ],\"dir\" :[ -5, 0, 5]},{\"pos\" : [ -20, 0, 20  ],\"dir\" :[ -5, 0, -5]},{\"pos\" : [ -20, 0, -50  ],\"dir\" :[ 5, 0, -5]},{\"pos\" : [ 20, 0, -50  ],\"dir\" :[ 5, 0, -5]},{\"pos\" : [ 20, 0, -70  ],\"dir\" :[ 5, 0, -5]},{\"pos\" : [ 40, 0, -70 ],\"dir\" :[ 5, 0, 5]},{\"pos\" : [ 40, 0, -10  ],\"dir\" :[ -5, 0, 5]},{\"pos\" : [ 0, 0, -10  ],\"dir\" :[ -5, 0, 5]}   ]}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string RoadRouteCommand()
        {
            return "{\"id\" : \"scene/road/add\",\"data\" :{\"route\" : \"" + RouteID + "\"}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string ModelOverRouteCommand(int speed)
        {
            return "{ \"id\":\"route/follow\",\"data\":{ \"route\": \"" + RouteID + "\",\"node\": \"" + NodeID + "\",\"speed\":" + speed + ",\"offset\":0,\"rotate\":\"XZ\",\"followHeight\":true,\"rotateOffset\":[0,0,0],\"positionOffset\":[0,0,0]}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string makePanelCommand()
        {
            return "{\"id\" : \"scene/node/add\",\"data\" :{\"name\" : \"HUD\",\"parent\" : \"" + HeadId + "\",\"components\" :{ \"transform\" :{ \"position\" : [ -1, 1, -3],\"scale\":1.00,\"rotation\":[0,0,0]},\"panel\" :{\"size\" : [ 1, 0.45 ],\"resolution\" : [ 512, 384 ],\"background\" : [ 1, 1, 1, 1]}}}}";
        }
        //add a parameter which determines to location/time/size/etc for usabilty
        private string writePanelTextCommand(String text)
        {
            return "{\"id\" : \"scene/panel/drawtext\",\"data\" : { \"id\" : \"" + PanelID + "\", \"text\" : \"" + text + "\",\"position\" : [0,192],\"color\" : [ 0,0,1,1 ],\"size\" : 30,\"font\" : \"segoeui\"}}";
            //return "{ \"id\" : \"scene/panel/drawlines\",\"data\" :{ \"id\" : \""+PanelID+"\",\"width\" : 1, lines :[ 0, 100, 100, 100, 1, 0, 0, 1] }}";
        }
        private string writePanelTextCommandOnCoord(int x, int y, String text)
        {
            return "{\"id\" : \"scene/panel/drawtext\",\"data\" : { \"id\" : \"" + PanelID + "\", \"text\" : \"" + text + "\",\"position\" : [" + x + "," + y + "],\"color\" : [ 0,0,0,1 ],\"size\" : 30,\"font\" : \"segoeui\"}}";
        }
        private string swapCommand()
        {
            return "{\"id\" : \"scene/panel/swap\",	\"data\" :{\"id\" : \"" + PanelID + "\"} }";
        }
        private string clearPanelCommand()
        {
            return "{\"id\" : \"scene/panel/clear\",\"data\" :{\"id\" : \"" + PanelID + "\"}}";
        }

        private string addHeadToCarCommand()
        {
            return "{\"id\":\"scene/node/update\",\"data\":{\"id\":\"" + CameraId + "\",\"parent\":\"" + NodeID + "\",\"transform\":{\"position\":[0,0,0],\"scale\":100.00,\"rotation\":[0,90,0]}}}";
        }

        private string adjustSpeed(int speed)
        {
            return "{\"id\":\"route/follow/speed\",\"data\":{\"node\":\"" + NodeID + "\",\"speed\" : " + speed + "}}";
        }

        private void SendMessage(string message)
        {
            Byte[] msg = Encoding.ASCII.GetBytes(message);
            Byte[] prependMsg = BitConverter.GetBytes(msg.Length);
            NetworkStream stream = client.GetStream();

            stream.Write(prependMsg, 0, prependMsg.Length);
            stream.Write(msg, 0, msg.Length);
        }

        //temp method to test
        private void temp()
        {
            while (true)
            {
                String text = Console.ReadLine();
                writeOnPanel(text);
            }
        }

        public void writeOnPanel(string text)
        {
            if (finished)
            {
                sendCommand(clearPanelCommand());
                sendCommand(writePanelTextCommand(text));
                sendCommand(swapCommand());
            }
        }

        public void updateGUIRef(clientGui cgui)
        {
            guiReference = cgui;
        }

        private void ListenToServer()
        {
            NetworkStream stream = client.GetStream();
            while (true)
            {
                if (stream.DataAvailable)
                {
                    Byte[] dataBuffer = new Byte[1024];
                    Int32 bytesRead = stream.Read(dataBuffer, 0, dataBuffer.Length);
                    Byte[] realDataBuffer = new Byte[bytesRead];
                    System.Buffer.BlockCopy(dataBuffer, 0, realDataBuffer, 0, bytesRead);

                    bigBuffer = addArrays(realDataBuffer, bigBuffer);
                    //Console.WriteLine("Bigbuffer length now: " + bigBuffer.Length);


                    byte[] firstFourBytes = new byte[4];
                    System.Buffer.BlockCopy(bigBuffer, 0, firstFourBytes, 0, 4);
                    Int32 packetSize = BitConverter.ToInt32(firstFourBytes, 0);
                    //Console.WriteLine("Packet length: " + packetSize);

                    if (packetSize + 4 >= bigBuffer.Length)
                    {
                        if (bigBuffer.Length >= packetSize + 4)
                        {
                            //Console.WriteLine("Current bigbuffer size: " + bigBuffer.Length);
                            Byte[] JsonData = new byte[packetSize + 4];
                            System.Buffer.BlockCopy(bigBuffer, 4, JsonData, 0, packetSize);
                            bigBuffer = deleteElementsArray(bigBuffer, packetSize + 4);

                            string jsonString = System.Text.Encoding.UTF8.GetString(JsonData, 0, JsonData.Length);
                            ReadData(jsonString);

                            //Console.WriteLine("Bigbuffer length now: " + bigBuffer.Length);
                        }
                    }
                }
            }
        }

        private void ReadData(string jsonString)
        {
            Console.WriteLine(jsonString);
            dynamic data = JObject.Parse(jsonString);
            switch (currentstate)
            {
                case states.disconnected:
                    int length = data.data.Count;
                    List<string> users = new List<string>();
                    //Searches for the last entry of the username in the list
                    int userConnectChoice = -1;
                    for (int i = 0; i < length; i++)
                    {
                        string tempUserName = data.data[i].clientinfo.user;
                        Console.WriteLine(i + " - " + tempUserName);
                        users.Add(tempUserName);
                        if (tempUserName == userName)
                        {
                            userConnectChoice = i;
                        }
                    }

                    if (userConnectChoice == -1)
                    {
                        Console.WriteLine("Network simulator view not found, are you sure the network engine is running?");
                        return;
                    }

                    //Set sessionID to connect to
                    SessionID = data.data[userConnectChoice].id;
                    break;
                case states.connected:
                    Console.WriteLine("connected");
                    SessionID = data.data.id;
                    currentstate = states.setup;
                    break;
                case states.setup:
                    switch ((string)data.data.data.id)
                    {
                        case "scene/node/find":
                            for (int j = 0; j < data.data.data.data.Count; j++)
                            {
                                Console.WriteLine(data.data.data.data[j].name);
                                if (data.data.data.data[j].name == "terrain")
                                {
                                    TerrainId = (string)data.data.data.data[0].uuid;
                                    Console.WriteLine(TerrainId);
                                }
                            }


                            break;
                        case "scene/get":

                            for (int j = 0; j < data.data.data.data.children.Count; j++)
                            {
                                // Console.WriteLine(data.data.data.data.children[j].name);
                                if (data.data.data.data.children[j].name == "GroundPlane")
                                {
                                    SceneID = data.data.data.data.children[j].uuid;
                                }
                                if (data.data.data.data.children[j].name == "Camera")
                                {
                                    CameraId = (string)data.data.data.data.children[j].uuid;
                                }
                                if (data.data.data.data.children[j].name == "Head")
                                {
                                    HeadId = (string)data.data.data.data.children[j].uuid;
                                }
                            }



                            break;
                        case "route/add":
                            RouteID = data.data.data.data.uuid;
                            break;
                        case "scene/node/add":
                            if (data.data.data.data.name == "carwhite")
                            {
                                NodeID = data.data.data.data.uuid;
                                Console.WriteLine("in if");
                            }
                            if (data.data.data.data.name == "bicycle")
                            {
                                NodeID = data.data.data.data.uuid;
                                Console.WriteLine("in if");
                            }
                            if (data.data.data.data.name == "HUD")
                            {
                                PanelID = data.data.data.data.uuid;
                                Console.WriteLine("panel id = " + PanelID);
                            }
                            break;
                    }
                    Console.WriteLine("setting continue true");
                    canContinue = true;
                    break;
                case states.ingame:
                    canContinue = true;
                    break;
             }
        }

        private Byte[] addArrays(Byte[] dataBuffer, Byte[] bigBuffer)
        {
            byte[] tempBuffer = new byte[dataBuffer.Length + bigBuffer.Length];
            System.Buffer.BlockCopy(bigBuffer, 0, tempBuffer, 0, bigBuffer.Length);
            System.Buffer.BlockCopy(dataBuffer, 0, tempBuffer, bigBuffer.Length, dataBuffer.Length);
            return tempBuffer;
        }

        private Byte[] deleteElementsArray(Byte[] buffer, int elementsDeleted)
        {
            Byte[] tempBuffer = new byte[buffer.Length - elementsDeleted];
            System.Buffer.BlockCopy(buffer, elementsDeleted, tempBuffer, 0, buffer.Length - elementsDeleted);
            return tempBuffer;
        }
        public void makeEmergencyPanelCommand()
        {
            sendCommand("{\"id\" : \"scene/node/add\",\"data\" :{\"name\" : \"Emergency\",\"parent\" : \"" + HeadId + "\",\"components\" :{ \"transform\" :{ \"position\" : [ 0, 0, -1],\"scale\":1.00,\"rotation\":[0,0,0]},\"panel\" :{\"size\" : [ 10, 10 ],\"resolution\" : [ 1, 1 ],\"background\" : [ 0, 0, 0, 1]}}}}");
            sendCommand("{\"id\" : \"scene/panel/drawtext\",\"data\" : { \"id\" : \"" + EmergencyID + "\", \"text\" : \" .\",\"position\" : [0,0],\"color\" : [ 1,1,1,1 ],\"size\" : 30,\"font\" : \"segoeui\"}}");
            sendCommand("{\"id\" : \"scene/panel/swap\",	\"data\" :{\"id\" : \"" + EmergencyID + "\"} }");

        }

        private void makeEnvironment()
        {
            try
            {
                StreamReader reader = new StreamReader("Environment.txt");
                while (reader.Peek() >= 0)
                {
                    sendCommand(reader.ReadLine());
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Couldn't load environment");
            }


        }
    }
}
