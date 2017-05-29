using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch09Ex01
{
    // Public classes are available from any project. An abstract class can only
    // be derived (subclassed), but never instantiated.
    public abstract class MyBase { }

    // Internal classes are only available within a project. Internal is the
    // default, and can be omitted.
    internal class MyClass : MyBase { }

    // Public and internal have the same implications for interfaces. Here, too,
    // internal is the default scope, and can be omitted.
    public interface IMyBaseInterface { }
    internal interface IMyBaseInterface2 { }

    // An interface can include other interfaces in it.
    internal interface IMyInterface : IMyBaseInterface, IMyBaseInterface2 { }

    // A sealed class can never be derived (subclassed), but can only be instantiated.
    // Any class can implement one or more interfaces, but they must always be listed
    // after the base class.
    internal sealed class MyComplexClass : MyClass, IMyInterface { }

    /// <summary>
    /// This class contains my program!
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MyComplexClass myObj = new MyComplexClass();
            WriteLine(myObj.ToString());
            ReadKey();
        }
    }
}
