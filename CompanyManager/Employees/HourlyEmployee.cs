using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyManager
{
    class HourlyEmployee : Employee
    {
        private double hourlyRate = default;

        public HourlyEmployee(string name, int age, DateTime date, double hourlyRate) : base(name, age, date)
        {
            this.hourlyRate = hourlyRate;

        }

        public void ChangeHourlyRate(double rate) => hourlyRate = rate;

        public override string ToString() => $"Position: Hourly Employee; Name: {name}; Age: {age}; Hourly rate: {hourlyRate}; Profit: {((DateTime.Now - salaryDate)).TotalMinutes * hourlyRate}; SalaryDate: {salaryDate}";
    }
}
