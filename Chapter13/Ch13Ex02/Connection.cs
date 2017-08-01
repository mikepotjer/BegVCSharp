using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Ch13Ex02
{
    // Before defining an event, the delegate type for the event must be defined. This defines the
    // return value and parameters that an event handling method must have. Make it public so it's
    // available to external code.
    /// <summary>
    /// Message handler delegate type for a Connection object event.
    /// </summary>
    /// <param name="messageText">The message text to pass to the event handler</param>
    public delegate void MessageHandler(string messageText);

    public class Connection
    {
        // Define the event to call when messages arrive in the connection object.
        public event MessageHandler MessageArrived;

        // The timer is used internally to check for messages.
        private Timer pollTimer;

        // Create a default constructor to initialize the timer, and subscribe the CheckForMessage()
        // method to the Elapsed event.
        public Connection()
        {
            pollTimer = new Timer(100);
            pollTimer.Elapsed += new ElapsedEventHandler(CheckForMessage);
        }

        // Add methods to provide access to the state of the timer. Connect() turns the timer on,
        // Disconnect() turns it back off.
        public void Connect() => pollTimer.Start();
        public void Disconnect() => pollTimer.Stop();

        // For demo purposes, create a Random object which will be used to generate messages on
        // random intervals.
        private static Random random = new Random();

        private void CheckForMessage(object source, ElapsedEventArgs e)
        {
            WriteLine("Checking for new messages.");

            // Generate a number between 0 and 9, raising an event whenever the number is 0.
            // However, an event will be raised only if it has subscribers.
            if ((random.Next(9) == 0) && MessageArrived != null)
            {
                // Calling the event method raises the event.
                MessageArrived("Hello Mami!");
            }
        }
    }
}
