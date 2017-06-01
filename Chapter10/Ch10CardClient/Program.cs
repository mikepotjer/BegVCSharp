using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Ch10CardLib;

namespace Ch10CardClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get a new deck and shuffle it.
            Deck myDeck = new Deck();
            myDeck.Shuffle();

            // Read all the cards in the deck.
            for (int i = 0; i < 52; i++)
            {
                Card tempCard = myDeck.GetCard(i);
                Write(tempCard.ToString());

                // After all but the last card, add a comma to separate it from the next card.
                if (i != 51)
                    Write(", ");
                else
                    WriteLine();
            }
            ReadKey();
        }
    }
}
