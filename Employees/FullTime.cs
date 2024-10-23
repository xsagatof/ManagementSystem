using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Employees
{
    public class FullTime : Employee
    {
        public FullTime(string name, int id, float salary) : base(name, id, salary)
        {
            baseSalary = salary = 0;
            this.name = name = null;
            this.id = id = 0;
            float bonus = 0;
        }

        public override double calculateSalary()
        {
            return baseSalary;
        }

        public override void displayEmployeeDetails()
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Salary: " + baseSalary);
        }
    }
}
