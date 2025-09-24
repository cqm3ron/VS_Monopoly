using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal class Property
    {
        private static int idCounter = 0;

        public Property(string[] properties /*string aName, string aColour, string aOwner, int aPrice, int[] aRent*/)
        {
            idCounter++;
            foreach (string property in properties)
            {
                string[] data = property.Split(',');
                id = idCounter;
                name = data[0];
                colour = data[1];
                owner = "";
                price = int.Parse(data[2]);
                rent[0] = int.Parse(data[3]); // Rent
                rent[1] = int.Parse(data[4]); // Rent with colour set
                rent[2] = int.Parse(data[5]); // Rent with 1 house
                rent[3] = int.Parse(data[6]); // Rent with 2 houses
                rent[4] = int.Parse(data[7]); // Rent with 3 houses
                rent[5] = int.Parse(data[8]); // Rent with 4 houses
                rent[6] = int.Parse(data[9]); // Rent with hotel

            }

            //idCounter++;
            //id = idCounter;
            //name = aName;
            //colour = aColour;
            //owner = "";
            //price = aPrice;
            //rent = aRent;
        }


        public required int id;
        public required string name;
        public string colour;
        private string owner;
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public int price;
        public int[] rent = new int[4];
    }
}
