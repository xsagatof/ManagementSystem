using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
	public class Enrollment
	{
		public int EnrollmentId { get; set; }
		public int StudentId { get; set; }
		public int CourseId { get; set; }
		public string? Grade { get; set; }


		public override string ToString()
		{
			return $"Enrollment ID: {EnrollmentId} | Student: {StudentId} | Course: {CourseId} | Grade: {Grade}";
		}
	}
}
