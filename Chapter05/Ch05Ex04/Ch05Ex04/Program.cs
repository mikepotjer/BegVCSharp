using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch05Ex04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] friendNames = { "Todd Anthony", "Kevin Holton", "Shane Laigle" };
            //int i;

            WriteLine($"Here are {friendNames.Length} of my friends:");
            //for (i = 0; i < friendNames.Length; i++)
            //{
            //    WriteLine(friendNames[i]);
            //}
            // This does the same as the above, without the danger of accessing
            // illegal elements.  The friendName variable is read-only.
            foreach (string friendName in friendNames)
            {
                WriteLine(friendName);
            }
            ReadKey();
        }
    }
}
