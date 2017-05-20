using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch05Ex0x5_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string myString = "This is another test";
            char[] separator = { ' ' };
            string[] myWords;
            string newString = "";

            myWords = myString.Split(separator);
            foreach (string word in myWords)
            {
                newString += "\"" + word + "\" ";
            }
            newString = newString.TrimEnd();
            WriteLine(newString);
            ReadKey();
        }
    }
}
