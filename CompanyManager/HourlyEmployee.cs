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

        public static implicit operator HourlyEmployee(SalariedEmployee x)
        {
            return new HourlyEmployee(x.name,x.age,x.salaryDate,5);
        }
        public static implicit operator SalariedEmployee(HourlyEmployee x)
        {
            return new SalariedEmployee(x.name,x.age,x.salaryDate,500);
        }


        public void ChangeHourlyRate(double rate) => hourlyRate = rate;

        public string GetInfo() => $"Position: Hourly Employee; Name: {name}; Age: {age}; Hourly rate: {hourlyRate}; Profit: {((TimeSpan)(DateTime.Now-salaryDate)).TotalMinutes*hourlyRate}; SalaryDate: {salaryDate}";
    }
}
