using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch13CardLib
{
    /// <summary>
    /// The computer player is a special player with additional attributes to automate
    /// its play.
    /// </summary>
    [Serializable]
    public class ComputerPlayer : Player
    {
        private Random random = new Random();

        public ComputerSkillLevel Skill { get; set; }

        public override string PlayerName
        {
            get { return $"Computer {Index}"; }
        }

        /// <summary>
        /// Defines how the computer player draws cards from the deck.
        /// </summary>
        /// <param name="deck">A reference to the deck in use for the game.</param>
        /// <param name="availableCard">A reference to a discard card that is currently in play.</param>
        public void PerformDraw(Deck deck, Card availableCard)
        {
            switch (Skill)
            {
                case ComputerSkillLevel.Dumb:
                    // At this skill level, the computer just draws a new card every time.
                    DrawCard(deck);
                    break;
                default:
                    // For the other skill levels, the computer attempts to use the best card
                    // for the current hand.
                    DrawBestCard(deck, availableCard, (Skill == ComputerSkillLevel.Cheats));
                    break;
            }
        }

        /// <summary>
        /// Defines how the computer discards a card from its hand.
        /// </summary>
        /// <param name="deck">A reference to the deck in use for the game.</param>
        public void PerformDiscard(Deck deck)
        {
            switch (Skill)
            {
                case ComputerSkillLevel.Dumb:
                    // At this skill level, the computer discards a random card, regardless of
                    // how it affects the hand.
                    int discardIndex = random.Next(Hand.Count);
                    DiscardCard(Hand[discardIndex]);
                    break;
                default:
                    // For the other skill levels, the computer attempts to determine the least
                    // useful card for the hand, and discard that.
                    DiscardWorstCard();
                    break;
            }
        }

        /// <summary>
        /// Draws a card from the deck based on what is best for the player's current hand.
        /// </summary>
        /// <param name="deck">A reference to the deck in use for the game.</param>
        /// <param name="availableCard">A reference to the card available on the discard pile.</param>
        /// <param name="cheat">Indicates whether the computer player is cheating.</param>
        private void DrawBestCard(Deck deck, Card availableCard, bool cheat = false)
        {
            var bestSuit = CalculateBestSuit();
            if (availableCard.suit == bestSuit)
                // The card on the discard pile belongs to the most desireable suit for the
                // current hand, so take it.
                AddCard(availableCard);
            else if (cheat == false)
                // The card on the discard pile isn't very useful, so draw the next available
                // card from the deck.
                DrawCard(deck);
            else
                // The computer is cheating, so attempt to draw a card of the desired suit.
                AddCard(deck.SelectCardOfSpecificSuit(bestSuit));
        }

        /// <summary>
        /// Discards a card from the player's hand based on what is best for that hand.
        /// </summary>
        private void DiscardWorstCard()
        {
            var worstSuit = CalculateWorstSuit();

            // Locate the first available card of the worst suit in the player's current hand.
            foreach (Card card in Hand)
            {
                if (card.suit == worstSuit)
                {
                    DiscardCard(card);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines the best suit in the player's current hand.
        /// </summary>
        /// <returns>The Suit that has been determined to be the best.</returns>
        private Suit CalculateBestSuit()
        {
            // Use a Dictionary collection to determine how many cards of each suit are in the
            // player's current hand.
            Dictionary<Suit, List<Card>> cardSuits = new Dictionary<Suit, List<Card>>();
            cardSuits.Add(Suit.Club, new List<Card>());
            cardSuits.Add(Suit.Diamond, new List<Card>());
            cardSuits.Add(Suit.Heart, new List<Card>());
            cardSuits.Add(Suit.Spade, new List<Card>());

            int max = 0;

            // A default value is needed for the current suit, but it doesn't matter which one,
            // since it will change as needed.
            Suit currentSuit = Suit.Club;

            foreach (Card card in Hand)
            {
                cardSuits[card.suit].Add(card);

                // If the current suit contains more cards than any other (so far), flag it as
                // the best suit.
                if (cardSuits[card.suit].Count > max)
                {
                    max = cardSuits[card.suit].Count;
                    currentSuit = card.suit;
                }
            }
            return currentSuit;
        }

        /// <summary>
        /// Determines the worst suit in the player's current hand.
        /// </summary>
        /// <returns>The Suit that has been determined to be the worst.</returns>
        private Suit CalculateWorstSuit()
        {
            // Use a Dictionary collection to determine how many cards of each suit are in the
            // player's current hand.
            Dictionary<Suit, List<Card>> cardSuits = new Dictionary<Suit, List<Card>>();
            cardSuits.Add(Suit.Club, new List<Card>());
            cardSuits.Add(Suit.Diamond, new List<Card>());
            cardSuits.Add(Suit.Heart, new List<Card>());
            cardSuits.Add(Suit.Spade, new List<Card>());

            int min = Hand.Count;

            // A default value is needed for the current suit, but it doesn't matter which one,
            // since it will change as needed.
            Suit currentSuit = Suit.Club;

            // Populate the Dictionary.
            foreach (Card card in Hand)
            {
                cardSuits[card.suit].Add(card);
            }

            foreach (var item in cardSuits)
            {
                // If the current suit exists in the hand, and contains fewer cards than any
                // other (so far), flag it as the worst suit.
                if (item.Value.Count > 0 && item.Value.Count < min)
                {
                    min = item.Value.Count;
                    currentSuit = item.Key;
                }
            }
            return currentSuit;
        }
    }

}
