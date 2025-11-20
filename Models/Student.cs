using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
	public class Student
	{
		public int UserId { get; set; }
		public String StudentIdNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime EnrollmentDate { get; set; } = DateTime.Now;


		public User User { get; set; }
	}
}
