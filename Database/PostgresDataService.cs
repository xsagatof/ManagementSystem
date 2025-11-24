using ManagementSystem.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

		public  List<Student> LoadStudents() 
		{
			var students = new List<Student>();

			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM students";
			using var command = new NpgsqlCommand(sql, connection);
			using var reader = command.ExecuteReader();

			while (reader.Read())
			{
				var student = new Student()
				{
					StudentId = reader.GetInt32("studentid"),
					Fullname = reader.GetString("fullname"),
					Age = reader.GetInt32("age"),
					Email = reader.GetString("email"),
					Faculty = reader.GetString("faculty")
				};
				students.Add(student);
			}
			return students;
		}
		public  void SaveStudent(Student student) { }
		public  void UpdateStudent(Student student) { }
		public  void DeleteStudent(int id) { }
		public Student GetStudentById (int Id) { }
		public List<Student> SearchStudents(string searchStudents) { }
	}
}
