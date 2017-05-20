using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch05Ex02
{
    enum orientation : byte
    {
        north = 1,
        south = 2,
        east = 3,
        west = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
            byte directionByte;
            string directionString;
            orientation myDirection = orientation.north;
            WriteLine($"myDirection = {myDirection}");

            directionByte = (byte)myDirection;
            // These 2 assignments to string produce the same result.
            //directionString = Convert.ToString(myDirection);
            directionString = myDirection.ToString();
            WriteLine($"byte equivalent = {directionByte}");
            WriteLine($"string equivalent = {directionString}");
            ReadKey();
        }
    }
}
