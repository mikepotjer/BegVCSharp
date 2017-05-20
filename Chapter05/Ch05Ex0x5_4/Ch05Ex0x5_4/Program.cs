using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch05Ex0x5_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string userString,
                backwardString = "";

            WriteLine("Please enter a string:");
            userString = ReadLine();

            foreach (char character in userString)
            {
                backwardString = character + backwardString;
            }
            WriteLine($"Your string in reverse is: {backwardString}");
            ReadKey();
        }
    }
}
