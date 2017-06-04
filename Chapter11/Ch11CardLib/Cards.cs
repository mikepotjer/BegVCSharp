using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch11CardLib
{
    // Allow this class to be cloned, so a copy of a Cards collection can be obtained,
    // rather than a duplicate reference to the same instance.
    public class Cards : CollectionBase, ICloneable
    {
        public void Add(Card newCard)
        {
            List.Add(newCard);
        }
        public void Remove(Card newCard)
        {
            List.Remove(newCard);
        }

        public Card this[int cardIndex]
        {
            get { return (Card)List[cardIndex]; }
            set { List[cardIndex] = value; }
        }

        /// <summary>
        /// Utility method for copying card instances into another Cards instance - used
        /// in Deck.Shuffle(). This implementation assumes that source and target collections
        /// are the same size.
        /// </summary>
        /// <param name="targetCards">Another collection based on the Cards class.</param>
        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

        /// <summary>
        /// Check to see if the Cards collection contains a particular card. This calls
        /// the Contains() method of the ArrayList for the code, which you access through
        /// the InnerList property.
        /// </summary>
        /// <param name="card">An instance of the Card class.</param>
        /// <returns>True if this collection contains an instance of the specified card,
        /// false otherwise.</returns>
        public bool Contains(Card card) => InnerList.Contains(card);

        /// <summary>
        /// This method performs a deep copy of the Cards collection, creating new instances
        /// of all its members.
        /// </summary>
        /// <returns>An object matching this Cards collection.</returns>
        public object Clone()
        {
            // Since this collection contains *references* to Card objects, we need to
            // create a new Cards collection, AND clone each Card object in it.
            Cards newCards = new Cards();
            foreach (Card sourceCard in List)
            {
                newCards.Add((Card)sourceCard.Clone());
            }
            return newCards;
        }
    }
}
