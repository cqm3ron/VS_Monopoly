using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal class Property
    {
        private static int idCounter = 0;
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
        public int rentLevel = 0;

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

        public static void UpdateRent()
        {
            Console.SetCursorPosition(11, 26);
            Console.Write("You landed on GO! Enjoy an extra £200.");
            Console.SetCursorPosition(11, 27);
            Console.Write("Press any key to continue.");
            Console.ReadKey(true);
        }

        public void Actions(Player player)
        {
            Console.SetCursorPosition(11, 27);
            if (name.ToLower() == "go")
            {
                Console.Write("You receive another £200, for a total of £400.");
                Bank.Receive(200, player);
                Console.ReadKey(true);
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
                Console.Write($"Buy {name} for £{price}? You have £{player.balance}");
                Console.SetCursorPosition(11, 28);
                Console.CursorVisible = true;
                Console.Write("Enter y or n: ");
                string purchase = Console.ReadLine();
                Console.CursorVisible = false;
                if (purchase.ToLower().Contains('y'))
                {
                    Bank.Pay(price, player);
                    player.properties.Add(id);
                    player.properties.Sort(); //sort by value later? is that even necessary?
                    Owner = player;
                    Console.SetCursorPosition(11, 29);
                    Console.Write("Player ");
                    Board.SelectTextColour(player);
                    Console.Write(player.Name);
                    Console.ResetColor();
                    Console.Write($" now owns {name}");

                    Console.SetCursorPosition(11, 30);
                    Console.WriteLine($"New balance: £{player.balance}");
                }
                else if (purchase.ToLower().Contains('n'))
                {
                    // Want to auction?
                }
                else
                {
                    // uhhhh what ok youre just bad at typing atp
                }
                Console.SetCursorPosition(11, 30);
                Console.Write("Press any key to continue.");
                Console.ReadKey(true);
            }
            else if (ownable && owner != null && owner.Name != player.Name)
            {
                // Pay rent to owner (or not if theyre in jail or something)
                if (!owner.inJail)
                {
                    Console.Write($"You landed on {owner.Name}'s property!");
                    Console.SetCursorPosition(11, 28);
                    Console.Write($"Since it is rent level {rentLevel}, you pay £{rent[rentLevel]}.");
                        
                    Bank.Pay(rent[rentLevel], owner, player);

                    Console.SetCursorPosition(11, 29);
                    Console.Write("New balances:");
                    Console.SetCursorPosition(11, 30);
                    Board.SelectTextColour(player);
                    Console.Write(player.Name);
                    Console.ResetColor();
                    Console.Write($": £{player.balance}");
                    Console.SetCursorPosition(11, 31);
                    Board.SelectTextColour(owner);
                    Console.Write(owner.Name);
                    Console.ResetColor();
                    Console.Write($": £{owner.balance}"); Console.SetCursorPosition(11, 32);
                    Console.Write("Press any key to continue.");
                    Console.ReadKey(true);
                }
            }
            else if (ownable && owner != null && owner.Name == player.Name)
            {
                // Do nothing but houses and hotels maybe soon idk they are confusing
            }
        }
    }
}
