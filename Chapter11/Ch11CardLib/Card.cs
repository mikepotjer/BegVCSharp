using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch11CardLib
{
    // Include the ICloneable interface, to make it easy to get a copy of a card, rather than a
    // duplicate reference to the same card object.
    public class Card : ICloneable
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

        /// <summary>
        /// This method implements the Clone() method required to satisfy the ICloneable interface.
        /// No deep copying is necessary for a card, so the default MemberwiseClone() is sufficient.
        /// </summary>
        /// <returns>An object matching the current Card object.</returns>
        public object Clone() => MemberwiseClone();
    }
}