using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.IO.Ports;

namespace Project21
{

    class FakeBike
    {
        private int pulse = 10;
        private int speed = 2;
        private int distance = 0;
        private int power = 25;
        private int energy = 0;
        private int minutes;
        private int rpm = 40;
        private int seconds;
        private int actualPower = 60;

        System.Timers.Timer bikeUpdate = new System.Timers.Timer(100);
        System.Timers.Timer bikeTime = new System.Timers.Timer(1000);

        public FakeBike()
        {
            bikeUpdate.Elapsed += new ElapsedEventHandler(updateBike);
            bikeUpdate.Start();

            bikeTime.Elapsed += new ElapsedEventHandler(updateTime);
            bikeTime.Start();
        }

        public void setPower(int power)
        { this.power = power; }
        public void setSeconds(int seconds)
        { this.seconds = seconds; }
        public void setDistance(int distance)
        { this.distance = distance; }
        public void reset()
        {   pulse = 10;
            speed = 2;
            distance = 0;
            power = 25;
            energy = 0;
            minutes = 0;
            rpm = 40;
            seconds = 0;
            actualPower = 60;
        }
    

        private void updateBike(Object source, System.Timers.ElapsedEventArgs e)
        {
            pulse = new Random().Next(60, 140);
            speed = new Random().Next(1, 500); ;
            energy++;
            distance += (int)Math.Ceiling(speed / 3600.0);
        }

        private void updateTime(Object source, System.Timers.ElapsedEventArgs e)
        {
            seconds++;
            seconds = seconds++ % 60;
                if (seconds == 0)
                    minutes++;
        }

        public string getData()
        {
            return pulse + "\t" + rpm + "\t" + speed + "\t" + distance + "\t" + power + "\t" + energy + "\t" + minutes + ":" + seconds + "\t" + actualPower;
        }
    }
}
