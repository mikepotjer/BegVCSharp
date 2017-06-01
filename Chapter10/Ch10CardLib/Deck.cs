using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch10CardLib
{
    public class Deck
    {
        private Card[] cards;

        /// <summary>
        /// Initializes the deck by populating it with 52 Card objects.
        /// </summary>
        public Deck()
        {
            // Add 52 cards to the deck, with one card for each suit and rank combination.
            cards = new Card[52];
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    // The initial array will be populated sequentially. The Card class expects
                    // parameters of type Suit and Rank, so the integers need to be cast to
                    // those data types.
                    cards[suitVal * 13 + rankVal - 1] = new Card((Suit)suitVal, (Rank)rankVal);
                }
            }
        }

        /// <summary>
        /// Retrieves a card from the deck based on its position in the deck
        /// </summary>
        /// <param name="cardNum">The position of the card within the deck. This is a number
        /// betweeb 0 and 51.</param>
        /// <returns>A reference to the Card object in the specified position.</returns>
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 51)
                return cards[cardNum];
            else
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum,
                    "Value must be between 0 and 51."));
        }

        /// <summary>
        /// Shuffles all the cards in this deck.
        /// </summary>
        public void Shuffle()
        {
            // Create a temporary array to shuffle the cards into.
            Card[] newDeck = new Card[52];

            // Create an array to track which elements of the temporary array already have
            // a card assigned to them.
            bool[] assigned = new bool[52];

            // Use a Random class to select the positions to shuffle the cards into.
            Random sourceGen = new Random();

            // Shuffle each card in the cards field into the temporary array.
            for (int i = 0; i < 52; i++)
            {
                int destCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    // Generate random numbers from 0 - 51 until we find a position in the
                    // temporary array that doesn't have a card assigned to it.
                    destCard = sourceGen.Next(52);
                    if (assigned[destCard] == false)
                        foundCard = true;
                }

                //Set the flag that an empty position was find, and assign the current card
                // to it.
                assigned[destCard] = true;
                newDeck[destCard] = cards[i];
            }

            // Copy the shuffled deck back into the same instance of the cards field.
            // Note that cards = newDeck would create a new instance in cards, which could
            // cause problems if some other code was holding a reference to the original
            // instance of cards.
            newDeck.CopyTo(cards, 0);
        }
    }
}