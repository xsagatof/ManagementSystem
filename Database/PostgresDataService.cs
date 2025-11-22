using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Database
{
	public class PostgresDataService
	{
		private readonly string _connectionString;

		public PostgresDataService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public  List<Student> LoadStudents() { }
		public  void SaveStudent(Student student) { }
		public  void UpdateStudent(Student student) { }
		public  void DeleteStudent(int id) { }
		public Student GetStudentById (int Id) { }
		public List<Student> SearchStudents(string searchStudents) { }
	}
}
