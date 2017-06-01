using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch10Ex99x10_4
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCopyableClass myObj = new MyCopyableClass();
            myObj.Val = 10;

            MyCopyableClass myCopy = myObj.GetCopy();
            myCopy.Val = 20;

            WriteLine($"myObj.Val = {myObj.Val}");
            WriteLine($"myCopy.Val = {myCopy.Val}");
            ReadKey();
        }
    }

    public class MyCopyableClass
    {
        public int Val { get; set; }

        public MyCopyableClass GetCopy()
        {
            return (MyCopyableClass)MemberwiseClone();
        }
    }
}
