using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch11Ex99
{
    public class People : DictionaryBase, ICloneable
    {
        public void Add(Person newPerson)
        {
            // Use the person name as the ID for the Person object.
            Dictionary.Add(newPerson.Name, newPerson);
        }

        public void Remove(string personName)
        {
            Dictionary.Remove(personName);
        }

        // Define an indexer than can be accessed by the key (person name).
        public Person this[string personName]
        {
            get { return (Person)Dictionary[personName]; }

            // In a real implementation, we might be much more protective of what we allow
            // to be stored via the indexer -- if we allow set at all.
            set { Dictionary[personName] = value; }
        }

        // Define an iterator to make it easier iterate through the Persons in this collection.
        // To iterate over a class, you need a method called GetEnumerator() with a return
        // value of IEnumerator.
        public new IEnumerator GetEnumerator()
        {
            foreach (object person in Dictionary.Values)
                yield return (Person)person;
        }

        // To iterate over a class member, such as method or property, use IEnumerable.
        // For method, the 'yield return' commands go in the body of the method.  For
        // a property, the return values come from the getter.
        // In this case, we want to iterate through a particular value in each member
        // object, so we're using a property. When implemented this way, it resembles a
        // virtual collection, since it retrieves values on demand from the Dictionary
        // collection.
        public IEnumerable Ages
        {
            get
            {
                foreach (object person in Dictionary.Values)
                    yield return (person as Person).Age;
            }
        }

        // We are implementing a deep copy for this class.
        public object Clone()
        {
            People newPeople = new People();
            Person currentPerson;

            foreach (object person in Dictionary.Values)
            {
                // To make this a little more readable, we first retrieve the object from the
                // collection, and cast it back to a Person object. We can then call the
                // Clone() method of the Person object, but that returns a base object, so we
                // need to do another cast to get a Person object again, which we can then
                // add to the new collection.
                currentPerson = (person as Person);
                newPeople.Add((Person)currentPerson.Clone());
            }

            return newPeople;
        }

        public Person[] GetOldest()
        {
            // Since an array size is static, we'll use an ArrayList to collect the oldest
            // members, then copy the results to an array afterward.
            ArrayList oldest = new ArrayList();

            // These will be used in the loop to make the code more readable.
            Person currentPerson;
            Person priorOldestPerson;

            // Throw an error if this method is called without any members being added.
            if (Dictionary.Count == 0)
            {
                throw new InvalidOperationException("Collection does not contain any members, unable to determine oldest Person.");
            }

            // The items in a Dictionary collection are a special struct object. We need to iterate
            // through the Values to get our Person objects, which are boxed and need to be cast as a
            // Person again.
            foreach (object person in Dictionary.Values)
            {
                // Get the Person reference from the current item.
                currentPerson = (person as Person);

                if (oldest.Count == 0)
                {
                    // The list hasn't been populated yet, so the current person is automatically the oldest.
                    oldest.Add(currentPerson);
                }
                else
                {
                    // Since every Person in the list is the same age, use the first one as our reference.
                    priorOldestPerson = (oldest[0] as Person);

                    // We only need to update the list if the current Person is the same age or older than
                    // the Person from the list.
                    if (currentPerson.Age >= priorOldestPerson.Age)
                    {
                        // When the current Person is older than the Person from the list, then everyone
                        // that was in the list needs to be removed before adding the current Person.
                        if (currentPerson.Age > priorOldestPerson.Age)
                            oldest.Clear();

                        oldest.Add(currentPerson);
                    }
                }
            }

            // Create a Person array, and copy the list into it. As long as they are compatible, the
            // items from the list will automatically be cast.
            Person[] oldestPeople = new Person[oldest.Count];
            oldest.CopyTo(oldestPeople);

            return oldestPeople;
        }
    }
}
