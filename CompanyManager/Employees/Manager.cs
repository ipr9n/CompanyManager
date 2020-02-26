using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    class Manager : Employee
    {
        private double managerSalary = default;

        public Manager(string name, int age, DateTime date, double managerSalary) : base(name, age, date)
        {
            this.managerSalary = managerSalary;
        }

        public void ChangeManagerSalary(double salary) => this.managerSalary = salary;

        public override string ToString() => $"Position: Manager; Name: {name}; Age: {age}; Manager salary: {managerSalary}; Profit:  {managerSalary / 180 * (DateTime.Now - salaryDate).TotalMinutes}; SalaryDate: {salaryDate}";
    }
}