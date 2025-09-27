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
        public static int turnId = 1;
        public int currentPosition = 0; //current location on board (matches property ID)
        private string name;
        public int id;
        public List<int> properties; //store properties by ID
        public double balance;
        public int consecutiveDoubles = 0;
        public bool inJail = false;
        public int jailTurns = 0;
        public bool rolledDouble = false;
        
        public static void IncrementTurnID()
        {
            turnId++;
            if (turnId > playerCount)
            {
                turnId = 1;
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
            Player player = players[Player.turnId - 1];

            while (true)
            {
                Board.Display(propertyData, players);
                //Console.WriteLine(playerCount + " " + turnId);
                player.rolledDouble = false;
                Console.Write($"Player {player.Name}: Press 'R' to roll the dice: ");
                while (Console.ReadKey(true).Key != ConsoleKey.R) ;
                (int d1, int d2, int dTotal) = Board.Dice();
                player.currentPosition += dTotal;
                Console.SetCursorPosition(10, 24);
                if (d1 == d2 && player.consecutiveDoubles < 3)
                {
                    player.rolledDouble = true;
                    player.consecutiveDoubles++;
                    Console.Write($"You rolled a double {d1}, giving you a total of {dTotal}.");
                }
                else if (d1 == d2 && player.consecutiveDoubles == 3)
                {
                    player.inJail = true;
                    player.consecutiveDoubles = 0;
                    // player.currentPosition = find jail? how to do that?
                }
                else
                {
                    Console.Write($"You rolled a {d1} & {d2}, giving you a total of {dTotal}.");
                }
                Console.SetCursorPosition(10, 25);
                if (!player.inJail)
                {
                    Console.Write($"This moved you to {propertyData[player.currentPosition].name}");
                    propertyData[player.currentPosition].Actions(player);
                }
                if (!player.rolledDouble)
                {
                    IncrementTurnID();
                    break;
                }
            }

        }
    }
}
