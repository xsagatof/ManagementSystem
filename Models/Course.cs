using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
	public class Course
	{
		public int CourseID { get; set; }
		public string CourseCode { get; set; }
		public string Name { get; set; }
		public double Credits { get; set; }

		public override string ToString()
		{
			return $"ID: {CourseID} | {CourseCode}: {Name} | Credits: {Credits}";
		}
	}
}
