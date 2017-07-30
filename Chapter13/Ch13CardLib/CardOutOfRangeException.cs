using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13CardLib
{
    public class CardOutOfRangeException : Exception
    {
        // Provide a property to store the collection of cards at the time of the exception.
        private Cards deckContents;
        public Cards DeckContents
        {
            get { return deckContents; }
        }

        // Add a constructor to receive the cards collection, and set a default error message.
        public CardOutOfRangeException(Cards sourceDeckContents)
            : base("There are only 52 cards in the deck.")
        {
            deckContents = sourceDeckContents;
        }
    }
}
