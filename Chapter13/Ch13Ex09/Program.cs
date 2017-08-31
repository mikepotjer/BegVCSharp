using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex09
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] curries = { "pathia", "jalfrezi", "korma" };

            // Concatenate all the items in the array, with a space between each item.
            WriteLine(curries.Aggregate((a, b) => a + " " + b));

            // Add the lengths of all the items in the array. Seed with 0, adding in each length.
            WriteLine(curries.Aggregate<string, int>(0, (a, b) => a + b.Length));

            // Concatenate all the items in the array, with a space between each item. A label is
            // used as the seed, and the complete string is returned as the result.
            WriteLine(curries.Aggregate<string, string, string>("Some curries:", (a, b) => a + " " + b, a => a));

            // Concatenate all the items in the array, with a space between each item. A label is
            // used as the seed, and the *length* of the complete string is returned as the result.
            WriteLine(curries.Aggregate<string, string, int>("Some curries:", (a, b) => a + " " + b, a => a.Length));

            ReadKey();
        }
    }
}
