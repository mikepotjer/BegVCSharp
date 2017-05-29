using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Vehicle;

namespace Traffic
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle.Vehicle[] vehicles = new Vehicle.Vehicle[6];
            vehicles[0] = new Compact();
            vehicles[1] = new SUV();
            vehicles[2] = new Pickup();
            vehicles[3] = new PassengerTrain();
            vehicles[4] = new FreightTrain();
            vehicles[5] = new DoubleBogey424();

            foreach (Vehicle.Vehicle veh in vehicles)
            {
                try
                {
                    AddPassenger((IPassengerCarrier)veh);
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                    WriteLine($"Unable to add passenger to {veh.ToString()}");
                }
            }
            ReadKey();
        }

        public static void AddPassenger(IPassengerCarrier passengerCarrier)
        {
            WriteLine($"Added passenger to {passengerCarrier.ToString()}");
        }
    }
}
