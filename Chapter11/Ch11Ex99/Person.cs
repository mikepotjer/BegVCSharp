using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch11Ex99
{
    public class Person : ICloneable
    {
        // Fields for person name and age.
        private string name;
        private int age;

        // Property for person name.
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Property for person age.
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        // Direct the default constructor to the nondefault, so we always have
        // initial values.
        public Person() : this("The person with no name", 0) { }

        // Add a nondefault constructor to simplify adding a person.
        public Person(string newName, int newAge)
        {
            name = newName;
            age = newAge;
        }

        public override string ToString()
        {
            return name + "; age " + age.ToString();
        }

        // No deep cloning is needed here, so the default implementation is fine.
        public object Clone() => MemberwiseClone();

        #region Operator overloads
        public static bool operator >(Person person1, Person person2)
        {
            return (person1.Age > person2.Age);
        }

        public static bool operator <(Person person1, Person person2)
        {
            return (person1.Age < person2.Age);
        }

        public static bool operator >=(Person person1, Person person2)
            => !(person1 < person2);

        public static bool operator <=(Person person1, Person person2)
            => !(person1 > person2);
        #endregion
    }
}
