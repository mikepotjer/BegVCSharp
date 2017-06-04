using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch11Ex02
{
    public class Animals : CollectionBase
    {
        // CollectionBase provides the protect property List to allow access to the items
        // through the IList interface. This allows us to create strongly-typed Add() and
        // Remove() methods for our collection.
        public void Add(Animal newAnimal)
        {
            List.Add(newAnimal);
        }
        public void Remove(Animal newAnimal)
        {
            List.Remove(newAnimal);
        }

        // A special type of property called an indexer allows us to define array-like
        // access to the collection. This can be used to make index access use strongly-
        // typed values.
        public Animal this[int animalIndex]
        {
            get { return (Animal)List[animalIndex]; }
            set { List[animalIndex] = value; }
        }
    }
}
