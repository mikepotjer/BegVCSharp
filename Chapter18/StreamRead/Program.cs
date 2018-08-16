using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace StreamRead
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;

            try
            {
                // Create a file stream to an existing file, and open a stream reader for the file.
                FileStream aFile = new FileStream("Log.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                // Read the contents of the file line by line.
                line = sr.ReadLine();
                while (line != null)
                {
                    WriteLine(line);
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (IOException ex)
            {
                WriteLine("An IO exception has been thrown!");
                WriteLine(ex.ToString());
                return;
            }

            ReadKey();
        }
    }
}
