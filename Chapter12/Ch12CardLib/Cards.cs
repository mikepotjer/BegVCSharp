using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12CardLib
{
    // This Cards class duplicates the behavior of Ch11CardLib, but using a generic
    // collection class, eliminating a lot of custom code.
    // Allow this class to be cloned, so a copy of a Cards collection can be obtained,
    // rather than a duplicate reference to the same instance.
    public class Cards : List<Card>, ICloneable
    {
        /// <summary>
        /// Utility method for copying card instances into another Cards instance - used
        /// in Deck.Shuffle(). This implementation assumes that source and target collections
        /// are the same size.
        /// We still need a custom method here, because the default implementation will
        /// create an array of Card objects, not a Cards collection.
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
        /// This method performs a deep copy of the Cards collection, creating new instances
        /// of all its members.
        /// This method is required to implement the ICloneable interface, and is not supplied
        /// by the collection class.
        /// </summary>
        /// <returns>An object matching this Cards collection.</returns>
        public object Clone()
        {
            // Since this collection contains *references* to Card objects, we need to
            // create a new Cards collection, AND clone each Card object in it.
            Cards newCards = new Cards();
            foreach (Card sourceCard in this)
            {
                newCards.Add((Card)sourceCard.Clone());
            }
            return newCards;
        }
    }
}
