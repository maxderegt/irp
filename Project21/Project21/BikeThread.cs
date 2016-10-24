using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project21
{
    class BikeThread
    {
        private int tempInt;
        public BikeThread(int crazyMen)
        {
            tempInt = crazyMen;
        }

        public void printData()
        {
            Console.WriteLine("Data from crazy men: " + tempInt);
        }
    }
}
