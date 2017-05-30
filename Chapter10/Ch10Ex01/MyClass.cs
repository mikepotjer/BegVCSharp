using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10Ex01
{
    public class MyClass
    {
        // A readonly property can only be assigned here (in the declaration) or in the
        // constructor, which is where we do assign it.
        public readonly string Name;

        private int intVal;
        public int Val
        {
            get { return intVal; }
            set
            {
                if (value >= 0 && value <= 10)
                    intVal = value;
                else
                    throw (new ArgumentOutOfRangeException("Val", value,
                        "Val must be assigned a value between 0 and 10."));
            }
        }

        // Use the lambda syntax to override this method.
        public override string ToString() => "Name: " + Name + "\nVal: " + Val;

        // Don't allow the default constructor to be called directly, and redirect it
        // to the overload constructor when it is called (such as in a derived class).
        private MyClass() : this("Default Name") { }

        public MyClass(string newName)
        {
            Name = newName;
            intVal = 0;
        }

        private int myDoubledInt = 5;
        public int MyDoubledIntProp => (myDoubledInt * 2);
    }
}
