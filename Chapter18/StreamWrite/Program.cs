using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace StreamWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create or open a file with a file stream, then create a stream writer for the file.
                FileStream aFile = new FileStream("Log.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                bool truth = true;

                // Write text to the file.
                sw.WriteLine("Hello to you.");
                sw.Write($"It is now {DateTime.Now.ToLongDateString()}");
                sw.WriteLine(" and things are looking good.");
                sw.Write("More than that,");
                sw.Write($" it's {truth} that C# is fun.");
                sw.Close();
            }
            catch(IOException ex)
            {
                WriteLine("An IO exception has been thrown!");
                WriteLine(ex.ToString());
                ReadKey();
                return;
            }
        }
    }
}
