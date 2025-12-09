using ManagementSystem.Database;
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Services
{
	public class EnrollmentService
	{
	
		private readonly PostgresDataService _dataService;
		private readonly StudentService _studentService;
		private readonly CourseService _courseService;

		public EnrollmentService(PostgresDataService dataService, StudentService studentService, CourseService courseService)
		{
			_dataService = dataService;
			_studentService = studentService;
			_courseService = courseService;
		}

		public void EnrollStudent(int studentId, int courseId)
		{
			var student = _studentService.GetStudentById(studentId);
			if (student == null)
			{
				Console.WriteLine($"Student with ID {studentId} not found.");
				return;
			}

			var course = _courseService.GetCourseById(courseId);
			if (course == null)
			{
				Console.WriteLine($"Course with ID {courseId} not found.");
				return;
			}

			var existingEnrollments = _dataService.GetStudentEnrollments(studentId);
			if (existingEnrollments.Any(e => e.CourseId == courseId))
			{
				Console.WriteLine($"Student {studentId} is already enrolled in course {courseId}.");
				return;
			}

			var enrollment = new Enrollment
			{
				StudentId = studentId,
				CourseId = courseId,
				Grade = null
			};

			_dataService.AddEnrollment(enrollment);
			//Console.WriteLine($"Student {student.Fullname} enrolled in {course.CourseCode}: {course.CourseName}. Enrollment ID: {enrollment.EnrollmentId}");
		}

		public void ListAllEnrollments()
		{
			var enrollments = _dataService.GetEnrollments();

			if (!enrollments.Any())
			{
				Console.WriteLine("No enrollments found.");
				return;
			}

			Console.WriteLine("\n--- All Enrollments ---");

			// Option 1: Basic display (just IDs)
			foreach (var enrollment in enrollments)
			{
				Console.WriteLine(enrollment);
			}

				
			foreach (var enrollment in enrollments)
			{
				    var student = _studentService.GetStudentById(enrollment.StudentId);
				    var course = _courseService.GetCourseById(enrollment.CourseId);
				    enrollment.Student = student;
				    enrollment.Course = course;
				    Console.WriteLine(enrollment);
			}
		}
	
	}
}
