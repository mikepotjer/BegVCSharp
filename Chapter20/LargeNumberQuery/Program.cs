using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LargeNumberQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arraySizes = { 100, 1000, 10000, 100000, 1000000, 5000000, 10000000, 50000000 };

            foreach (int i in arraySizes)
            {
                WriteLine($"\nNumber array size = {i}");

                int[] numbers = GenerateLotsOfNumbers(i);

                var queryResults =
                    from n in numbers
                    where n < 1000
                    select n;

                // Note that the data isn't actually retrieved until the results are accessed.
                // This is called "deferred query execution" or "lazy evaluation".
                WriteLine("Numbers less than 1000:");
                foreach (var item in queryResults)
                {
                    WriteLine(item);
                }

                WriteLine("\nNumeric Aggregates");

                // orderby makes this drastically slower at the highest result sizes.
                var queryResults2 =
                    from n in numbers
                    where n > 1000
                    select n;

                WriteLine($"Count of Numbers > 1000: {queryResults2.Count()}");
                WriteLine($"Max of Numbers > 1000: {queryResults2.Max()}");
                WriteLine($"Min of Numbers > 1000: {queryResults2.Min()}");
                WriteLine($"Average of Numbers > 1000: {queryResults2.Average()}");
                // The no-parameter overload of Sum() returns an integer, so we need to use a lambda expression
                // to prevent an overload error.
                WriteLine($"Sum of Numbers > 1000: {queryResults2.Sum(n => (long)n)}");
            }

            Write("Program finished, press Enter/Return to continue:");
            ReadLine();
        }

        private static int[] GenerateLotsOfNumbers(int count)
        {
            Random generator = new Random(0);
            int[] result = new int[count];

            for (int i = 0; i < count; i++)
            {
                // There are different Next..() method to return other types, and Next() accepts
                // parameter to limit the range.
                result[i] = generator.Next();
            }

            return result;
        }
    }
}
