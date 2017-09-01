using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13CardLib
{
    // Define a custom comparer class that can compare Card objects by suit, then rank.
    public class CardComparerSuit : IComparer<Card>
    {
        // Add a field to make it easy to get an instance of this class via CardComparerSuit.Default.
        public static IComparer<Card> Default = new CardComparerSuit();

        public int Compare(Card card1, Card card2)
        {
            // If the cards are the same suit, use the default comparison behavior for the Card class,
            // otherwise sort by suit. We don't really care what order the suits are in, as long as all
            // cards of the same suit are grouped together.
            if (card1.suit == card2.suit)
                return card1.CompareTo(card2);
            else
                return card1.suit - card2.suit;
        }
    }
}
