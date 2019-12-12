using System;
using System.Linq;

namespace Employee
{
    abstract class Employee
    {
        protected double _pay;
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public abstract double Salary();
    }
    
    class HourlyEmployee: Employee
    {
        private double _hoursWorked;
        private double _hourlyRate;

        public HourlyEmployee(int ID, string name, double _hoursWorked, double _hourlyRate)
        {
            EmployeeID = ID;
            EmployeeName = name;
            this._hoursWorked = _hoursWorked;
            this._hourlyRate = _hourlyRate;
        }
        public override double Salary()
        {
            _pay = _hourlyRate * _hoursWorked;
            return _pay;
        }
    }

    class SalariedEmployee: Employee
    {
        private double _totalSalary;
        private int _totalHours = 2080;
        private double _hoursWorked;

        public SalariedEmployee(int ID, string name, double _hoursWorked, double _totalSalary)
        {
            EmployeeID = ID;
            EmployeeName = name;
            this._hoursWorked = _hoursWorked;
            this._totalSalary = _totalSalary;
        }
        public override double Salary()
        {
            _pay = (_totalSalary / _totalHours) * _hoursWorked;
            return _pay;
        }
    }

    class Manager: Employee
    {
        private double _paySum;
        private double _avgPay;
        private int _executiveEmployees;
        private int _nonExecutiveEmployees;

        public Manager(int ID, string name, Employee[] employees)
        {
            EmployeeID = ID;
            EmployeeName = name;
            foreach (Employee employee in employees)
            {
                if (employee is Manager || employee is Executive)
                    _executiveEmployees++; 
                else
                    _paySum += (employee == null) ? 0.0 : employee.Salary();                
            }
            _nonExecutiveEmployees = employees.Length - _executiveEmployees;
            _avgPay = _paySum / _nonExecutiveEmployees;            
        }
        public override double Salary()
        {
            return _avgPay * (_nonExecutiveEmployees/2);
        }
    }

    class Executive: Employee
    {
        private double _paySum;
        private double _avgPay;
        private int _executiveEmployees;
        private int _nonExecutiveEmployees;
        public Executive(int ID, string name, Employee[] employees)
        {
            EmployeeID = ID;
            EmployeeName = name;
            foreach (Employee employee in employees)
            {
                if (employee is Manager || employee is Executive)
                    _executiveEmployees++; 
                else
                    _paySum += (employee == null) ? 0.0 : employee.Salary(); 
            }
            _nonExecutiveEmployees = employees.Length - _executiveEmployees;
            _avgPay = _paySum / _nonExecutiveEmployees;
        }
        public override double Salary()
        {
            return _avgPay * _nonExecutiveEmployees;
        }
    }

    class Company
    {
        private Employee[] employees;
        private int _index;
        public Company(Employee[] employees)
        {
            this.employees = employees;
        }
        public void Hire()
        {

        }
        public Employee[] Fire(int Id)
        {
            for(int i=0; i<employees.Length; i++)
            {
                if (employees[i].EmployeeID == Id)
                {
                    _index = i;
                    
                }
            }
            while (_index < employees.Length-1)
            {
                employees[_index] = employees[_index+1];
                _index++;
            }
            employees[_index] = null;
            employees = employees.Where(emp => emp != null).ToArray();
            return employees;
        }
        public void Raise() 
        { 
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = new Employee[10];
            employees[0] = new HourlyEmployee(1, "A",250.5, 40);
            employees[1] = new SalariedEmployee(2, "B",300, 60000);
            employees[2] = new HourlyEmployee(3, "C",450, 50);
            employees[3] = new SalariedEmployee(4, "D", 350, 75000);
            employees[4] = new HourlyEmployee(5, "E", 300, 35);
            employees[5] = new SalariedEmployee(6, "F", 458.9, 65900);
            employees[6] = new HourlyEmployee(7, "G", 560, 80);
            employees[7] = new SalariedEmployee(8, "H", 259.9, 70900);
            employees[8] = new Manager(9, "M", employees);
            employees[9] = new Executive(10, "CEO", employees); 
            foreach(Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeName} with employee ID {employee.EmployeeID} is a/an {employee.GetType().Name} with salary {employee.Salary()}");
            }
            
            Company company = new Company(employees);
            employees = company.Fire(4);
            
            foreach (Employee employee in employees)
            {
                 Console.WriteLine($"{employee.EmployeeName} with employee ID {employee.EmployeeID} is a/an {employee.GetType().Name} with salary {employee.Salary()}");
            }

        }
    }
}

