using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch07Ex99x7_2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10000; i++)
            {
                WriteLine($"Processing item {i}");
                if (i == 5000)
                    WriteLine(args[999]);
            }
            ReadKey();
        }
    }
}
