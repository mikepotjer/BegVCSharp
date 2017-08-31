using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch13Ex99_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection myConnection1 = new Connection();
            myConnection1.Name = "First connection.";
            Connection myConnection2 = new Connection();
            myConnection2.Name = "Second connection.";
            GenericHandler myHandler = new GenericHandler();

            // Setup the GenericHandler object ProcessEvent() method as a subscriber to the MessageArrived
            // event of the Connection objects.
            myConnection1.MessageArrived += myHandler.ProcessEvent;
            myConnection2.MessageArrived += myHandler.ProcessEvent;

            // Calling the Connect() method will start the object polling for messages.
            myConnection1.Connect();
            myConnection2.Connect();
            ReadKey();

            // Stop polling for messages so we can see the output generated.
            myConnection1.Disconnect();
            myConnection2.Disconnect();
            ReadKey();
        }
    }
}
