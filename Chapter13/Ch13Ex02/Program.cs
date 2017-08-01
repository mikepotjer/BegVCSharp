using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection myConnection = new Connection();
            Display myDisplay = new Display();

            // Setup the Display object DisplayMessage() method as a subscriber to the MessageArrived
            //event of the Connection object.
            myConnection.MessageArrived += new MessageHandler(myDisplay.DisplayMessage);

            // Calling the Connect() method will start the object polling for messages.
            myConnection.Connect();
            ReadKey();
        }
    }
}
