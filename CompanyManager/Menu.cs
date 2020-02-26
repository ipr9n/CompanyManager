using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace CompanyManager
{
    class Menu
    {
        private MenuItems _chooseItem = 0;

        List<Employee> employeeList = new List<Employee>();

        private int chooseEmployee = 0;
        private double hourlyRate = 5;
        private int salary = 500;
        private double managerSalary = 2000;

        private enum MenuItems
        {
            InviteEmployee,
            ShowEmployeeMenu,
            ShowEmployee,
            SetHourRate
        }

        private readonly Dictionary<MenuItems, string> _mainMenuItems = new Dictionary<MenuItems, string>()
        {
            {MenuItems.InviteEmployee, "Invite employee"},
            {MenuItems.ShowEmployeeMenu, "Show employee menu"},
            {MenuItems.ShowEmployee, "Show all employee"},
            {MenuItems.SetHourRate, "Set hourly rate to HourlyEmployee"}
        };

        public void ShowMenu()
        {

            Console.Clear();

            foreach (MenuItems menuItem in _mainMenuItems.Keys.OrderBy(menuItem => menuItem))
            {
                if (_chooseItem == menuItem)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"----->\t{_mainMenuItems[menuItem]}");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.WriteLine($"\t{_mainMenuItems[menuItem]}");
                }
            }

            CheckKey();
        }

        private void CheckKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow);
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    MenuAction(_chooseItem);
                    break;
            }

            CheckKey();
        }

        private void MakeChoise(ConsoleKey key)
        {
            if ((int) _chooseItem < _mainMenuItems.Count - 1 && key == ConsoleKey.DownArrow)
            {
                _chooseItem++;
                ShowMenu();
            }
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow)
            {
                _chooseItem--;
                ShowMenu();
            }
        }

        private void MenuAction(MenuItems chooseItem)
        {
            switch (chooseItem)
            {
                case MenuItems.InviteEmployee:
                    InviteEmployee();
                    break;
                case MenuItems.ShowEmployeeMenu:
                    ShowEmployeeMenu();
                    break;
                case MenuItems.ShowEmployee:
                    ShowEmployee();
                    break;
                case MenuItems.SetHourRate:
                    SetHourRate();
                    break;
            }

            ShowMenu();
        }

        private void SetHourRate()
        {
            double tempRate = default;
            while (!Double.TryParse(Console.ReadLine(), out tempRate))
            {
                Console.WriteLine("Write correct hourly rate");
            }

            hourlyRate = tempRate;

            Console.WriteLine("Hourly rate is set");
            Console.ReadKey();
        }

        private void ShowEmployeeMenu()
        {
            Console.Clear();

            if (employeeList.Count > 0)
            {
                for (var employeeIndex = 0; employeeIndex < employeeList.Count; employeeIndex++)
                {
                    var employee = employeeList[employeeIndex];

                    if (chooseEmployee == employeeIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        switch (employee)
                        {
                            case HourlyEmployee hourlyEmployee:
                                Console.WriteLine($"----->{hourlyEmployee}");
                                break;
                            case SalariedEmployee salariedEmployee:
                                Console.WriteLine($"----->{salariedEmployee}");
                                break;
                            case Manager manager:
                                Console.WriteLine($"----->{manager}");
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {

                        switch (employee)
                        {
                            case HourlyEmployee hourlyEmployee:
                                Console.WriteLine(hourlyEmployee);
                                break;
                            case SalariedEmployee salariedEmployee:
                                Console.WriteLine(salariedEmployee);
                                break;
                            case Manager manager:
                                Console.WriteLine(manager);
                                break;
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\t1) Change hourly rate or salary ( Hourly/salary employee)\n\t" +
                                  "2) Change this employee to salary\n\t" +
                                  "3) Change this employee to hourlyEmployee\n\t" +
                                  "4) Change this employee to manager\n\t" +
                                  "5) Uninvite this employee");
                Console.ForegroundColor = ConsoleColor.Red;

                CheckEmployeeKey();
            }
            else
            {
                Console.WriteLine("No one employee");
                Console.ReadKey();
            }
        }

        private void CheckEmployeeKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeEmployeeChoise(ConsoleKey.DownArrow);
                    break;

                case ConsoleKey.UpArrow:
                    MakeEmployeeChoise(ConsoleKey.UpArrow);
                    break;

                case ConsoleKey.D1:
                    ChangeRate();
                    ShowEmployeeMenu();
                    break;
                case ConsoleKey.D2:
                    ChangeToSalary();
                    ShowEmployeeMenu();
                    break;
                case ConsoleKey.D3:
                    ChangeToHourly();
                    ShowEmployeeMenu();
                    break;
                case ConsoleKey.D4:
                    ChangeToManager();
                    ShowEmployeeMenu();
                    break;
                case ConsoleKey.D5:
                    employeeList.RemoveAt(chooseEmployee);
                    chooseEmployee = 0;
                    ShowEmployeeMenu();
                    break;
            }
        }

        private void ChangeToSalary()
        {
            Console.Clear();

            if (employeeList[chooseEmployee] is SalariedEmployee)
            {
                Console.WriteLine("Employee is already salary");
            }

            else if (employeeList[chooseEmployee] is HourlyEmployee || employeeList[chooseEmployee] is Manager)
            {
                employeeList[chooseEmployee] = new SalariedEmployee(employeeList[chooseEmployee].name,
                    employeeList[chooseEmployee].age, employeeList[chooseEmployee].salaryDate, salary);
                Console.WriteLine("Complete");
            }

            Console.ReadKey();
        }

        private void ChangeToManager()
        {
            Console.Clear();

            if (employeeList[chooseEmployee] is SalariedEmployee)
            {
                Console.WriteLine("Employee is already manager");
            }

            else if (employeeList[chooseEmployee] is HourlyEmployee || employeeList[chooseEmployee] is SalariedEmployee)
            {
                employeeList[chooseEmployee] = new Manager(employeeList[chooseEmployee].name,
                    employeeList[chooseEmployee].age, employeeList[chooseEmployee].salaryDate,managerSalary);
                Console.WriteLine("Complete");
            }

            Console.ReadKey();
        }

        private void ChangeToHourly()
        {
            Console.Clear();

            if (employeeList[chooseEmployee] is HourlyEmployee)
            {
                Console.WriteLine("Employee is already hourly");
            }

            else if (employeeList[chooseEmployee] is SalariedEmployee || employeeList[chooseEmployee] is Manager)
            {
                employeeList[chooseEmployee] = new HourlyEmployee(employeeList[chooseEmployee].name,
                    employeeList[chooseEmployee].age, employeeList[chooseEmployee].salaryDate, salary);
                Console.WriteLine("Complete");
            }

            Console.ReadKey();
        }

        private void ChangeRate()
        {
            Console.Clear();

            if (employeeList[chooseEmployee] is SalariedEmployee salaried)
            {
                Console.WriteLine("Write new salary for this employee");

                salaried.ChangeSalary(GetDouble());
            }

            if (employeeList[chooseEmployee] is HourlyEmployee hourly)
            {
                Console.WriteLine("Write new hourly rate for this employee");
                hourly.ChangeHourlyRate(GetDouble());
            }

            if (employeeList[chooseEmployee] is Manager manager)
            {
                Console.WriteLine("Write new salary for manager");
                manager.ChangeManagerSalary(GetDouble());
            }

            Console.WriteLine("Complete. Any key to return");
        }

        private double GetDouble()
        {
            double tempRate = default;

            while (!Double.TryParse(Console.ReadLine(), out tempRate))
            {
                Console.WriteLine("Write correct");
            }

            hourlyRate = tempRate;

            return tempRate;
        }

        private void MakeEmployeeChoise(ConsoleKey key)
        {
            if (chooseEmployee < employeeList.Count - 1 && key == ConsoleKey.DownArrow)
            {
                chooseEmployee++;
                ShowEmployeeMenu();
            }
            else if (chooseEmployee > 0 && key == ConsoleKey.UpArrow)
            {
                chooseEmployee--;
                ShowEmployeeMenu();
            }
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
                CheckEmployeeKey();
        }

        private void InviteEmployee()
        {
            Console.Clear();
            Console.WriteLine("Choose position:\n" +
                              "1)Hourly employee\n" +
                              "2)Salaried employee\n" +
                              "3)Manager");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    employeeList.Add(new HourlyEmployee(Guid.NewGuid().ToString(), new Random().Next(18, 60),
                        DateTime.Now, hourlyRate));
                    break;
                case ConsoleKey.D2:
                    employeeList.Add(new SalariedEmployee(Guid.NewGuid().ToString(), new Random().Next(18, 60),
                        DateTime.Now, salary));
                    break;
                case ConsoleKey.D3:
                    employeeList.Add(new Manager(Guid.NewGuid().ToString(), new Random().Next(18, 60), DateTime.Now, managerSalary));
                    break;
                default:
                    InviteEmployee();
                    break;
            }
        }

        private void ShowEmployee()
        {
            Console.Clear();

            if (employeeList.Count > 0)
            {
                foreach (var employee in employeeList)
                {
                    switch (employee)
                    {
                        case HourlyEmployee hourlyEmployee:
                            Console.WriteLine(hourlyEmployee);
                            break;
                        case SalariedEmployee salariedEmployee:
                            Console.WriteLine(salariedEmployee);
                            break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bosses:");
                Console.ForegroundColor = ConsoleColor.Red;

                foreach (var employee in employeeList)
                {
                    if (employee is Manager)
                        Console.WriteLine(((Manager)employee));
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No one employee");
                Console.ReadKey();
            }
        }
    }
}
