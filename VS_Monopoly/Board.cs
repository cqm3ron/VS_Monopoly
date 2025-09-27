using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VS_Monopoly.Properties;

namespace VS_Monopoly
{
    internal class Board
    {
        public static List<Property> Generate()
        {
            List<Property> propertyData;
            string[] properties = Resources.standard_uk.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            propertyData = Property.Setup(properties);
            return propertyData;
        }

        public static void Display(List<Property> propertyData, List<Player> players)
        {
            Console.Clear();
            Console.WriteLine("+------+------+------+------+------+------+------+------+------+------+------+");
            for (int i = 20; i < 31; i++)
            {
                DisplayProperty(propertyData[i]);
            }
            LineTerminator();
            Console.WriteLine("+------+------+------+------+------+------+------+------+------+------+------+");

            DisplayProperty(propertyData[19]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[31]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[18]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[32]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[17]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[33]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[16]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[34]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[15]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[35]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[14]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[36]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[13]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[37]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[12]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[38]);
            LineTerminator();
            Console.WriteLine("+------+                                                              +------+");

            DisplayProperty(propertyData[11]);
            LineTerminator();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + 8, Console.GetCursorPosition().Top - 3);
            Console.Write("                                                              ");
            DisplayProperty(propertyData[39]);
            LineTerminator();


            Console.WriteLine("+------+------+------+------+------+------+------+------+------+------+------+");
            for (int i = 10; i > 0; i--)
            {
                DisplayProperty(propertyData[i]);
            }
            DisplayProperty(propertyData[0]);
            LineTerminator();
            Console.Write("+------+------+------+------+------+------+------+------+------+------+------+");

            Console.SetCursorPosition(10, 5);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(@" ___ ___   ___   ____    ___   ____    ___   _      __ __ "); Console.SetCursorPosition(10, 6);
            Console.Write(@"|   T   T /   \ |    \  /   \ |    \  /   \ | T    |  T  T"); Console.SetCursorPosition(10, 7);
            Console.Write(@"| _   _ |Y     Y|  _  YY     Y|  o  )Y     Y| |    |  |  |"); Console.SetCursorPosition(10, 8);
            Console.Write(@"|  \_/  ||  O  ||  |  ||  O  ||   _/ |  O  || l___ |  ~  |"); Console.SetCursorPosition(10, 9);
            Console.Write(@"|   |   ||     ||  |  ||     ||  |   |     ||     Tl___, |"); Console.SetCursorPosition(10, 10);
            Console.Write(@"|   |   |l     !|  |  |l     !|  |   l     !|     ||     !"); Console.SetCursorPosition(10, 11);
            Console.Write(@"l___j___j \___/ l__j__j \___/ l__j    \___/ l_____jl____/"); Console.SetCursorPosition(10, 12);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            Console.SetCursorPosition(10, 13);
            Console.WriteLine("Players:");
            for (int i = 0; i < players.Count; i++)
            {
                Console.SetCursorPosition(10, 14 + i);
                if (Player.turnId == i + 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write($"{players[i].Name} - £{players[i].balance}");
                Console.WriteLine();
                Console.ResetColor();
            }

            Dice(true);
            Console.SetCursorPosition(10, 23);
        }

