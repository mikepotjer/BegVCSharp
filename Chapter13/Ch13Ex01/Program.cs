using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Ch13Ex01
{
    class Program
    {
        static int counter = 0;

        static string displayString = "This string will appear one letter at a time. ";

        static void Main(string[] args)
        {
            // Instantiate a timer that will raise an event every 100 milliseconds.
            Timer myTimer = new Timer(100);

            // Subscribe the WriteChar() method to the Elapsed event using the ElapsedEventHandler
            // delegate defined in .NET.
            myTimer.Elapsed += new ElapsedEventHandler(WriteChar);

            // The following will also work. The compiler will infer the delegate type from the
            // context.
            // myTimer.Elapsed += WriteChar;

            myTimer.Start();

            // Add a delay to allow the timer to start sending messages before we reach the ReadKey()
            // command.
            System.Threading.Thread.Sleep(200);

            ReadKey();
        }

        /// <summary>
        /// Define a method to serve as a delegate for the timer's Elapsed event. This method uses the same
        /// signature as the ElapsedEventHandler delegate.
        /// </summary>
        /// <param name="source">A reference to the timer object that initiated the event.</param>
        /// <param name="e">An instance of an ElapsedEventArgs object.</param>
        static void WriteChar(object source, ElapsedEventArgs e)
        {
            Write(displayString[counter++ % displayString.Length]);
        }
    }
}
