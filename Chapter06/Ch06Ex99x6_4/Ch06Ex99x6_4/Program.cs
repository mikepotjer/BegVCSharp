using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch06Ex99x6_4
{
    struct order
    {
        public string itemName;
        public int unitCount;
        public double unitCost;

        // Add a function to the structure to calculate the total cost.
        public double TotalPrice() => unitCount * unitCost;

        // Override ToString() to output the details and total for the order.
        public override string ToString()
        {
            // I didn't have the ToString() on the numeric results initially,
            // but C# seemed to know enough to convert them to strings anyway.
            return "Order Information: " + unitCount.ToString() + " " + itemName
                + " items at $" + unitCost.ToString()
                + " each, total cost $" + TotalPrice().ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            order myOrder = new order();

            myOrder.itemName = "Widget";
            myOrder.unitCost = 4.99;
            myOrder.unitCount = 12;

            WriteLine(myOrder.ToString());
            ReadKey();
        }
    }
}
