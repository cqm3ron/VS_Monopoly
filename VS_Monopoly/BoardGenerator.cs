using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace VS_Monopoly
{
    internal class BoardGenerator
    {
        public static void Generate()
        {
            List<Property> board = new List<Property>();
            string[] properties = File.ReadAllLines("properties.csv");

            }
    }
}
