using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch12Ex03
{
    public class Vectors : List<Vector>
    {
        public Vectors() { }

        // Add a nondefault constructor that accepts an enumerable list of Vector types,
        // which allows us to initialize this object with a List<Vector>, which is the
        // result of a method like List<Vector>.FindAll().
        public Vectors(IEnumerable<Vector> initialItems)
        {
            foreach (Vector vector in initialItems)
            {
                Add(vector);
            }
        }

        // Add a method that sums all the vectors in this list, and displays the result.
        public string Sum()
        {
            // Use the StringBuilder from the System.Text namespace, which has better
            // performance than concatenating strings together.
            StringBuilder sb = new StringBuilder();
            Vector currentPoint = new Vector(0.0, 0.0);

            sb.Append("origin");
            foreach (Vector vector in this)
            {
                sb.AppendFormat($" + {vector}");
                currentPoint += vector;
            }
            sb.AppendFormat($" = {currentPoint}");

            return sb.ToString();
        }
    }
}
