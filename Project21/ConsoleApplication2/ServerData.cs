using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer 
{
    [Serializable]
    public class ServerData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Doctor { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public List<string> Results { get; set; } = new List<string>();

        public ServerData(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public ServerData(string name, string password, string Doctor)
        {
            Name = name;
            Password = password;
            if (Doctor.Equals("true")) this.Doctor = true;
            else this.Doctor = false;
        }

        public ServerData()
        {
        }
    }
}
