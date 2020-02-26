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
            if ((int)_chooseItem < _mainMenuItems.Count - 1 && key == ConsoleKey.DownArrow)
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

            for (var employeeIndex = 0; employeeIndex < employeeList.Count; employeeIndex++)
            {
                var employee = employeeList[employeeIndex];

                if (chooseEmployee == employeeIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (employee is HourlyEmployee hourlyEmployee)
                        Console.WriteLine($"----->{hourlyEmployee.GetInfo()}");

                    if (employee is SalariedEmployee salariedEmployee)
                        Console.WriteLine($"----->{salariedEmployee.GetInfo()}");

                    if (employee is Manager manager)
                        Console.WriteLine($"----->{manager.GetInfo()}");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {

                    if (employee is HourlyEmployee hourlyEmployee)
                        Console.WriteLine(hourlyEmployee.GetInfo());

                    if (employee is SalariedEmployee salariedEmployee)
                        Console.WriteLine(salariedEmployee.GetInfo());

                    if (employee is Manager manager)
                        Console.WriteLine(manager.GetInfo());
                }
            }

            CheckEmployeeKey();
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
            }
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
                    employeeList.Add(new HourlyEmployee(Guid.NewGuid().ToString(), new Random().Next(18, 60), DateTime.Now, hourlyRate));
                    break;
                case ConsoleKey.D2:
                    employeeList.Add(new SalariedEmployee(Guid.NewGuid().ToString(), new Random().Next(18, 60), DateTime.Now, salary));
                    break;
                case ConsoleKey.D3:
                    employeeList.Add(new Manager(Guid.NewGuid().ToString(), new Random().Next(18, 60), DateTime.Now));
                    break;
                default:
                    InviteEmployee();
                    break;
            }
        }

        private void ShowEmployee()
        {
            Console.Clear();

            foreach (var employee in employeeList)
            {
                if (employee is HourlyEmployee hourlyEmployee)
                    Console.WriteLine(hourlyEmployee.GetInfo());

                if (employee is SalariedEmployee salariedEmployee)
                    Console.WriteLine(salariedEmployee.GetInfo());
            }

            Console.WriteLine("Bosses:");

            foreach (var employee in employeeList)
            {
                if (employee is Manager)
                    Console.WriteLine(((Manager)employee).GetInfo());
            }

            Console.ReadKey();
        }
    }
}
