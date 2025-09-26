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
        private Player owner;
        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public int price;
        public int[] rent = new int[6];

        public void Actions(Player player)
        {
            if (name == "go")
            {
                // player.balance += 200; //do i need a method to increase balance? Don't think so but we shall see
                // update: im silly cause apparently this only works if you LAND on go not if u pass it LOL
            }
            else if (name == "income tax")
            {
                // Pay £200 (add pay method so bankruptcy happens)
            }
            else if (name == "super tax")
            {
                // Pay £100 (add pay method so bankruptcy happens)
            }
            else if (name == "chance")
            {
                // Draw a chance card (ughhhhhhhhhhhhh i have to program a bunch of possible chance cards in why did i pick this game to code)
            }
            else if (name == "community chest")
            {
                // Draw a community chest card (see above)
            }
            else if (name == "free parking")
            {
                // maybe the real free parking was the friends we made along the way
            }
            else if (name == "go to jail")
            {
                // Go to jail (criminal)
            }
            else if (name == "jail" && !player.inJail)
            {
                // Just visiting (wave at the criminals)
            }
            else if (name == "jail" && player.inJail)
            {
                // In jail (haha criminal get rekt)
            }
            else if (ownable && owner == null)
            {
                Console.SetCursorPosition(10, 26);
                Console.Write($"Buy {name} for £{price}? You have £{player.balance}");
                Console.SetCursorPosition(10, 27);
                Console.CursorVisible = true;
                Console.Write("Enter y or n: ");
                string purchase = Console.ReadLine();
                Console.CursorVisible = false; 
                if (purchase.ToLower() == "y")
                {
                    Bank.Pay(price, player);
                    player.properties.Add(id);
                    player.properties.Sort(); //sort by value later? is that even necessary?
                    Owner = player;
                    Console.SetCursorPosition(10, 28);
                    Console.WriteLine($"Player {player.Name} now owns {name}");
                    Console.SetCursorPosition(10, 29);
                    Console.WriteLine($"New balance: £{player.balance}");
                }
                else if (purchase.ToLower() == "n")
                {

                }
                else
                {
                    Console.WriteLine("");
                }

                // auction? ill do that later its too complicated rn)
            }
            else if (ownable && owner != null && owner.Name != player.Name)
            {
                // Pay rent to owner (or not if theyre in jail or something)
            }
            else if (ownable && owner != null && owner.Name == player.Name)
            {
                // Do nothing but houses and hotels maybe soon idk they are confusing
            }
        }
    }
}
