using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch11Ex05
{
    public class Person : IComparable
    {
        public string Name;
        public int Age;

        // Add a nondefault constructor to set the name and age.
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Implement the one method of the interface to perform the comparison.
        public int CompareTo(object obj)
        {
            if (obj is Person)
            {
                Person otherPerson = obj as Person;
                return this.Age - otherPerson.Age;
            }
            else
            {
                throw new ArgumentException("Object to compare to is not a person object.");
            }
        }
    }

    // Add a class that can be used to compare the names of two Person objects.
    public class PersonComparerName : IComparer
    {
        // Add a public static field to allow us to easily access an instance of this class, just
        // like we can with the generic System.Collections.Comparer class.
        public static IComparer Default = new PersonComparerName();

        public int Compare(object x, object y)
        {
            if (x is Person && y is Person)
            {
                // Compare the names using the logic of the Default comparer in System.Collections.
                return Comparer.Default.Compare(((Person)x).Name, ((Person)y).Name);
            }
            else
            {
                throw new ArgumentException("One or both objects to compare are not Person objects.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArrayList list = new ArrayList();
            list.Add(new Person("Raul", 30));
            list.Add(new Person("Donna", 25));
            list.Add(new Person("Mary", 27));
            list.Add(new Person("Ben", 44));

            WriteLine("Unsorted people:");
            for (int i = 0; i < list.Count; i++)
            {
                WriteLine($"{(list[i] as Person).Name} ({(list[i] as Person).Age})");
            }

            WriteLine("\nPeople sorted with default comparer (by age):");
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                WriteLine($"{(list[i] as Person).Name} ({(list[i] as Person).Age})");
            }

            WriteLine("\nPeople sorted with nondefault comparer (by name):");
            list.Sort(PersonComparerName.Default);
            for (int i = 0; i < list.Count; i++)
            {
                WriteLine($"{(list[i] as Person).Name} ({(list[i] as Person).Age})");
            }

            ReadKey();
        }
    }
}
