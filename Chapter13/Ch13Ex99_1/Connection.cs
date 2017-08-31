using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Ch13Ex99_1
{
    public class Connection
    {
        // Define the event to call when messages arrive in the connection object.
        // Here we use a generic delegate type, which is going to use our custom event arguments
        // class.
        public event EventHandler<MessageArrivedEventArgs> MessageArrived;

        public string Name { get; set; }

        // The timer is used internally to check for messages.
        private Timer pollTimer;

        // Create a default constructor to initialize the timer, and subscribe the CheckForMessage()
        // method to the Elapsed event.
        public Connection()
        {
            pollTimer = new Timer(100);
            
            // Also subscribe our generic handler, to show it can handle different events.
            GenericHandler myHandler = new GenericHandler();
            pollTimer.Elapsed += myHandler.ProcessEvent;

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
                // Calling the event method raises the event. The event handler requires an
                // object, and our custom event arguments class. The message will be stored in
                // the arguments object.
                MessageArrived(this, new MessageArrivedEventArgs("Hello Mami!"));
            }
        }
    }
}
