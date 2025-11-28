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
		static void Main(string[] args)
		{
			var connectionString = "Host=localhost; Port=5432; Database=student_management; Username=postgres; Password=12345";

			var dataService = new PostgresDataService(connectionString);
			_studentService = new StudentService(dataService);

			Console.WriteLine("###   Student Management System   ###");
			ShowMainMenu();
		}

		static void ShowMainMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Main Menu ---");
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