using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sf.elasticsearch.demo.Domain
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string About { get; set; }
        public List<string> Interests { get; set; }

        public Employee(string firstName, string lastName, int age, string about, List<string> interests)
        {
            this.FirstName = firstName;
            this.LastName = LastName;
            this.Age = age;
            this.About = about;
            this.Interests = interests;
        }
    }
}
