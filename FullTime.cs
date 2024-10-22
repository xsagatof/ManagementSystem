﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
	public class FullTime : Employee
	{
		public FullTime(string name, int id, float salary) : base(name, id, salary)
		{
			baseSalary = salary;
			this.name = name;
			this.id = id;
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
