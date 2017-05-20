using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch05Ex0x5_5
{
    class Program
    {
        static void Main(string[] args)
        {
            string userString;

            WriteLine("Enter a string (include the word \"no\" in it)");
            userString = ReadLine();

            WriteLine(userString.Replace("no", "yes"));
            ReadKey();
        }
    }
}
