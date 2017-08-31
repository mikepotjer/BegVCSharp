using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Ch13Ex99_1
{
    class GenericHandler
    {
        /// <summary>
        /// A generic event handler method that can subscribe to any event.
        /// </summary>
        /// <param name="source">The object generating the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        public void ProcessEvent(object source, EventArgs e)
        {
            // Check the event type.
            if (e is MessageArrivedEventArgs)
            {
                WriteLine("Connection.MessageArrived event received.");
                WriteLine($"Message: {(e as MessageArrivedEventArgs).Message}");
            }
            if (e is ElapsedEventArgs)
            {
                WriteLine("Timer.Elapsed event received.");
                WriteLine($"SignalTime: {(e as ElapsedEventArgs).SignalTime}");
            }
        }
    }
}
