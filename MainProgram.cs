using ManagementSystem.Database;
using ManagementSystem.Models;
using ManagementSystem.Services;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
	public class MainProgram
	{
		private static StudentService _studentService;
		private static CourseService _courseService;
		static void Main(string[] args)
		{
			var connectionString = "Host=localhost; Port=5432; Database=student_management; Username=postgres; Password=12345";

			var dataService = new PostgresDataService(connectionString);
			_studentService = new StudentService(dataService);
			_courseService = new CourseService(dataService);

			Console.WriteLine("###   Student Management System   ###");
			ShowMainMenu();
		}

		static void ShowMainMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Main Menu ---");
				Console.WriteLine("1. Manage students");
				Console.WriteLine("2. Manage courses");
				Console.WriteLine("3. Exit");
				Console.WriteLine("Select an option: ");

				var choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						ShowStudentsMenu();
						break;
					case "2":
						ShowCoursesMenu();
						break;
					case "3":
						Console.WriteLine("Goodbye!");
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		static void ShowStudentsMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Student's Menu ---");
				Console.WriteLine("1. Add Student");
				Console.WriteLine("2. View All Students");
				Console.WriteLine("3. Search Students");
				Console.WriteLine("4. Update Student");
				Console.WriteLine("5. Delete Student");
				Console.WriteLine("6. Exit");
				Console.Write("Select an option: ");

				var choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						AddStudent();
						break;
					case "2":
						ViewAllStudents();
						break;
					case "3":
						SearchStudents();
						break;
					case "4":
						UpdateStudent();
						break;
					case "5":
						DeleteStudent();
						break;
					case "6":
						Console.WriteLine("Goodbye!");
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		static void ShowCoursesMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Course's Menu ---");
				Console.WriteLine("1. Add Course");
				Console.WriteLine("2. View All Courses");
				Console.WriteLine("3. Search Course");
				Console.WriteLine("4. Update Course");
				Console.WriteLine("5. Delete Course");
				Console.WriteLine("6. Exit");
				Console.Write("Select an option: ");

				var choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						AddCourse();
						break;
					case "2":
						ViewAllCourses();
						break;
					case "3":
						SearchCourses();
						break;
					case "4":
						//UpdateCourses();
						break;
					case "5":
						DeleteCourse();
						break;
					case "6":
						Console.WriteLine("Goodbye!");
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		static void AddCourse()
		{
			Console.WriteLine("\n--- Add New Course ---");

			var course = new Course();

			Console.WriteLine("ID of the course: ");
			course.CourseID = Convert.ToInt16(Console.ReadLine());


			Console.WriteLine("Name of the course: ");
			course.Name = Console.ReadLine();

			Console.WriteLine("Credits for the course: ");
			course.Credits = Convert.ToDouble(Console.ReadLine());

			_courseService.AddCourse(course);
		}

		static void ViewAllCourses()
		{
			Console.WriteLine("\n--- All Courses ---");
			var courses = _courseService.GetAllCourses();

			if (!courses.Any())
			{
				Console.WriteLine("No courses found.");
				return;
			}

			foreach (var course in courses)
			{
				Console.WriteLine(course);
			}
		}

		static void SearchCourses()
		{
			Console.WriteLine("\n--- Search Courses ---");
			Console.Write("Enter course name to search: ");
			var searchTerm = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				Console.WriteLine("Search term cannot be empty.");
				return;
			}

			var courses = _courseService.SearchCourse(searchTerm);

			if (!courses.Any())
			{
				Console.WriteLine("No courses found matching your search.");
				return;
			}

			Console.WriteLine($"Found {courses.Count} course(s):");
			foreach (var course in courses)
			{
				Console.WriteLine(course);
			}
		}

		static void DeleteCourse()
		{
			Console.WriteLine("\n--- Delete Course ---");
			Console.Write("Enter Course ID to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var course = _courseService.GetCourseById(id);
			if (course == null)
			{
				Console.WriteLine($"Course with ID {id} not found.");
				return;
			}

			Console.WriteLine($"You are about to delete: {course}");
			Console.Write("Are you sure? (Y/N): ");
			var confirmation = Console.ReadLine();

			if (confirmation?.ToUpper() == "Y")
			{
				if (_courseService.DeleteStudent(id))
				{
					Console.WriteLine("Course deleted successfully.");
				}
				else
				{
					Console.WriteLine("Failed to delete course.");
				}
			}
			else
			{
				Console.WriteLine("Deletion cancelled.");
			}
		}

		static void AddStudent()
		{
			Console.WriteLine("\n--- Add New Student ---");

			var student = new Student();

			Console.Write("Full Name: ");
			student.Fullname = Console.ReadLine();

			Console.Write("Age: ");
			student.Age = Convert.ToInt32(Console.ReadLine());

			Console.Write("Email: ");
			student.Email = Console.ReadLine();

			Console.WriteLine("Faculty: ");
			student.Faculty = Console.ReadLine();

			Console.WriteLine("Date of Birth: ");
			student.DateOfBirth = Convert.ToDateTime(Console.ReadLine());

			Console.WriteLine("Enrollment date: ");
			student.EnrollmentDate = Convert.ToDateTime(Console.ReadLine());

			_studentService.AddStudent(student);
		}

		static void ViewAllStudents()
		{
			Console.WriteLine("\n--- All Students ---");
			var students = _studentService.GetAllStudents();

			if (!students.Any())
			{
				Console.WriteLine("No students found.");
				return;
			}

			foreach (var student in students)
			{
				Console.WriteLine(student);
			}
		}

		static void SearchStudents()
		{
			Console.WriteLine("\n--- Search Students ---");
			Console.Write("Enter name to search: ");
			var searchTerm = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				Console.WriteLine("Search term cannot be empty.");
				return;
			}

			var students = _studentService.SearchStudents(searchTerm);

			if (!students.Any())
			{
				Console.WriteLine("No students found matching your search.");
				return;
			}

			Console.WriteLine($"Found {students.Count} student(s):");
			foreach (var student in students)
			{
				Console.WriteLine(student);
			}
		}

		static void UpdateStudent()
		{
			Console.WriteLine("\n--- Update Student ---");
			Console.Write("Enter Student ID to update: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var existingStudent = _studentService.GetStudentById(id);
			if (existingStudent == null)
			{
				Console.WriteLine($"Student with ID {id} not found.");
				return;
			}

			Console.WriteLine($"Current details: {existingStudent}");

			var updatedStudent = new Student
			{
				StudentId = existingStudent.StudentId,
				EnrollmentDate = existingStudent.EnrollmentDate
			};

			Console.Write($"Full Name ({existingStudent.Fullname}): ");
			updatedStudent.Fullname = GetInputOrDefault(Console.ReadLine(), existingStudent.Fullname);

			Console.Write($"Age ({existingStudent.Age}): ");
			if (int.TryParse(Console.ReadLine(), out int age))
				updatedStudent.Age = age;
			else
				updatedStudent.Age = existingStudent.Age;

			Console.Write($"Email ({existingStudent.Email}): ");
			updatedStudent.Email = GetInputOrDefault(Console.ReadLine(), existingStudent.Email);

			Console.Write($"Faculty ({existingStudent.Faculty}): ");
			updatedStudent.Faculty = GetInputOrDefault(Console.ReadLine(), existingStudent.Faculty);

			Console.Write($"Date of birth ({existingStudent.DateOfBirth}): ");
			if(!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
				updatedStudent.DateOfBirth = dob;
			else
				updatedStudent.DateOfBirth = existingStudent.DateOfBirth;
			
			Console.Write($"Date of enrollment ({existingStudent.EnrollmentDate}): ");
			if(!DateTime.TryParse(Console.ReadLine(), out DateTime ed))
				updatedStudent.EnrollmentDate = ed;
			else
				updatedStudent.EnrollmentDate = existingStudent.EnrollmentDate;

			if (_studentService.UpdateStudent(updatedStudent))
			{
				Console.WriteLine("Student updated successfully.");
			}
			else
			{
				Console.WriteLine("Failed to update student.");
			}
		}

		static void DeleteStudent()
		{
			Console.WriteLine("\n--- Delete Student ---");
			Console.Write("Enter Student ID to delete: ");

			if (!int.TryParse(Console.ReadLine(), out int id))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var student = _studentService.GetStudentById(id);
			if (student == null)
			{
				Console.WriteLine($"Student with ID {id} not found.");
				return;
			}

			Console.WriteLine($"You are about to delete: {student}");
			Console.Write("Are you sure? (Y/N): ");
			var confirmation = Console.ReadLine();

			if (confirmation?.ToUpper() == "Y")
			{
				if (_studentService.DeleteStudent(id))
				{
					Console.WriteLine("Student deleted successfully.");
				}
				else
				{
					Console.WriteLine("Failed to delete student.");
				}
			}
			else
			{
				Console.WriteLine("Deletion cancelled.");
			}
		}

		static string GetInputOrDefault(string input, string defaultValue)
		{
			return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
		}
	}
}