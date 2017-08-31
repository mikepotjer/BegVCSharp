using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13Ex99_1
{
    /// <summary>
    /// A custom class for passing information from a Connection event.
    /// </summary>
    public class MessageArrivedEventArgs : EventArgs
    {
        // Add a field and property to store and access the message retrieved at the time of
        // the event.
        private string message;
        public string Message
        {
            get { return message; }
        }

        // The default constructor sets a default message.
        public MessageArrivedEventArgs()
        {
            message = "No message sent.";
        }

        // A custom constructor allows us to set the message field.
        public MessageArrivedEventArgs(string newMessage)
        {
            message = newMessage;
        }
    }
}
