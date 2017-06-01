using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch10CardLib
{
    public class Card
    {
        public readonly Rank rank;
        public readonly Suit suit;

        private Card()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates a card object of a specific suit and rank.
        /// </summary>
        /// <param name="newSuit">The suit to assign to the new card. This must be a value from the Suit enum.</param>
        /// <param name="newRank">The rank to assign to the new card. This must be a value from the Rank enum.</param>
        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }

        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }
    }
}