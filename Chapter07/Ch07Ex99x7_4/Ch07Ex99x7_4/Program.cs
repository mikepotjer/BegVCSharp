using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch07Ex99x7_4
{
    class Program
    {
        enum Orientation : byte
        {
            North = 1,
            South,
            East,
            West
        }

        static void Main(string[] args)
        {
            Orientation myDirection;
            for (byte myByte = 0; myByte < 10; myByte++)
            {
                try
                {
                    myDirection = checked((Orientation)myByte);
                    if (myDirection < Orientation.North || myDirection > Orientation.West)
                    {
                        throw new ArgumentOutOfRangeException("myByte", myByte,
                            "Value must be between 1 and 4");
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    WriteLine($"Error: {e.Message}\nAssigning default value to {Orientation.North}.");
                    myDirection = Orientation.North;
                }
                WriteLine($"myDirection = {myDirection}");
            }
            ReadKey();
        }
    }
}
