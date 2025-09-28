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
        public static int propertyCount = 0;

        public static List<Property> Generate()
        {
            List<Property> propertyData;
            string[] properties = Resources.standard_uk.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            propertyData = Property.Setup(properties);
            propertyCount = propertyData.Count;
            return propertyData;
        }

        public static void Display(List<Property> propertyData, List<Player> players)
        {
            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(80, 47);
            }

            Console.Clear();

            Console.SetCursorPosition(1, 1);

            for (int i = 20; i < 31; i++)
            {
                DisplayProperty(propertyData[i], players);
            }
            
            for (int i = 19; i > 9; i--)
            {
                Console.SetCursorPosition(1, Console.GetCursorPosition().Top + 4);
                DisplayProperty(propertyData[i], players);
            }
            
            Console.SetCursorPosition(71, 1);
            for (int i = 31; i < 40; i++)
            {
                Console.SetCursorPosition(71, Console.GetCursorPosition().Top + 4);
                DisplayProperty(propertyData[i], players);
            }

            Console.SetCursorPosition(8, 41);
            for (int i = 9; i >= 0; i--)
            {
                DisplayProperty(propertyData[i], players);
            }

            Console.SetCursorPosition(11, 6);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(@" ___ ___   ___   ____    ___   ____    ___   _      __ __ "); Console.SetCursorPosition(11, 7);
            Console.Write(@"|   T   | /   \ |    \  /   \ |    \  /   \ | |    |  T  |"); Console.SetCursorPosition(11, 8);
            Console.Write(@"| _   _ |Y     Y|  _  YY     Y|  o  )Y     Y| |    |  |  |"); Console.SetCursorPosition(11, 9);
            Console.Write(@"|  \_/  ||  O  ||  |  ||  O  ||   _/ |  O  || l___ |  ~  |"); Console.SetCursorPosition(11, 10);
            Console.Write(@"|   |   ||     ||  |  ||     ||  |   |     ||     Tl___, |"); Console.SetCursorPosition(11, 11);
            Console.Write(@"|   |   |:     !|  |  |l     !|  |   l     !|     ||     !"); Console.SetCursorPosition(11, 12);
            Console.Write(@"l___j___| \___/ l__j__j \___/ l__j    \___/ l_____jl____/ "); Console.SetCursorPosition(11, 13);
            Console.ResetColor();

            DisplayPlayerInfo(propertyData, players);

            if (OperatingSystem.IsWindows())
            {
                Console.SetBufferSize(80, 47);
                Console.SetWindowSize(80, 47);
                //Console.MoveBufferArea(0, 0, 80, 47, 1, 1);
            }
            Dice(true);
            Console.SetCursorPosition(11, 25);
        }

        private static void DisplayProperty(Property property, List<Player> players)
        {
            do
            {
                Console.Write("+------+");
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                Console.Write('|');
                Console.BackgroundColor = SelectColour(property);
                Console.Write("      ");
                Console.ResetColor();
                Console.Write('|');
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                Console.Write('|');
                string[] propertyName = new string[3];
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
                    Console.Write('|');
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                    Console.Write("|      |");
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                    Console.Write('|');
                    Console.Write("V'TING");
                    Console.Write('|');
                    Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                    Console.Write("+------+");
                    Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 2);
                    break;
                }
                else if (property.name.ToLower() == "go")
                {
                    propertyName = new string[2];
                    propertyName[0] = "  GO  ";
                    propertyName[1] = "      ";
                }
                else if (property.name.ToLower().Contains("community chest"))
                {
                    propertyName[0] = "CChest";
                    propertyName[1] = "  #" + propertyName[2] + "  ";
                }
                else if (property.name.ToLower().Contains("chance"))
                {
                    propertyName[0] = "Chance";
                    propertyName[1] = "  #" + propertyName[1] + "  ";
                }

                while (propertyName[0].Length < 6)
                {
                    propertyName[0] += " ";
                }

                if (propertyName[0].Length > 6)
                {
                    propertyName[0] = propertyName[0].Remove(6, propertyName[0].Length - 6);
                }

                if (property.Owner != null)
                {
                    SelectTextColour(property);

                }
                Console.Write(propertyName[0]);
                Console.ResetColor();
                Console.Write('|');
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                Console.Write('|');
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

                    if (property.Owner != null)
                    {
                        SelectTextColour(property);
                    }

                    Console.Write(propertyName[1]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("      ");
                }
                Console.Write('|');
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 8, Console.GetCursorPosition().Top + 1);
                Console.Write("+------+");
                Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top - 4);
            } while (0 != 0);
        }

        public static void DisplayPlayerInfo(List<Property> propertyData, List<Player> players)
        {
            Console.SetCursorPosition(11, 15);
            Console.WriteLine("Players:");
            for (int i = 0; i < players.Count; i++)
            {
                switch (i + 1)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 8:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                }

                Console.SetCursorPosition(11, 16 + i);
                if (Player.turnId == i + 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                string data = $"{players[i].Name} - £{players[i].balance} - {propertyData[players[i].CurrentPosition].name}";
                int spacesCounter = 0;

                while (data.Length < 30)
                {
                    data += " ";
                    spacesCounter++;
                }

                string dataToWrite = data.TrimEnd() ;
                Console.Write(dataToWrite);
                
                Console.ResetColor();
                for (int j = 0; j < spacesCounter; j++)
                {
                    Console.Write(' ');
                }

                Console.WriteLine();
                Console.ResetColor();
            }
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

        private static void SelectTextColour(Property property)
        {
            switch (property.Owner.id)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
            }
        }
        public static void SelectTextColour(Player player)
        {
            switch (player.id)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
            }
        }

        public static (int, int, int) Dice(bool empty = false, int dice1 = -1, int dice2 = -1)
        {
            if (dice1 == -1)
            {
                dice1 = Random.Shared.Next(1, 7);
            }
            if (dice2 == -1)
            {
                dice2 = Random.Shared.Next(1, 7);
            }

            int total = dice1 + dice2;
            Console.SetCursorPosition(41, 15);

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