        private static void DisplayProperty(Property property)
        {
            do
            {
                Console.Write("|");
                Console.BackgroundColor = SelectColour(property);
                Console.Write("      ");
                Console.ResetColor();

                Console.SetCursorPosition(Console.GetCursorPosition().Left - 7, Console.GetCursorPosition().Top + 1);
                Console.Write('|');
                string[] propertyName = new string[1];
                propertyName = property.name.Split(' ');
                if (property.name.ToLower() == "go to jail")
                {
                    propertyName[0] = "Go To";
                    propertyName[1] = "Jail";
                }
                else if (property.name.ToLower().Contains("station"))
                {
                    if (property.name.ToLower().Contains("street"))
                    {
                        propertyName[0] = ($"{property.name[0]}{property.name[1]}{property.name[2]} St");
                    }
                    propertyName[1] = "Stat.";
                }
                else if (property.name.ToLower() == "jail")
                {
                    Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 1);
                    Console.Write("  Jail");
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 7, Console.GetCursorPosition().Top + 1);
                    Console.Write("|      ");
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 7, Console.GetCursorPosition().Top + 1);
                    Console.Write("|V'TING");
                    Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 2);
                    break;
                }
                else if (property.name.ToLower() == "go")
                {
                    propertyName = new string[2];
                    propertyName[0] = "  GO  ";
                    propertyName[1] = "      ";
                }
                else if (property.name.ToLower() == "community chest")
                {
                    propertyName = new string[2];
                    propertyName[0] = "CChest";
                    propertyName[1] = "      ";
                }
                else if (property.name.ToLower() == "chance")
                {
                    propertyName = new string[2];
                    propertyName[0] = "Chance";
                    propertyName[1] = "  ??  ";
                }

                    while (propertyName[0].Length < 6)
                    {
                        propertyName[0] += " ";
                    }
                if (propertyName[0].Length > 6)
                {
                    propertyName[0] = propertyName[0].Remove(6, propertyName[0].Length - 6);
                }
                Console.Write(propertyName[0]);
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 7, Console.GetCursorPosition().Top + 1);
                Console.Write("|");
                if (propertyName.Length > 1)
                {
                    while (propertyName[1].Length < 6)
                    {
                        propertyName[1] += " ";
                    }
                    if (propertyName[1].Length > 6)
                    {
                        propertyName[1] = propertyName[1].Remove(6, propertyName[1].Length - 6);
                    }
                    Console.Write(propertyName[1]);
                }
                else
                {
                    Console.Write("      ");
                }
                Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 2);
            } while (0 != 0);
        }

        private static void LineTerminator()
        {
            Console.Write("|");
            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top + 1);
            Console.Write("|");
            Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top + 1);
            Console.WriteLine("|");
        }

        private static ConsoleColor SelectColour(Property property)
        {
            switch (property.colour.ToLower()) 
            {
                case "brown":
                    return ConsoleColor.DarkMagenta;
                case "light blue":
                    return ConsoleColor.Cyan;
                case "pink":
                    return ConsoleColor.Magenta;
                case "orange":
                    return ConsoleColor.DarkYellow;
                case "red":
                    return ConsoleColor.Red;
                case "yellow":
                    return ConsoleColor.Yellow;
                case "green":
                    return ConsoleColor.Green;
                case "dark blue":
                    return ConsoleColor.DarkBlue;
                case "station":
                    return ConsoleColor.White;
                case "utility":
                    return ConsoleColor.Gray;
                default:
                    return ConsoleColor.Black;

            }
        }

        public static (int, int, int) Dice(bool empty = false)
        {
            int dice1 = Random.Shared.Next(1, 7);
            int dice2 = Random.Shared.Next(1, 7);
            int total = dice1 + dice2;
            Console.SetCursorPosition(36, 14);

            if (empty)
            {
                Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|       |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|   ?   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|       | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("+-------+'   ");
                Console.SetCursorPosition(Console.GetCursorPosition().Left + 2, Console.GetCursorPosition().Top - 6);
                Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|       |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|   ?   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("|       | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                Console.Write("+-------+'   ");
            }
            else
            {
                switch (dice1)
                {
                    case 1:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 2:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o     |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|     o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 3:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o     |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|     o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 4:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 5:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 6:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                }

                Console.SetCursorPosition(Console.GetCursorPosition().Left + 2, Console.GetCursorPosition().Top - 6);
                switch (dice2)
                {
                    case 1:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 2:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o     |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|     o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 3:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o     |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|     o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 4:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|       |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 5:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("|   o   |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                    case 6:
                        Console.Write("   .+-------+"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write(" .'       .'|"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'  |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   |"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o |   +"); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("| o   o | .' "); Console.SetCursorPosition(Console.GetCursorPosition().Left - 13, Console.GetCursorPosition().Top + 1);
                        Console.Write("+-------+'   ");
                        break;
                }
            }
            return (dice1, dice2, total);
        }
    }
}
