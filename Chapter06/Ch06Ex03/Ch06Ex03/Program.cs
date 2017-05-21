using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch06Ex03
{
    class Program
    {
        // The params keyword indicates a parameter array.  This can be used to pass a variable
        // number of parameters to a function (0 or more), but all of the same type.  There can
        // be other parameters for the function, but params must always be last. and there can
        // be only one.
        static int SumVals(params int[] vals)
        {
            int sum = 0;
            foreach (int val in vals)
            {
                sum += val;
            }
            return sum;
        }

        static void Main(string[] args)
        {
            int sum = SumVals(1, 5, 2, 9, 8);
            WriteLine($"Summed Values = {sum}");
            ReadKey();
        }
    }
}
