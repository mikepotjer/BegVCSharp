using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;

namespace Ch04Ex0x4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool integersOK = false;
            int myInt1 = 0,
                myInt2 = 0;

            while (!integersOK)
            {
                WriteLine("Enter the first integer:");
                myInt1 = ToInt32(ReadLine());

                WriteLine("Enter the second integer:");
                myInt2 = ToInt32(ReadLine());

                integersOK = (myInt1 <= 10) || (myInt2 <= 10);
                if (!integersOK)
                    WriteLine("Both integers may not be greater than 10.");
            }

            WriteLine($"You entered {myInt1} and {myInt2}.");
            bool isLessThan10 = myInt1 < 10;
            bool isBetween0And5 = (0 <= myInt1) && (myInt1 <= 5);
            WriteLine($"First integer less than 10? {isLessThan10}");
            WriteLine($"First integer between 0 and 5? {isBetween0And5}");
            WriteLine($"Exactly one of the above is true? {isLessThan10 ^ isBetween0And5}");

            isLessThan10 = myInt2 < 10;
            isBetween0And5 = (0 <= myInt2) && (myInt2 <= 5);
            WriteLine($"Second integer less than 10? {isLessThan10}");
            WriteLine($"Second integer between 0 and 5? {isBetween0And5}");
            WriteLine($"Exactly one of the above is true? {isLessThan10 ^ isBetween0And5}");
            ReadKey();
        }
    }
}
