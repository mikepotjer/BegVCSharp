using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch10CardLib
{
    public class Card
    {
        private readonly Rank rank;
        private readonly Suit suit;

        private Card()
        {
            throw new System.NotImplementedException();
        }

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