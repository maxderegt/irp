using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project21
{
    class BikeData
    {
        public int bikeDataID;
        public static int BikeDataCount = 0;
        public int pulse;
        public int rpm;
        public int kmh;
        public int distance;
        public int reqPower;
        public int energy;
        public int minutes;
        public int seconds;
        public int actPower;

        public BikeData()
        {
            BikeDataCount++;
            bikeDataID = BikeDataCount;
            pulse = 0;
            rpm = 0;
            kmh = 0;
            distance = 0;
            reqPower = 0;
            energy = 0;
            minutes = 0;
            seconds = 0;
            actPower = 0;
        }

        public BikeData(int pulse, int rpm, int kmh, int distance, int reqPower, int energy, int minutes, int seconds, int actPower)
        {
            BikeDataCount++;
            bikeDataID = BikeDataCount;
            this.pulse = pulse;
            this.rpm = rpm;
            this.kmh = kmh;
            this.distance = distance;
            this.reqPower = reqPower;
            this.energy = energy;
            this.minutes = minutes;
            this.seconds = seconds;
            this.actPower = actPower;
        }

        public override string ToString()
        {
            return
                "Bike Data ID : " + bikeDataID + "\n" +
                "Heartrate : " + pulse + " Hz" + "\n" +
                "RPM : " + rpm + "\n" +
                "Speed : " + (kmh / 10.0) + " km/h" + "\n" +
                "Distance : " + (distance / 10.0) + " km" + "\n" +
                "Req Power : " + reqPower + " Watt" + "\n" +
                "Burned nergy : " + energy + " kJ" + "\n" +
                "Duration : " + minutes + ":" + seconds + "\n" +
                "Actual Power : " + actPower + " Watt";
        }

        public string GetAll()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + bikeDataID + " " + pulse + " " + rpm + " " + kmh + " " + distance + " " + reqPower + " "+ energy + " " + minutes + " " + seconds + " " +actPower;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().IsAssignableFrom(this.GetType()))
                return this.bikeDataID == ((BikeData)obj).bikeDataID;
            else
                return false;
        }

        public bool Equals(BikeData bd)
        {
            return this.bikeDataID == bd.bikeDataID;
        }
    }
}
