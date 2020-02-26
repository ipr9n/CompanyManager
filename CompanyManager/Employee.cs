using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    abstract class Employee
    {
        protected string name { get; set; }
        protected int age { get; set; }
        protected DateTime salaryDate { get; set; }

        public Employee(string name, int age, DateTime date)
        {
            this.name = name;
            this.age = age;
            this.salaryDate = date;
        }
    }
}
