using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MethodSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names ={ "Alonso", "Zheng", "Smith", "Jones", "Smythe", "Small", "Ruiz", "Hsieh",
                "Jorgenson", "Ilyich", "Singh", "Samba", "Fatimah" };

            // We could explicitly declare the data type of the result set, but it's simpler
            // and more flexible to let C# infer it. The method syntax in this case is more
            // concise that the query syntax equivalent.
            // Addition exercise 20.5: eliminating the Where clause would simply return all
            // the names, because there would be no query.
            var queryResults = names.Where(n => n.StartsWith("S"));

            // Note that the data isn't actually retrieved until the results are accessed.
            // This is called "deferred query execution" or "lazy evaluation".
            WriteLine("Name beginning with S:");
            foreach (var item in queryResults)
            {
                WriteLine(item);
            }

            Write("Program finished, press Enter/Return to continue:");
            ReadLine();
        }
    }
}
