using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch10Ex99x10_1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDerivedClass myObj = new MyDerivedClass();
            myObj.ContainedString = "This is my string.";
            WriteLine(myObj.GetString());
            ReadKey();
        }
    }

    // Exercise 10.1
    public class MyClass
    {
        protected string myString;

        public string ContainedString
        {
            set
            {
                myString = value;
            }
        }

        public virtual string GetString() => myString;
    }

    // Exercise 10.2
    public class MyDerivedClass : MyClass
    {
        public override string GetString()
        {
            return base.GetString()
                + " (output from derived class)";
        }
    }
}
