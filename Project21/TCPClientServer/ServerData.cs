using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientServer 
{
    [Serializable]
    public class ServerData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public List<string> Results { get; set; } = new List<string>();

        public ServerData(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public ServerData()
        {
        }
    }
}
