using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
	public class Teacher : User
	{
		public int UserId { get; set; }
		public string TeacherIdNumber { get; set; }
		public string Department {  get; set; }
		public DateTime HireDate { get; set; } = DateTime.Now;

		public User User { get; set; }
	}
}
