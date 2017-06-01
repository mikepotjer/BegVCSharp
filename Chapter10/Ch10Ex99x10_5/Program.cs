using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ch10CardLib;

namespace Ch10Ex99x10_5
{
    /// <summary>
    /// This program will draw 5-card hands from a deck of cards until either a flush is found,
    /// or the user decides to quit shuffling the deck.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Declare variables that we want in scope for this entire method.
            Deck myDeck = new Deck();
            string userInput = "";
            bool isFlush = false;
            int firstCard = 0;

            // Track how many decks we use.
            int deckNumber = 0;

            // Set the hand to a number that will trigger a shuffle on the first iteration of
            // the outer loop.
            int hand = 99;

            // Keep shuffling until the user enters "Q" or "q" to quit.
            do
            {
                // Keep making 5-card draws until the current deck is exhausted. Once a deck is
                // is exhausted, reset the hand counter and shuffle the deck.
                if (hand >= 10)
                {
                    hand = 1;
                    deckNumber++;
                    WriteLine($"Drawing from deck number {deckNumber}...");
                    myDeck.Shuffle();
                }
                else
                    hand++;


                // Store the index to the first card in the current hand. This can be used
                // later to display the cards in this hand.
                firstCard = (hand - 1) * 5;

                // Initialize the flag for a flush to true, since we will assume this hand
                // is a flush until we turn over the first card that proves otherwise.
                isFlush = true;

                // Store the suit of the first card in the hand. This is the suit we need to
                // match to get a flush.
                Suit currentSuit = myDeck.GetCard(firstCard).suit;
                //Write(myDeck.GetCard(firstCard).ToString());

                // Check the rest of the cards in the hand.
                for (int i = 1; i < 5; i++)
                {
                    //Write(", " + myDeck.GetCard(firstCard + i).ToString());

                    // As soon as we find a card that doesn't match the suit, we need to clear
                    // the flag, and we can stop checking this hand.
                    if (myDeck.GetCard(firstCard + i).suit != currentSuit)
                    {
                        isFlush = false;
                        break;
                    }
                }

                Write($"Hand {hand}: ");
                if (isFlush)
                {
                    // We got a flush! Display the hand, and wait for the user to press a key.
                    // Then exit the outer loop, ending the program.
                    WriteLine("Flush!");
                    for (int i = 0; i < 5; i++)
                    {
                        WriteLine(myDeck.GetCard(firstCard + i).ToString());
                    }
                    ReadKey();
                    break;
                }
                else
                {
                    // No flush for this hand. On the last hand of the deck, prompt the user to
                    // allow the opportunity to quit.
                    WriteLine("No flush");
                    if (hand == 10)
                        userInput = ReadLine();
                }

                // Check if the user wants to [Q]uit before the next pass.
            } while (userInput.ToLower() != "q");
        }
    }
}
