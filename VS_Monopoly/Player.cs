using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal class Player
    {
        public string name;
        public int id;
        public List<Property> properties;
        public double balance;
        
        public Player()
        {
            name = string.Empty;
            id = 0;
            properties = new List<Property>();
            balance = 0;
        }
    }
}
