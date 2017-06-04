using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ch11CardLib;

namespace Ch11CardClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Get a new deck and shuffle it.
            //Deck myDeck = new Deck();
            //myDeck.Shuffle();

            //// Read all the cards in the deck.
            //for (int i = 0; i < 52; i++)
            //{
            //    Card tempCard = myDeck.GetCard(i);
            //    Write(tempCard.ToString());

            //    // After all but the last card, add a comma to separate it from the next card.
            //    if (i != 51)
            //        Write(", ");
            //    else
            //        WriteLine();
            //}

            Deck deck1 = new Deck();
            Deck deck2 = (Deck)deck1.Clone();

            // First show that both decks start out the same.
            WriteLine($"The first card in the original deck is: {deck1.GetCard(0)}");
            WriteLine($"The first card in the cloned deck is: {deck2.GetCard(0)}");

            // Shuffle the first deck, then show that changes to it do not affect the cloned deck.
            deck1.Shuffle();
            WriteLine("Original deck shuffled.");
            WriteLine($"The first card in the original deck is: {deck1.GetCard(0)}");
            WriteLine($"The first card in the cloned deck is: {deck2.GetCard(0)}");

            ReadKey();
        }
    }
}
