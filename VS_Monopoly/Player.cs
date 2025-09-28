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
        private int currentPosition = 0; // matches property ID
        private string name;
        public int id;
        public List<int> properties; // store properties by ID
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
        public int CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;

                if (currentPosition >= Board.propertyCount)
                {
                    currentPosition %= Board.propertyCount;
                    Bank.Receive(200, this);
                    Console.SetCursorPosition(11, 24);
                    Console.WriteLine("You received £200 for passing GO!");
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

        public bool OwnsColourSet(string colour, List<Property> properties)
        {
            int existingColours = 0;
            int ownedColours = 0;
            foreach (Property property in properties)
            {
                if (property.colour == colour && property.Owner == this)
                {
                    existingColours++;
                    ownedColours++;
                }
                else if (property.colour == colour && property.Owner != this)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            if (existingColours == ownedColours)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Turn(List<Property> propertyData, List<Player> players)
        {
            Player player = players[Player.turnId - 1];
            
            while (true)
            {
                Board.Display(propertyData, players);
                player.rolledDouble = false;
                Console.Write($"Player {player.Name}: Press 'R' to roll the dice: ");
                while (Console.ReadKey(true).Key != ConsoleKey.R) ;
                (int d1, int d2, int dTotal) = Board.Dice();
                player.CurrentPosition += dTotal;
                Console.SetCursorPosition(11, 25);
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
                    // player.CurrentPosition = find jail? how to do that?
                }
                else
                {
                    Console.Write($"You rolled a {d1} & {d2}, giving you a total of {dTotal}.");
                }
                Console.SetCursorPosition(11, 26);
                if (!player.inJail)
                {
                    Console.Write($"This moved you to {propertyData[player.CurrentPosition].name}");
                    Board.DisplayPlayerInfo(propertyData, players);
                    Board.Dice(dice1:d1, dice2:d2);
                    propertyData[player.CurrentPosition].Actions(player);
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
