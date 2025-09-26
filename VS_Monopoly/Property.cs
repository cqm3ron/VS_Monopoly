using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal class Property
    {
        private static int idCounter = 0;

        public static List<Property> Setup(string[] properties)
        {
            List<Property> propertiesOut = new List<Property>();
            string[] data;
            string name;
            string colour;
            bool ownable;
            int price;
            int[] rent;

            foreach (string property in properties)
            {
                name = "";
                colour = "";
                ownable = false;
                price = 0;
                rent = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

                data = property.Split(',');

                name = data[0];
                colour = data[1];
                ownable = bool.Parse(data[2]);
                if (ownable)
                {
                    price = int.Parse(data[3]);
                    try
                    {
                        rent[0] = int.Parse(data[4]); // Rent
                        rent[1] = int.Parse(data[5]); // Rent with colour set
                        rent[2] = int.Parse(data[6]); // Rent with 1 house
                        rent[3] = int.Parse(data[7]); // Rent with 2 houses
                        rent[4] = int.Parse(data[8]); // Rent with 3 houses
                        rent[5] = int.Parse(data[9]); // Rent with 4 houses
                        rent[6] = int.Parse(data[10]); // Rent with hotel
                        propertiesOut.Add(new Property(name, colour, ownable, price, rent));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        propertiesOut.Add(new Property(name, "station", true, price));
                    }

                }
                else
                {
                    propertiesOut.Add(new Property(name, colour, false));
                }
            }


            return propertiesOut;

        }

        public Property(string aName, string aColour, bool aOwnable, int aPrice, int[] aRent)
        {
            idCounter++;
            id = idCounter;
            ownable = aOwnable;
            name = aName;
            colour = aColour;
            price = aPrice;
            rent = aRent;
        }
        public Property(string aName, string aColour, bool aOwnable)
        {
            idCounter++;
            id = idCounter;
            ownable = aOwnable;
            name = aName;
            colour = aColour;
        }
        public Property(string aName, string aColour, bool aOwnable, int aPrice)
        {
            idCounter++;
            id = idCounter;
            ownable = aOwnable;
            name = aName;
            colour = aColour;
            price = aPrice;
        }

        public int id;
        public string name;
        public bool ownable = true;
        public string colour = "";
        private string owner = "";
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public int price;
        public int[] rent = new int[6];
    }
}
