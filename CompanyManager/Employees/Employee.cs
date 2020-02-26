using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    abstract class Employee
    {
        public string name { get; private set; }
        public int age { get; private set; }
        public DateTime salaryDate { get; private set; }

        public Employee(string name, int age, DateTime date)
        {
            this.name = name;
            this.age = age;
            this.salaryDate = date;
        }
    }
}
