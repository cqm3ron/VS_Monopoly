using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using VS_Monopoly.Properties;

namespace VS_Monopoly
{
    internal class BoardGenerator
    {
        public static List<Property> Generate()
        {
            List<Property> propertyData;
            string[] properties = Resources.standard_uk.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            propertyData = Property.Setup(properties);
            return propertyData;
        }

        public static void Display(List<Property> propertyData)
        {
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
            LineTerminator();
            Console.WriteLine("+------+------+------+------+------+------+------+------+------+------+------+");
            //Console.WriteLine("|");
            //DisplayProperty(propertyData[19]);
            //Console.Write("|                                                              ");
            //DisplayProperty(propertyData[31]);
            //Console.WriteLine("|");


            // TODO: add code to make the board look less like a variable and more like a board cause no one wants to play monopoly on a list of variables
            // TODO: add train station logic in property.cs cause currently it detects if theres a station but does nothing about it
            // TODO: fix the horrendous sight that is program.cs
            // TODO: make it work (aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah)
        }

        private static void DisplayProperty(Property property)
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
            if (property.name.ToLower().Contains("station"))
            {
                if (property.name.ToLower().Contains("street"))
                {
                    propertyName[0] = ($"{property.name[0]}{property.name[1]}{property.name[2]} St");
                }
                propertyName[1] = "Stat.";
            }
            if (property.name.ToLower() == "jail")
            {
                propertyName[0] = "  Jail";
                propertyName[1] = "V'TING";
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
                    return ConsoleColor.DarkCyan;
                case "light blue":
                    return ConsoleColor.Blue;
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
    }

}
