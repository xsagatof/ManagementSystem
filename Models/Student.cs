using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
	public class Student
	{
		public int StudentiD { get; set; }
		public string Fullname { get; set; }
		public string Faculty { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime EnrollmentDate { get; set; } = DateTime.Now;
	}
}
