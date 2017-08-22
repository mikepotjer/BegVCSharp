using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex08
{
    // Define a delegate type to represent the type of lambda expression methods we're going
    // to create.
    delegate int TwoIntegerOperationDelegate(int paramA, int paramB);

    class Program
    {
        // Create a method that accepts our delegate type, so we can pass our lambda expressions
        // to it.
        static void PerformOperations(TwoIntegerOperationDelegate del)
        {
            for (int paramAVal = 1; paramAVal <= 5; paramAVal++)
            {
                for (int paramBVal = 1; paramBVal <= 5; paramBVal++)
                {
                    int delegateCallResult = del(paramAVal, paramBVal);
                    Write($"f({paramAVal}, {paramBVal}) = {delegateCallResult}");
                    if (paramBVal != 5)
                    {
                        Write(", ");
                    }
                }
                WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // Define lambda expressions we can use to call our method.
            // 1. The parameter definition section uses untyped parameters, since the compiler
            //    can infer the types.
            // 2. The => operator separates the parameters from the body.
            // 3. The body specifies the operation. The compiler knows there must be a return
            //    value, and the result of the expression is the same type as the return value,
            //    so the compiler infers the result as the return type.
            WriteLine("f(a, b) = a + b:");
            PerformOperations((paramA, paramB) => paramA + paramB);
            WriteLine();

            WriteLine("f(a, b) = a * b:");
            PerformOperations((paramA, paramB) => paramA * paramB);
            WriteLine();

            WriteLine("f(a, b) = (a - b) % b:");
            PerformOperations((paramA, paramB) => (paramA - paramB) % paramB);

            ReadKey();
        }
    }
}
