using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ch22Ex01Client.ServiceReference1;
using static System.Console;

namespace Ch22Ex01Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Title = "Ch22Ex01Client";
            string numericInput = null;
            int intParam;

            do
            {
                WriteLine("Enter an integer and press Enter to call the WCF service.");
                numericInput = ReadLine();
            }
            while (!int.TryParse(numericInput, out intParam));

            Service1Client client = new Service1Client();
            WriteLine(client.GetData(intParam));
            WriteLine("Press any key to exit.");

            ReadKey();
        }
    }
}
