using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex03
{
    public class Display
    {
        // Define a method with the signature required to be able to subscribe to the event
        // defined in the Connection class. Since the subscriber method is in a different
        // class, it has to be public.
        public void DisplayMessage(object source, MessageArrivedEventArgs e)
        {
            // The source object needs to be cast to access its custom properties, but since
            // we are using a specific arguments class, we can access the message directly.
            WriteLine($"Message arrived from: {((Connection)source).Name}");
            WriteLine($"Message text: {e.Message}");
        }
    }
}
