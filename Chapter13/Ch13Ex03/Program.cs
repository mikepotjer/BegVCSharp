using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex03
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection myConnection1 = new Connection();
            myConnection1.Name = "First connection.";
            Connection myConnection2 = new Connection();
            myConnection2.Name = "Second connection.";
            Display myDisplay = new Display();

            // Setup the Display object DisplayMessage() method as a subscriber to the MessageArrived
            // event of the Connection objects.
            myConnection1.MessageArrived += myDisplay.DisplayMessage;
            myConnection2.MessageArrived += myDisplay.DisplayMessage;

            // Calling the Connect() method will start the object polling for messages.
            myConnection1.Connect();
            myConnection2.Connect();
            ReadKey();
        }
    }
}
