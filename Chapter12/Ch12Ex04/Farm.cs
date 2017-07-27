using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12Ex04
{
    public class Farm<T> : IEnumerable<T>
        where T : Animal
    {
        // Expose a generic List class property to store objects of type T, where T is
        // constrained as an Animal class.
        private List<T> animals = new List<T>();
        public List<T> Animals
        {
            get { return animals; }
        }

        // The IEnumerable interface is being implemented in this class so that we can
        // iterate through the items in this class without needing to explicitly iterate
        // over Farm<T>.Animals. This requires implementing a GetEnumerator() method here.
        // We must also implement the IEnumerable.GetEnumerator() method, because
        // IEnumerator<T> inherits from IEnumerable.
        public IEnumerator<T> GetEnumerator() => animals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => animals.GetEnumerator();

        public void MakeNoises()
        {
            // Because T is constrained to the Animal type, we have access to the methods
            // defined in that class.
            foreach (T animal in animals)
            {
                animal.MakeANoise();
            }
        }

        public void FeedTheAnimals()
        {
            foreach (T animal in animals)
            {
                animal.Feed();
            }
        }

        /// <summary>
        /// This method extracts all items that are of type Cow, or inherit from it.
        /// </summary>
        /// <returns>A new Farm object containing all Cow objects in this Farm object.</returns>
        public Farm<Cow> GetCows()
        {
            Farm<Cow> cowFarm = new Farm<Cow>();

            foreach (T animal in animals)
            {
                if (animal is Cow)
                {
                    cowFarm.Animals.Add(animal as Cow);
                }
            }

            return cowFarm;
        }
    }
}
