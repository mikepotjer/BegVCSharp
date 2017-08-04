using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ch13CardLib;

namespace Ch13CardClient
{
    public class Player
    {
        public string Name { get; private set; }
        public Cards PlayHand { get; private set; }

        // Hide the default constructor so the non-default one must be used.
        private Player() { }

        // Add a constructor to initialize the player.
        public Player(string name)
        {
            Name = name;
            PlayHand = new Cards();
        }

        /// <summary>
        /// Determines if the player has won the game.
        /// </summary>
        /// <returns>true if the player has a winning hand.</returns>
        public bool HasWon()
        {
            bool won = true;
            Suit match = PlayHand[0].suit;

            // For now, a simple winning condition is supported where all the cards are the
            // same suit.
            for (int i = 1; i < PlayHand.Count; i++)
            {
                // won &= PlayHand[i].suit == match;
                if (PlayHand[i].suit != match)
                {
                    won = false;
                    break;
                }
            }
            return won;
        }
    }
}
