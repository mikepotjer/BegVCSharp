using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch10Ex02
{
    public class ClassA
    {
        private int state = -1;
        public int State
        {
            get { return state; }
        }

        // This is a nested class. It has access to all protected and private members
        // of the outer class.
        public class ClassB
        {
            public void SetPrivateState(ClassA target, int newState)
            {
                // The nested class has access to "state", the backing field for the
                // ClassA.State property.
                target.state = newState;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ClassA myObject = new ClassA();
            WriteLine($"myObject.State = {myObject.State}");

            // Create an instance of the nested class, and use it to set the read-only State
            // property of the outer class. Note the hierarchical syntax required to define
            // and instantiate the nested class.
            ClassA.ClassB myOtherObject = new ClassA.ClassB();
            myOtherObject.SetPrivateState(myObject, 999);
            WriteLine($"myObject.State = {myObject.State}");
            ReadKey();
        }
    }
}
