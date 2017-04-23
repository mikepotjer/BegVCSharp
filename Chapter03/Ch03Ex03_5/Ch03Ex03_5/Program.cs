using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;

namespace Ch03Ex03_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstInteger,
                secondInteger,
                thirdInteger,
                fourthInteger;

            WriteLine("Enter the first integer:");
            firstInteger = ToInt32(ReadLine());
            WriteLine("Enter the second integer:");
            secondInteger = ToInt32(ReadLine());
            WriteLine("Enter the third integer:");
            thirdInteger = ToInt32(ReadLine());
            WriteLine("Enter the fourth integer:");
            fourthInteger = ToInt32(ReadLine());

            WriteLine($"The product of {firstInteger} and {secondInteger} "
                + $"and {thirdInteger} and {fourthInteger} is "
                + $"{firstInteger * secondInteger * thirdInteger * fourthInteger}.");
            ReadKey();
        }
    }
}
