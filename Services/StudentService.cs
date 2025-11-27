using ManagementSystem.Database;
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Services
{
	public class StudentService
	{
		private readonly PostgresDataService _postgresDataService;

		public StudentService(PostgresDataService postgresDataService)
		{
			_postgresDataService = postgresDataService;
		}

		public void AddStudent(Student student) 
		{
			try
			{
				_postgresDataService.SaveNewStudent(student);
				Console.WriteLine($"Student added successfully with ID: {student.StudentId}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while adding new student: {ex.Message}");
			}
		}
		public List<Student> GetAllStudents()
		{
			try
			{
				return _postgresDataService.LoadStudents();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while retrieving all students: {ex.Message}");
				return new List<Student>();
			}
		}
		public Student GetStudentById(int id)
		{
			try
			{
				return _postgresDataService.GetStudentById(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while retrieving student by id: {ex.Message}");
				return null;
			}
		}
		public List<Student> SearchStudents(string text) 
		{
			try
			{
				return _postgresDataService.SearchStudents(text);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while searching students: {ex.Message}");
				return new List<Student>();
			}

		}
		public bool UpdateStudent(Student updatedStudent)
		{
			try
			{
				var existingStudent = GetStudentById(updatedStudent.StudentId);
				if (existingStudent == null)
					return false;

				_postgresDataService.UpdateStudent(updatedStudent);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while updating student: {ex.Message}");
				return false;
			}
		}

		public bool DeleteStudent(int id)
		{
			try
			{
				var student = GetStudentById(id);
				if (student == null)
					return false;

				_postgresDataService.DeleteStudent(id);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while deleting student: {ex.Message}");
				return false;
			}
		}
	}
}
