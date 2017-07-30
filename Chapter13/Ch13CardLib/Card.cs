using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch13CardLib
{
    // Include the ICloneable interface, to make it easy to get a copy of a card, rather than a
    // duplicate reference to the same card object.
    public class Card : ICloneable
    {
        public readonly Rank rank;
        public readonly Suit suit;

        /// <summary>
        /// Flag for trump usage. If true, trumps are valued higher than cards of other suits.
        /// </summary>
        public static bool useTrumps = false;

        /// <summary>
        /// Trump suit to use if useTrumps is true.
        /// </summary>
        public static Suit trump = Suit.Club;

        /// <summary>
        /// Flag that determines whether aces are higher than kings or lower than deuces.
        /// </summary>
        public static bool isAceHigh = true;

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

        #region Operator overloads
        public static bool operator ==(Card card1, Card card2)
            => (card1?.suit == card2?.suit) && (card1?.rank == card2?.rank);

        public static bool operator !=(Card card1, Card card2)
            => !(card1 == card2);

        public override bool Equals(object card) => this == (Card)card;

        public override int GetHashCode()
        {
            // Generate an integer that is unique to each suit and rank in the deck.
            return 13 * (int)suit + (int)rank;
        }

        public static bool operator >(Card card1, Card card2)
        {
            if (card1.suit == card2.suit)
            {
                // The cards are the same suit, so trumps are irrelevant.
                if (isAceHigh)
                {
                    // Aces are high, so we can't use the enum to compare aces.
                    if (card1.rank == Rank.Ace)
                    {
                        // The first card is an ace, so it beats everything except another ace.
                        if (card2.rank == Rank.Ace)
                            return false;
                        else
                            return true;
                    }
                    // The first card is not an ace.
                    else
                    {
                        // The first card loses if the second card is an ace, otherwise the first
                        // card beats if it out-ranks the second card.
                        if (card2.rank == Rank.Ace)
                            return false;
                        else
                            return (card1.rank > card2?.rank);
                    }
                }
                // Aces are not high.
                else
                {
                    // The enum is enough to determine if the first card beats.
                    return (card1.rank > card2?.rank);
                }
            }
            // The cards are not the same suit.
            else
            {
                // The first card always loses to a trump, otherwise it always wins if the second
                // card can't match the suit.
                if (useTrumps && (card2.suit == Card.trump))
                    return false;
                else
                    return true;
            }
        }

        public static bool operator <(Card card1, Card card2)
            => !(card1 >= card2);

        public static bool operator >=(Card card1, Card card2)
        {
            if (card1.suit == card2.suit)
            {
                // The cards are the same suit, so trumps are irrelevant.
                if (isAceHigh)
                {
                    if (card1.rank == Rank.Ace)
                    {
                        // The first card is an ace, so the second card can't beat it.
                        return true;
                    }
                    // The first card is not an ace.
                    else
                    {
                        // If the second card is an ace, it beats, otherwise compare rank.
                        if (card2.rank == Rank.Ace)
                            return false;
                        else
                            return (card1.rank >= card2.rank);
                    }
                }
                // Aces are not high.
                else
                {
                    // We just need to compare rank.
                    return (card1.rank >= card2.rank);
                }
            }
            // The cards are not the same suit.
            else
            {
                // The first card always loses to a trump, otherwise it always wins if the second
                // card can't match the suit.
                if (useTrumps && (card2.suit == Card.trump))
                    return false;
                else
                    return true;
            }
        }

        public static bool operator <=(Card card1, Card card2)
            => !(card1 > card2);
        #endregion
    }
}