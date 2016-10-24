using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project21
{
    public class BikeCommunicator : BikeCommunication
    {
        private string comPort;
        private SerialPort mySerialPort;
        private static List<string> bikeData = new List<string>();
        private List<BikeData> bikeList = new List<BikeData>();
        private Queue<Byte[]> queue = new Queue<byte[]>();
        private FakeBike fakeBike;

        internal List<BikeData> BikeList
        {
            get
            {
                return bikeList;
            }

            set
            {
                bikeList = value;
            }
        }

        public BikeCommunicator(string comPort)
        {
            this.comPort = comPort.ToUpper();

            //Try creating thread
            Console.WriteLine("Creating Program thread");
            Thread bikeThread = new Thread(BikeDataListenThread);
            bikeThread.Start();
        }

        private void BikeDataListenThread()
        {
            try
            {
                //Init serialport
                mySerialPort = new SerialPort("com" + comPort);

                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.DataBits = 8;
                mySerialPort.Handshake = Handshake.None;
                mySerialPort.RtsEnable = true;

                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                //if(mySerialPort.Open())
                mySerialPort.Open();

                //Reset bicycle
                string temp1 = "RS\r\n";
                char[] yourString1 = temp1.ToCharArray();
                byte[] MyMessage1 = System.Text.Encoding.UTF8.GetBytes(yourString1);
                queue.Enqueue(MyMessage1);

                //Put bicycle in CM
                string temp2 = "CM\r\n";
                char[] yourString2 = temp2.ToCharArray();
                byte[] MyMessage2 = System.Text.Encoding.UTF8.GetBytes(yourString1);
                queue.Enqueue(MyMessage1);

                while (true)
                {
                    //Console.WriteLine("Reading bicycle data...");

                    string temp = "ST\r\n";
                    char[] yourString = temp.ToCharArray();
                    queue.Enqueue(System.Text.Encoding.UTF8.GetBytes(yourString));

                    while (queue.Count > 0)
                    {
                        try
                        {
                            byte[] MyMessage = queue.Dequeue();
                            mySerialPort.Write(MyMessage, 0, MyMessage.Length);
                        }
                        catch(System.InvalidOperationException) 
                        {
                            Console.WriteLine("exception occurred port was closed");
                        }
                    }
                    //Print data
                    Thread.Sleep(500);
                }
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("no real bike found start simulation");
                fakeBike = new FakeBike();

                while (true)
                {
                    bikeData.Add(fakeBike.getData());
                    PrintBikeDataList();
                    Thread.Sleep(500);
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.WriteLine("COM Port invalid start simulation");
                fakeBike = new FakeBike();

                while (true)
                {
                    bikeData.Add(fakeBike.getData());
                    PrintBikeDataList();
                    Thread.Sleep(500);
                }
            }
        }

        public void SendCommand(String fullCommand)
        {
            //checkt command
            try
            {
                fullCommand = fullCommand.ToUpper();
                String command = fullCommand.Substring(0, 2);
                int value = -1;
                if (fullCommand.Length > 3)
                {
                    value = Int32.Parse(fullCommand.Substring(3, fullCommand.Length - 3));
                }
                switch (command.ToUpper())
                {
                    case "CD": if (value == -1) Send("CD", null); break;
                    case "CM": if (value == -1) Send("CM", null); break;
                    case "PW":
                        if (value >= 25 && value <= 400)
                        {
                            Send(command, value.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Not a valid value");
                        }
                        break;
                    case "CP": if (value == -1) Send("CP", null); break;
                    case "RS": if (value == -1) Send("RS", null); break;
                    case "ID": if (value == -1) Send("ID", null); break;
                    case "PD":
                        if (value >= 0 && value <= 999)
                        {
                            Send(command, value.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Not a valid value");
                        }
                        break;
                    case "PT":
                        if (value >= 0 && value <= 9959)
                        {
                            Send(command, value.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Not a valid value");
                        }
                        break;
                    case "PP":
                        if (value >= 25 && value <= 400)
                        {
                            Send(command, value.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Not a valid value");
                        }
                        break;
                    case "ST": if (value == -1) Console.WriteLine(ToString()); break;
                    default: Console.WriteLine("Not a valid command"); break;
                }
            }
            catch (ArgumentOutOfRangeException)
            { Console.WriteLine("Not a valid command / out of range"); }
            catch (FormatException)
            { Console.WriteLine("Not a valid command / format exception"); }


        }
        private void Send(String command, String value)
        {
            if (mySerialPort.IsOpen)
            {
                //send command
                String finalCommand = "CM\r\n";
                Byte[] MyMessage = System.Text.Encoding.UTF8.GetBytes(new String(finalCommand.ToCharArray()));
                mySerialPort.Write(MyMessage, 0, MyMessage.Length);
                if (value != null && value.Equals("-1"))
                    finalCommand = command + "\r\n";
                else
                    finalCommand = command + " " + value + "\r\n";
                MyMessage = System.Text.Encoding.UTF8.GetBytes(new String(finalCommand.ToCharArray()));
                queue.Enqueue(MyMessage);
            }
            else
            {
                switch (command.ToUpper())
                {
                    case "PW": fakeBike.setPower(Int32.Parse(value)); break;
                    case "PP": fakeBike.setPower(Int32.Parse(value)); break;
                    case "PT": fakeBike.setSeconds(Int32.Parse(value)); break;
                    case "PD": fakeBike.setDistance(Int32.Parse(value)); break;
                    case "RS": fakeBike.reset(); break;
                    default: Console.WriteLine("Unsupported command for the simulator"); break;
                }
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            lock (bikeData)
            {

                SerialPort sp = (SerialPort)sender;
                bikeData.Add(sp.ReadLine());
                PrintBikeDataList();
                sp.DiscardInBuffer();
            }
        }

        private void PrintBikeDataList()
        {
            string[] separatingChars = { "\t", };


            foreach (String data in bikeData)
            {
                if (data.Length > 10)
                {
                    string[] words;
                    words = data.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                    if(words.Count()==8)
                        InterpretBikeDataList(words);
                }
            }
            bikeData.Clear();
            //if (bikeData.Count != 0)
            //{
            //    if (bikeData[0].Length > 10)
            //    {
            //        bikeData.RemoveAt(0);
            //    }
            //    else
            //    { 
            //        string data = bikeData[0];
            //        string[] words;
            //        lock (bikeData)
            //        {
            //            words = data.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            //        }
            //        InterpretBikeDataList(words);
            //        bikeData.Clear();
            //    }

            //}
        }

        private  void InterpretBikeDataList(String[] words)
        {
            string[] separatingChar = { ":", };
            int count = 0;
            BikeData bd = new BikeData();
            foreach (string i2 in words)
            {
                switch (count)
                {
                    case 0:
                        bd.pulse = (Int32.Parse(i2));
                        break;
                    case 1:
                        bd.rpm = (Int32.Parse(i2));
                        break;
                    case 2:
                        bd.kmh = (Int32.Parse(i2));
                        break;
                    case 3:
                        bd.distance = (Int32.Parse(i2));
                        break;
                    case 4:
                        bd.reqPower = (Int32.Parse(i2));
                        break;
                    case 5:
                        bd.energy = (Int32.Parse(i2));
                        break;
                    case 6:
                        String[] times = i2.Split(separatingChar, System.StringSplitOptions.RemoveEmptyEntries);
                        bd.minutes = Int32.Parse(times[0]);
                        bd.seconds = Int32.Parse(times[1]);
                        break;
                    case 7:
                        bd.actPower = (Int32.Parse(i2));
                        break;

                }
                count++;
            }
            BikeList.Add(bd);
        }
        public override string ToString()
        {
            if (BikeList.Count != 0)
            {
                return BikeList[BikeList.Count - 1].ToString();
            }
            else
            {
                return "No bikedata yet";
            }
        }

        BikeData BikeCommunication.GetBikeData()
        {
            throw new NotImplementedException();
        }

       
    }
}
