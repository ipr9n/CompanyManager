using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    class SalariedEmployee : Employee
    {
        private int salary = default;
        public SalariedEmployee(string name, int age, DateTime date, int salary) : base(name, age, date)
        {
            this.salary = salary;
        }
        // 180 minutes in mouth is job
        public string GetInfo() => $"Position: Salaried employee; Name: {name}; Age: {age}; Salary: {salary}; Profit: {salary/180* ((TimeSpan)(DateTime.Now - salaryDate)).TotalMinutes}; SalaryDate: {salaryDate}";
    }
}
