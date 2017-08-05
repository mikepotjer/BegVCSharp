using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex05
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an array of anonymous type objects.
            var curries = new[]
            {
                new { MainIngredient = "Lamb", Style = "Dhansak", Spiciness = 5 },
                new { MainIngredient = "Lamb", Style = "Dhansak", Spiciness = 5 },
                new { MainIngredient = "Chicken", Style = "Dhansak", Spiciness = 5 }
            };

            WriteLine(curries[0].ToString());

            // Hash code is based on the state, and the first 2 objects in the array have the
            // same state, so they have the same hash code.
            WriteLine(curries[0].GetHashCode());
            WriteLine(curries[1].GetHashCode());
            WriteLine(curries[2].GetHashCode());

            // In anonymous types, the implementation of Equals() compares state, so this will
            // return true when the first 2 objects are compared.
            WriteLine(curries[0].Equals(curries[1]));
            WriteLine(curries[0].Equals(curries[2]));

            // The == operator compares object references. Since every object in the array is
            // a different instance, none of them are equal using this operator.
            WriteLine(curries[0] == curries[1]);
            WriteLine(curries[0] == curries[2]);
            ReadKey();
        }
    }
}
