using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Collections;
using System.Timers;

namespace Project21
{
    public class Bike
    {
        public BikeCommunicator bikeCom { get; }
        private static List<BikeData> records = new List<BikeData>();

        public Bike(string com)
        {
            string answer = com;
            Console.WriteLine("Connecting to " + answer.ToUpper());
            bikeCom = new BikeCommunicator(answer.ToUpper());
        }

        private void ProgramThread()
        {
            while (true)
            {
                Console.WriteLine(bikeCom.ToString());

                Thread.Sleep(500);
            }
        }

        private void ListenThread()
        {
            while (true)
            {
                bikeCom.SendCommand(Console.ReadLine());
            }
        }


    }
}
