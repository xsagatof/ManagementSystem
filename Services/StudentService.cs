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

		public void AddStudent() { }
		public List<Student> GetAllStudents() { }
		public Student GetStudentById(int id) { }
		public List<Student> SearchStudents(string searchTerm) { }
		public bool UpdateStudent(Student updatedStudent) { }
		public bool DeleteStudent(int id) { }
	}
}
