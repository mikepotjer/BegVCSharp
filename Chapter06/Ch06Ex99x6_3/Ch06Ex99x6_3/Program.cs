using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch06Ex99x6_3
{
    delegate string ReadDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            ReadDelegate readMe = new ReadDelegate(ReadLine);

            WriteLine("Enter some text:");
            string input = readMe();

            WriteLine($"You wrote: {input}");
            ReadKey();
        }
    }
}
