using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch11Ex99
{
    class Program
    {
        static void Main(string[] args)
        {
            People myPeeps = new People();
            // This line checks error handling on the GetOldest() method.
            //Person[] oldestPeepsTest = myPeeps.GetOldest();
            myPeeps.Add(new Person("Russell", 44));
            myPeeps.Add(new Person("Michael", 51));
            myPeeps.Add(new Person("Joel", 47));
            myPeeps.Add(new Person("Zachary", 17));
            myPeeps.Add(new Person("Heidi", 20));
            myPeeps.Add(new Person("Teresa", 46));
            myPeeps.Add(new Person("Bryan", 51));

            WriteLine($"Number of people: {myPeeps.Count}");
            foreach (Person person in myPeeps)
            {
                WriteLine(person);
            }

            WriteLine();
            WriteLine($"{myPeeps["Joel"]} > {myPeeps["Teresa"]}: {myPeeps["Joel"] > myPeeps["Teresa"]}");
            WriteLine($"{myPeeps["Heidi"]} < {myPeeps["Zachary"]}: {myPeeps["Heidi"] < myPeeps["Zachary"]}");
            WriteLine($"{myPeeps["Michael"]} >= {myPeeps["Bryan"]}: {myPeeps["Michael"] >= myPeeps["Bryan"]}");
            WriteLine($"{myPeeps["Russell"]} <= {myPeeps["Teresa"]}: {myPeeps["Russell"] <= myPeeps["Teresa"]}");

            WriteLine("\nThe oldest people in the list:");
            Person[] oldestPeeps = myPeeps.GetOldest();
            foreach (Person oldPerson in oldestPeeps)
            {
                WriteLine(oldPerson);
            }

            WriteLine("\nCloning friends and family!");
            People myClones = (People)myPeeps.Clone();
            myPeeps["Russell"].Age = 45;
            WriteLine($"Number of cloned People created: {myClones.Count}");
            WriteLine($"Original: {myPeeps["Russell"]}");
            WriteLine($"Clone: {myClones["Russell"]}");

            WriteLine("\nHere are the ages in the original list:");
            foreach (int age in myPeeps.Ages)
            {
                Write($"{age} ");
            }
            ReadKey();
        }
    }
}
