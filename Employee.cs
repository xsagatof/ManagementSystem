using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
	public abstract class Employee
	{
		public string Name;

		public int Id;

		public float Salary;

		public Employee(string name, int id, float salary)
		{
			Name = name;
			Id = id;
			Salary = salary;
		}

		public void setName(string name)
		{
			Name = name;
		}
		public void setId(int id)
		{
			Id = id;
		}
		public void setSalary(float salary)
		{
			Salary = salary;
		}

		public string getName()
		{
			return Name;
		}
		public int getId()
		{
			return Id;
		}
		public float getSalary()
		{
			return Salary;
		}

		public abstract double calculateSalary();
		public abstract void displayEmployeeDetails();
	}
}
