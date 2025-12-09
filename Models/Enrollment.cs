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

		public Student? Student { get; set; }
		public Course? Course { get; set; }

		public override string ToString()
		{
			if (Student != null && Course != null)
			{
				//return $"Enrollment ID: {EnrollmentId} | Student: {Student.Fullname} | Course: {Course.CourseCode} - {Course.CourseName} | Grade: {Grade ?? "Not graded"}";
			}
			return $"Enrollment ID: {EnrollmentId} | Student ID: {StudentId} | Course ID: {CourseId} | Grade: {Grade ?? "Not graded"}";
		}
	}
}
