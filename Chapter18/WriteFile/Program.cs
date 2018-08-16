using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace WriteFile
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteData;
            char[] charData;

            try
            {
                // Create the file stream for the text file.
                FileStream aFile = new FileStream("Temp.txt", FileMode.Create);

                // Set the string to write to the file.
                charData = "My pink half of the drainpipe.".ToCharArray();

                // Initialize the byte array, and convert the character array to a byte array.
                byteData = new byte[charData.Length];
                Encoder e = Encoding.UTF8.GetEncoder();
                e.GetBytes(charData, 0, charData.Length, byteData, 0, true);

                // Move the file pointer to the beginnning of the file, and write the contents of the
                // byte array to the file.
                aFile.Seek(0, SeekOrigin.Begin);
                aFile.Write(byteData, 0, byteData.Length);
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
