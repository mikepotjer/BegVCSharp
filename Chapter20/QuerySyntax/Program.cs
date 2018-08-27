using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace QuerySyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names ={ "Alonso", "Zheng", "Smith", "Jones", "Smythe", "Small", "Ruiz", "Hsieh",
                "Jorgenson", "Ilyich", "Singh", "Samba", "Fatimah" };

            // We could explicitly declare the data type of the result set, but it's simpler
            // and more flexible to let C# infer it.
            var queryResults =
                from n in names
                where n.StartsWith("S")
                orderby n descending
                select n;

            // Note that the data isn't actually retrieved until the results are accessed.
            // This is called "deferred query execution" or "lazy evaluation".
            WriteLine("Name beginning with S:");
            foreach(var item in queryResults)
            {
                WriteLine(item);
            }

            WriteLine($"Count of names: {queryResults.Count()}");
            // Max and Min are the only simple aggregates supported for strings.
            WriteLine($"Max of names: {queryResults.Max()}");
            WriteLine($"Min of names: {queryResults.Min()}");

            Write("Program finished, press Enter/Return to continue:");
            ReadLine();
        }
    }
}
