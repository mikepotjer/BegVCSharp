using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;

namespace Ch06Ex05
{
    class Program
    {
        // This defines a delegate for any other function that receives 2 doubles,
        // and returns a double.
        delegate double ProcessDelegate(double param1, double param2);

        // Define 2 functions with the same signature and return value as the delegate.
        // Note the lambda arrow to define the function inline.
        static double Multiply(double param1, double param2) => param1 * param2;
        static double Divide(double param1, double param2) => param1 / param2;

        static void Main(string[] args)
        {
            // Define a delegate variable, which can have a function assigned to it.
            ProcessDelegate process;

            WriteLine("Enter 2 numbers separated with a comma:");
            string input = ReadLine();

            int commaPos = input.IndexOf(',');
            double paramVal1 = ToDouble(input.Substring(0, commaPos));
            double paramVal2 = ToDouble(input.Substring(commaPos + 1, input.Length - commaPos - 1));

            WriteLine("Enter M to multiply or D to divide:");
            input = ReadLine();

            // Based on the user input, assign the appropriate function to the
            // delegate variable.
            if (input.ToUpper() == "M")
                process = new ProcessDelegate(Multiply);
            else
                process = new ProcessDelegate(Divide);

            // The following will also work. The compiler recognizes that the assigned
            // function matches the signature for the delegate, and automatically
            // initializes the delegate for you.
            //if (input.ToUpper() == "M")
            //    process = Multiply;
            //else
            //    process = Divide;

            // Pass the values input by the user to the function stored in the delegate
            // variable named "process", and display the return value.
            // Note that a delegate variable can be passed as a parameter to other
            // functions, which can then use it to perform additional operations.
            WriteLine($"Result: {process(paramVal1, paramVal2)}");
            ReadKey();
        }
    }
}
