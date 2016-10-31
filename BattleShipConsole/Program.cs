using System;
using System.Text.RegularExpressions;
using BattleShipModel;
using BattleShipService;

namespace BattleShipConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new Game();
            Console.Title = "Battleship";
            Console.WriteLine("Player 1 Name?");
            game.PlayerOne = new Player(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Player 2 Name?");
            game.PlayerTwo = new Player(Console.ReadLine());
            Console.WriteLine();

            InitializeShip(game, game.PlayerOne);
            Console.WriteLine();
            InitializeShip(game, game.PlayerTwo);
            Console.WriteLine();

            game.Start();

            while (!game.Over)
            {
                ProcessNextTurn(game);
            }
            Console.WriteLine("Congratulations " + game.Winner.Name + ", you sunk my battleship");
            Console.WriteLine("Game Over...");
            Console.WriteLine();
            Console.WriteLine(game.PlayerOne.Name + "'s Board");
            Console.WriteLine(game.PlayerOne.Board);
            Console.WriteLine();

            Console.WriteLine(game.PlayerTwo.Name + "'s Board");
            Console.WriteLine(game.PlayerTwo.Board);
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void ProcessNextTurn(Game game)
        {
            bool turnComplete = false;

            while (!turnComplete)
            {
                Console.WriteLine("{0}: Provide a location to hit {1}", game.CurrentPlayer.Name,
                    game.OpponentPlayer.Name);

                try
                {
                    Console.WriteLine(Enum.GetName(typeof (MarkerType), game.Call(Console.ReadLine())));
                    Console.WriteLine();
                    turnComplete = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid location, please try again");
                }
            }
        }

        private static void InitializeShip(Game game, Player player)
        {
            while (player.Board.Ships.Count == 0)
            {
                try
                {
                    Console.WriteLine("Please enter the ship location for {0}. Format: A3 A5", player.Name);

                    string input = Console.ReadLine();
                    if (input != null && Regex.IsMatch(input, "^[A-Ha-h]{1}[1-8]{1}[ ][A-Ha-h]{1}[1-8]{1}$"))
                    {
                        string start = input.Substring(0, 2);
                        string end = input.Substring(3, 2);
                        game.PlaceShip(player, ShipType.Cruiser, start, end);
                    }
                    else
                    {
                        Console.WriteLine("Invalid format, please try again");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid format, please try again");
                }
            }
        }
    }
}