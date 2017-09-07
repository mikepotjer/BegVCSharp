using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ch13CardLib;

namespace Ch13CardClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display introduction.
            WriteLine("BenjaminCards: a new and exciting version of Rummy.");
            WriteLine("To win you must have 2 sets of cards - a set of 3 and a set of 4.");
            WriteLine("There are 2 types of sets:");
            WriteLine("a) A set of 3 or 4 cards of the same rank (such as 2H, 2D, 2S)");
            WriteLine("b) A sequence of 3 or 4 cards of the same suit (such as 3H, 4H, 5H, 6H)");
            WriteLine();

            // Prompt for number of players.
            bool inputOK = false;
            int choice = -1;
            do
            {
                WriteLine("How many players (2-7)?");
                string input = ReadLine();
                try
                {
                    // Attempt to convert input into a valid number of players.
                    choice = Convert.ToInt32(input);
                    if ((choice >= 2) && (choice <= 7))
                        inputOK = true;
                }
                catch
                {
                    // Ignore failed conversions, just continue prompting.
                }
            } while (inputOK == false);

            // Initialize array of Player objects.
            Player[] players = new Player[choice];

            // Get player names.
            for (int p = 0; p < players.Length; p++)
            {
                WriteLine($"Player {p + 1}, enter your name:");
                string playerName = ReadLine();
                players[p] = new Player(playerName);
            }

            // Start the game.
            Game newGame = new Game();
            newGame.SetPlayers(players);
            int whoWon = newGame.PlayGame();

            // Display winning player.
            WriteLine($"{players[whoWon].Name} has won the game!");

            ReadKey();
        }
    }
}
