using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex02
{
    public class Display
    {
        // Define a method with the signature required to be able to subscribe to the event
        // defined in the Connection class. Since the subscriber method is in a different
        // class, it has to be public.
        public void DisplayMessage(string message) => WriteLine($"Message arrived: {message}");
    }
}
