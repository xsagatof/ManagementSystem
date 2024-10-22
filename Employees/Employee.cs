using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Employees
{
    public abstract class Employee
    {
        public string name;

        public int id;

        public float baseSalary;

        public Employee(string name, int id, float salary)
        {
            this.name = name;
            this.id = id;
            baseSalary = salary;
        }

        public void setName(string name)
        {
            this.name = name;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public void setBaseSalary(float salary)
        {
            if (salary < 0)
            {
                throw new InvalidSalaryException("Negative salary, please enter positive one");
            }
            else
            {
                baseSalary = salary;
            }
        }

        public string getName()
        {
            return name;
        }
        public int getId()
        {
            return id;
        }
        public float getSalary()
        {
            return baseSalary;
        }

        public abstract double calculateSalary();
        public abstract void displayEmployeeDetails();
    }
}
