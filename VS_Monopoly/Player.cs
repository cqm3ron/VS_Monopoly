using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VS_Monopoly
{
    internal class Player
    {
        public static int playerCount;
        private static int turnId = 1;
        public int currentPosition = 0; //current location on board (matches with property ID)
        private string name;
        public int id;
        public List<int> properties; //store properties by ID
        public double balance;
        public int consecutiveDoubles = 0;
        public bool inJail = false;
        public int jailTurns = 0;

        public static int TurnId
        {
            get { return turnId; }
            set
            {
                if (turnId > Player.playerCount)
                {
                    turnId = 1;
                }
                else
                {
                    turnId = value;
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length > 15) {
                    name = value.Substring(0, 15);
                }
                else
                {
                    name = value;
                }
            }
        }

        public Player()
        {
            name = string.Empty;
            id = 0;
            properties = new List<int>();
            balance = 0;
        }
    
        public static void Turn(List<Property> propertyData, List<Player> players)
        {
            Console.Write($"Player {players[Player.TurnId - 1].Name}: Press 'R' to roll the dice: ");
            while (Console.ReadKey(true).Key != ConsoleKey.R) ;
            (int d1, int d2, int dTotal) = Board.Dice();
            players[Player.TurnId - 1].currentPosition += dTotal;
            Console.SetCursorPosition(10, 24);
            Console.Write($"You rolled a {d1} & {d2}, giving you a total of {dTotal}.");
            Console.SetCursorPosition(10, 25);
            Console.Write($"This moved you to {propertyData[players[Player.TurnId - 1].currentPosition].name}");
            propertyData[players[Player.TurnId - 1].currentPosition].Actions(players[Player.TurnId - 1]);
        }
    }
}
