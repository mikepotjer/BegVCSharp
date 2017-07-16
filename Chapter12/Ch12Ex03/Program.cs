using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch12Ex03
{
    class Program
    {
        static void Main(string[] args)
        {
            Vectors route = new Vectors();

            route.Add(new Vector(2.0, 90.0));
            route.Add(new Vector(1.0, 180.0));
            route.Add(new Vector(0.5, 45.0));
            route.Add(new Vector(2.5, 315.0));

            // Write out the items in their initial order.
            WriteLine(route.Sum());

            // Use a delegate to sort the vectors. Since it is of type Comparison<Vector>, the
            // delegate can be assigned a method that returns an int, and accepts 2 Vector objects
            // as parameters.
            Comparison<Vector> sorter = new Comparison<Vector>(VectorDelegates.Compare);
            route.Sort(sorter);
            // The 2 lines above can also be simplified to:
            //  route.Sort(VectorDelegates.Compare)
            // The compiler realizes it needs an instance of type Comparison<Vector>, and implicitly
            // creates the delegate for you from the specified method. The reference to VectorDelegates.Compare
            // is called a "method group".
            WriteLine(route.Sum());

            // Define a search delegate that can be used to return a subset of the vectors in
            // the original route collection. Use the nondefault constructor to store the results
            // in a new Vectors collection.
            Predicate<Vector> searcher = new Predicate<Vector>(VectorDelegates.TopRightQuadrant);
            Vectors topRightQuadrantRoute = new Vectors(route.FindAll(searcher));
            WriteLine(topRightQuadrantRoute.Sum());

            ReadKey();
        }
    }
}
