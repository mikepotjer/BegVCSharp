using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Ch11Ex01
{
    class Chicken : Animal
    {
        public Chicken(string newName) : base(newName) { }

        public void LayEgg() => WriteLine($"{name} has laid an egg.");
    }
}
