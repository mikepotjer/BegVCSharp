using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.ServiceModel;
using Ch22Ex02Contracts;

namespace Ch22Ex02Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] people = new Person[]
            {
                new Person {Mark = 46, Name = "Jim"},
                new Person {Mark = 73, Name = "Mike"},
                new Person {Mark = 92, Name = "Stefan"},
                new Person {Mark = 24, Name = "Arthur"}
            };

            WriteLine("People:");
            OutputPeople(people);

            // Create a proxy class implementing the IAwardService client.
            IAwardService client = ChannelFactory<IAwardService>.CreateChannel(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost:2919/AwardService.svc"));
            client.SetPassMark(70);

            Person[] awardedPeople = client.GetAwardedPeople(people);

            WriteLine();
            WriteLine("Awarded People:");
            OutputPeople(awardedPeople);
            ReadKey();
        }

        private static void OutputPeople(Person[] people)
        {
            foreach (Person person in people)
                WriteLine($"{person.Name}, mark: {person.Mark}");
        }
    }
}
