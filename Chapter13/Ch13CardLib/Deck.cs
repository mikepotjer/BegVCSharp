using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch13CardLib
{
    public delegate void LastCardDrawnHandler(Deck currentDeck);

    // Allow this class to be cloned, we can obtain a new instance, not simply a duplicate
    // reference to the same object.
    public class Deck : ICloneable
    {
        // Add an event that will allow subscribers to reshuffle the deck when the last
        // card is drawn.
        public event LastCardDrawnHandler LastCardDrawn;

        // Create a field based on the new Cards collection class to manage the deck.
        private Cards cards = new Cards();

        /// <summary>
        /// Initializes the deck by populating it with 52 Card objects.
        /// </summary>
        public Deck()
        {
            InsertAllCards();
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high.
        /// "this" is used so that the default constructor is called before the nondefault one.
        /// </summary>
        /// <param name="isAceHigh">Flag indicating whether aces should be set high.</param>
        public Deck(bool isAceHigh) : this()
        {
            Card.isAceHigh = isAceHigh;
        }

        /// <summary>
        /// Nondefault contructor. Allows a trump suit to be used.
        /// </summary>
        /// <param name="useTrumps">Flag indicating whether to use a trump suit.</param>
        /// <param name="trump">The Suit to use as trump.</param>
        public Deck(bool useTrumps, Suit trump) : this()
        {
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high and a trump suit to be used.
        /// </summary>
        /// <param name="isAceHigh">Flag indicating whether aces should be set high.</param>
        /// <param name="useTrumps">Flag indicating whether to use a trump suit.</param>
        /// <param name="trump">The Suit to use as trump.</param>
        public Deck(bool isAceHigh, bool useTrumps, Suit trump) : this()
        {
            Card.isAceHigh = isAceHigh;
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        /// <summary>
        /// This constructor allows us to directly modify the cards contained in the deck.
        /// Since its only purpose is to be used internally (to simplify the implementation
        /// of the Clone() method), it's declared protected.
        /// </summary>
        /// <param name="newCards"></param>
        protected Deck(Cards newCards)
        {
            cards = newCards;
        }

        /// <summary>
        /// Provides external access to the number of cards in the deck.
        /// </summary>
        public int CardsInDeck
        {
            get { return cards.Count; }
        }

        /// <summary>
        /// Retrieves a card from the deck based on its position in the deck
        /// </summary>
        /// <param name="cardNum">The position of the card within the deck. This is a number
        /// between 0 and 51.</param>
        /// <returns>A reference to the Card object in the specified position.</returns>
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 51)
            {
                // Raise an event when the last card is drawn.
                if ((cardNum == 51) && (LastCardDrawn != null))
                    LastCardDrawn(this);

                return cards[cardNum];
            }
            else
                throw new CardOutOfRangeException(cards.Clone() as Cards);
        }

        /// <summary>
        /// Shuffles all the cards in this deck.
        /// </summary>
        public void Shuffle()
        {
            // Create a temporary collection to shuffle the cards into.
            Cards newDeck = new Cards();

            // Create an array to track which elements of the temporary array already have
            // a card assigned to them.
            bool[] assigned = new bool[cards.Count];

            // Use a Random class to select the positions to shuffle the cards into.
            Random sourceGen = new Random();

            // Shuffle each card in the cards field into the temporary collection.
            for (int i = 0; i < cards.Count; i++)
            {
                int sourceCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    // Generate random numbers from 0 to the number of cards in the deck until
                    // we find a position in the temporary array that doesn't have a card
                    // assigned to it.
                    sourceCard = sourceGen.Next(cards.Count);
                    if (assigned[sourceCard] == false)
                        foundCard = true;
                }

                //Set the flag that an empty position was found, and assign the current card
                // to it.
                assigned[sourceCard] = true;
                newDeck.Add(cards[sourceCard]);
            }

            // Copy the shuffled deck back into the same instance of the cards field.
            // Note that cards = newDeck would create a new instance in cards, which could
            // cause problems if some other code was holding a reference to the original
            // instance of cards.
            newDeck.CopyTo(cards);
        }

        /// <summary>
        /// Reshuffles all cards that are not currently in play.
        /// </summary>
        /// <param name="cardsInPlay">A collection containing all the cards that are
        /// currently in play</param>
        public void ReshuffleDiscarded(List<Card> cardsInPlay)
        {
            // Populate the deck with all cards except for those that are already in play,
            // and shuffle those cards.
            InsertAllCards(cardsInPlay);
            Shuffle();
        }

        /// <summary>
        /// Draws the next card from the deck.
        /// </summary>
        /// <returns>A Card object from the "top" of the cards collection field.</returns>
        public Card Draw()
        {
            // If there aren't any cards left in the deck, there's nothing to do.
            if (cards.Count == 0)
                return null;

            // Get a reference to the first card in the collection field, then remove that
            // card from the collection (since it is now in play).
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Allows us to attempt to draw a card of a specific suit.
        /// </summary>
        /// <param name="suit">Specifies the Suit that we want to draw from the deck.</param>
        /// <returns>A reference to the Card that was drawn. If the specified suit is not
        /// available, this might be a card of a different suit.</returns>
        public Card SelectCardOfSpecificSuit(Suit suit)
        {
            // Attempt to retrieve the first card in the deck which is of the specified suit.
            Card selectedCard = cards.FirstOrDefault(card => card?.suit == suit);

            // If there is no card of the specified suit, just draw a card from the top of
            // the deck.
            if (selectedCard == null)
                return Draw();

            // A card of the specified suit was found in the deck, so remove that card from the
            // deck.
            cards.Remove(selectedCard);
            return selectedCard;
        }

        /// <summary>
        /// This method performs a deep copy of the Deck object, creating new instances of
        /// all its members.
        /// </summary>
        /// <returns>An object matching this Deck object.</returns>
        public object Clone()
        {
            // The cards field needs to be cloned to ensure we get new instances. This
            // clone will be passed to the private constructor of this Deck class to create
            // a new deck instance with a collection of new card instances in the same order
            // as this deck. Since the Clone() method returns a generic object, we need to
            // cast it as a Cards object to satisy the Deck constructor.
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }

        /// <summary>
        /// Adds all the cards contained in a deck of cards into the cards collection field.
        /// </summary>
        private void InsertAllCards()
        {
            // Add 52 cards to the deck, with one card for each suit and rank combination.
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    // The collection will be populated sequentially. The Card class expects
                    // parameters of type Suit and Rank, so the integers need to be cast to
                    // those data types.
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                }
            }
        }

        /// <summary>
        /// An overload to insert all cards in a deck of cards into the cards collection field,
        /// but omitting the cards in a specified list.
        /// </summary>
        /// <param name="except">A collection of cards that should not be added to the cards
        /// collection field.</param>
        private void InsertAllCards(List<Card> except)
        {
            // Add 52 cards to the deck, with one card for each suit and rank combination.
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    // Get an instance of the next card to be added to the collection.
                    var card = new Card((Suit)suitVal, (Rank)rankVal);

                    // If the card to be added is in the list of exceptions, then skip it.
                    if (except?.Contains(card) == true)
                        continue;

                    cards.Add(card);
                }
            }
        }
    }
}