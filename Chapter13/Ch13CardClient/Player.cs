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
        /// Determines if the player has won the game, based on the rules of Rummy. Note that it is
        /// assumed that the game is being played with only a single deck of cards, so that the player
        /// can never have more than one card of the same suit and rank.
        /// </summary>
        /// <returns>true if the player has a winning hand.</returns>
        public bool HasWon()
        {
            //bool won = true;
            //Suit match = PlayHand[0].suit;

            //// For now, a simple winning condition is supported where all the cards are the
            //// same suit.
            //for (int i = 1; i < PlayHand.Count; i++)
            //{
            //    // won &= PlayHand[i].suit == match;
            //    if (PlayHand[i].suit != match)
            //    {
            //        won = false;
            //        break;
            //    }
            //}
            bool won = false;

            // Copy the hand to a new list, and apply the default sort order by rank.
            Cards remainingCards = (Cards)PlayHand.Clone();
            remainingCards.Sort();

            Cards threeOfAKindCards = new Cards();
            Cards fourOfAKindCards = new Cards();
            Cards sequenceOfFourCards = new Cards();

            // Do the simpler check for 3-of-a-kind or 4-of-a-kind first.
            int cardIndex = 0;
            do
            {
                // Before checking for 4-of-a-kind, make sure we have 4 cards left to check.
                if ((cardIndex + 3) < remainingCards.Count
                    && remainingCards[cardIndex].rank == remainingCards[cardIndex + 3].rank)
                {
                    // This is a range of 4 cards with the same rank. Copy the range to the four of a
                    // kind collection, and remove the range from the temporary hand.
                    fourOfAKindCards.AddRange(remainingCards.GetRange(cardIndex, 4));
                    remainingCards.RemoveRange(cardIndex, 4);
                }
                else if (remainingCards[cardIndex].rank == remainingCards[cardIndex + 2].rank)
                {
                    // Player already has a set of 3, so he can't possibly win with a second set of 3.
                    if (threeOfAKindCards.Count > 0)
                        return false;
                    else
                    {
                        // We didn't find a range of 4 cards, but did find a range of 3 cards with the
                        // same rank.
                        threeOfAKindCards.AddRange(remainingCards.GetRange(cardIndex, 3));
                        remainingCards.RemoveRange(cardIndex, 3);
                    }
                }
                else
                {
                    // The current card isn't part a range of the same rank, so check the next card.
                    cardIndex++;
                }
                // Stop checking once there are less than 3 cards left.
            } while (cardIndex < (remainingCards.Count - 2));

            if (remainingCards.Count == 0)
            {
                // Player has a set of 3 and a set of 4. Winner!
                return true;
            }

            // Sort the remaining cards by suit then rank, to check for sequences.
            remainingCards.Sort(CardComparerSuit.Default);

            if (remainingCards.Count == 7)
            {
                // With 7 cards remaining, there MUST be a sequence of 4 to win.
                sequenceOfFourCards.AddRange(remainingCards.GetRange(0, 4));
                if (IsSequence(sequenceOfFourCards))
                    // The first 4 cards form a sequence, so remove them from the hand.
                    remainingCards.RemoveRange(0, 4);
                else
                {
                    // The first 4 cards are not a sequence, so check if the last 4 are a sequence.
                    sequenceOfFourCards.AddRange(remainingCards.GetRange(3, 4));
                    if (IsSequence(sequenceOfFourCards))
                        // The last 4 are a sequence so remove them from the hand.
                        remainingCards.RemoveRange(3, 4);
                    else
                        // There isn't a sequence of 4, so this is not a winning hand.
                        return false;
                }

                // The hand contains a sequence of 4. This can only be a winning hand if the remaining
                // cards form a sequence.
                won = IsSequence(remainingCards);
            }
            else if (remainingCards.Count == 4)
                // The hand contained 3-of-a-kind. This can only be a winning hand if the remaining 4
                // cards form a sequence.
                won = IsSequence(remainingCards);
            else
            {
                // The hand contained 4-of-a-kind. First check if the remaining 3 cards form a sequence.
                if (IsSequence(remainingCards))
                    won = true;
                else
                {
                    // The remaining 3 cards are not a sequence, however, it's possible that by taking
                    // one card of the same suit from the 4-of-a-kind hand, we could form a sequence.
                    remainingCards.Add(new Card(remainingCards[0].suit, fourOfAKindCards[0].rank));
                    won = IsSequence(remainingCards);
                }
            }

            return won;
        }

        /// <summary>
        /// Checks the cards in the specified collection to determine if they form a sequence of Cards,
        /// all of the same suit (ex. 3H, 4H, 5H, 6H).
        /// </summary>
        /// <param name="cards">A collection of 3 or 4 Card objects to check</param>
        /// <returns>True if all cards in the collection are in sequence, otherwise false</returns>
        private bool IsSequence(Cards cards)
        {
            bool isSequence = false;

            // Make sure the cards are sorted by suit, then rank.
            cards.Sort(CardComparerSuit.Default);

            Card firstCard = cards[0];
            Card lastCard = cards[cards.Count - 1];

            if (firstCard.suit != lastCard.suit)
                // It can't be a sequence if the first and last card are not the same suit.
                isSequence = false;
            else if (lastCard.rank == Rank.Ace)
                // The Ace is the last card, which means Ace is high. The preceding cards must all be
                // in sequence from King down.
                isSequence = (firstCard.rank == (Rank.King - (cards.Count - 2)));
            else
                // Ace is not high.
                isSequence = (firstCard.rank == (lastCard.rank - (cards.Count - 1)));

            return isSequence;
        }
    }
}
