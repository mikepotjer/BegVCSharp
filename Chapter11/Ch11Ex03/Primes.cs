using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch11Ex03
{
    public class Primes
    {
        // Add fields to store the minimum and maximum values for the range of prime numbers.
        private long min;
        private long max;

        // If the default constructor is called, redirect it to the custom constructor,
        // setting a default minimum and maximum value.
        public Primes() : this(2, 100) { }
        public Primes(long minimum, long maximum)
        {
            // Restrict the minimum to the lowest prime number.
            if (minimum < 2)
                min = 2;
            else
                min = minimum;

            max = maximum;
        }

        /// <summary>
        /// This method satisfies the rules that allow this class to be used in an iterator block.
        /// </summary>
        /// <returns>The iEnumerator type required for an iterator block.</returns>
        public IEnumerator GetEnumerator()
        {
            // To check for primes, test all numbers in the range.
            for (long possiblePrime = min; possiblePrime <= max; possiblePrime++)
            {
                // Assume the number is prime until we prove otherwise.
                bool isPrime = true;

                // Test dividing by all integers from 2 to the square root of the current number.
                // The square root is the largest possible denominator that won't have a remainder.
                // Obviously not all square roots are whole numbers, so we need to round down the
                // square root to the nearest whole number, if necessary.
                for (long possibleFactor = 2; possibleFactor <= (long)Math.Floor(Math.Sqrt(possiblePrime)); possibleFactor++)
                {
                    // Divide the number by the current factor. If there's no remainder, this is
                    // is not a prime number.
                    long remainderAfterDivision = possiblePrime % possibleFactor;
                    if (remainderAfterDivision == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                // If the current number is prime, return it to the foreach loop. The yield keyword
                // is required to select the values to be used in the foreach loop.
                if (isPrime)
                {
                    yield return possiblePrime;
                }
            }
        }
    }
}
