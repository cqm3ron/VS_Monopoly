using System.Net.Http.Headers;
using System.Runtime.InteropServices;


namespace VS_Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //try
            {
                Console.CursorVisible = true;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("How many players? (2-8)");
                        Player.playerCount = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == -2146233033)
                        {
                            Console.WriteLine("That wasn't a number. Press any key to try again.");
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                            Console.WriteLine("Press any key to exit");
                            Console.ReadKey();
                            Environment.Exit(-1);
                        }
                    }
                }
                if (Player.playerCount < 2 || Player.playerCount > 8) throw new ArgumentOutOfRangeException("Number of players must be between 2 and 8.");
                Console.CursorVisible = false;

                List<Player> players = new List<Player>();
                for (int i = 0; i < Player.playerCount; i++)
                {
                    players.Add(new Player());
                    players[i].id = i + 1;
                    players[i].balance = 1500;
                    Console.WriteLine($"Enter name for Player {i + 1}:");
                    Console.CursorVisible = true;
                    players[i].Name = Console.ReadLine();
                    Console.CursorVisible = false;
                    if (players[i].Name == "") players[i].Name = $"{i + 1}";
                }

                List<Property> propertyData = Board.Generate();
                //eh so apparently you're supposed to have the highest roll go first but i can sort that out later (maybe if i can be bothered probably not but i probably should be cause like a levels and all that
                while (true)
                {
                    //Board.Display(propertyData, players);
                    Player.Turn(propertyData, players);
                }

            }

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
    }
}