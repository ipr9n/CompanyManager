using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    class Manager : Employee
    {
        public Manager(string name, int age, DateTime date) : base(name, age, date) { }

        public string GetInfo() => $"Position: Manager; Name: {name}; Age: {age}; SalaryDate: {salaryDate}";
    }
}