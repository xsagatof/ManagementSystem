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
					Faculty = reader.GetString("faculty"),
					DateOfBirth = reader.GetDateTime("dateofbirth"),
					EnrollmentDate = reader.GetDateTime("enrollmentdate")
				};
				students.Add(student);
			}
			return students;
		}
		public void SaveNewStudent(Student student) 
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"INSERT into students (fullname, age, email, faculty, dateofbirth, enrollmentdate)
						VALUES (@fullname, @age, @email, @faculty, @dateofbirth, @enrollmentdate)
						RETURNING studentid";

			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@fullname", student.Fullname);
			command.Parameters.AddWithValue("@age", student.Age);
			command.Parameters.AddWithValue("@email", student.Email);
			command.Parameters.AddWithValue("@faculty", student.Faculty);
			command.Parameters.AddWithValue("@dateofbirth", student.DateOfBirth);
			command.Parameters.AddWithValue("@enrollmentdate", student.EnrollmentDate);

			var newId = command.ExecuteScalar();
			if (newId != null)
			{
				student.StudentId = Convert.ToInt32(newId);
			}
		}
		public  void UpdateStudent(Student student) 
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"UPDATE students
						SET fullname = @fullname, 
							age = @age,
							email = @email,
							faculty = @faculty,
							dateofbirth = @dateofbirth,
							enrollmentdate = @enrollmentdate
						WHERE studentid = @studentid";

			using var command = new NpgsqlCommand( sql, connection);
			command.Parameters.AddWithValue("@studentid", student.StudentId);
			command.Parameters.AddWithValue("@fullname", student.Fullname);
			command.Parameters.AddWithValue("@age", student.Age);
			command.Parameters.AddWithValue("@email", student.Email);
			command.Parameters.AddWithValue("@faculty", student.Faculty);
			command.Parameters.AddWithValue("@dateofbirth", student.DateOfBirth);
			command.Parameters.AddWithValue("@enrollmentdate", student.EnrollmentDate);

			command.ExecuteNonQuery();
		}
		public  void DeleteStudent(int id) 
		{
			using var connection = new NpgsqlConnection( _connectionString);
			connection.Open();

			var sql = "DELETE FROM students WHERE studentid = @studentid";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@studentid", id);
			command.ExecuteNonQuery();
		}
		public Student GetStudentById (int id) 
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM students";
			using var command = new NpgsqlCommand(sql, connection);
			using var reader = command.ExecuteReader();

			if (reader.Read())
			{
				return new Student
				{
					StudentId = reader.GetInt32("studentid"),
					Fullname = reader.GetString("fullname"),
					Faculty = reader.GetString("faculty"),
					DateOfBirth = reader.GetDateTime("dateofbirth"),
					EnrollmentDate = reader.GetDateTime("enrollmentdate"),
					Email = reader.GetString("email"),
					Age = reader.GetInt32("age")
				};
			}

			return null;
		}
		public List<Student> SearchStudents(string text)
		{
			var students = new List<Student>();

			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"SELECT * FROM students
						WHERE fullname ILIKE @text
						ORDER BY studentid";

			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@text", $"%{text}%");
			using var reader = command.ExecuteReader();

			while (reader.Read())
			{
				var student = new Student
				{
					StudentId = reader.GetInt32("studentid"),
					Fullname = reader.GetString("fullname"),
					Age = reader.GetInt32("age"),
					Email = reader.GetString("email"),
					Faculty = reader.GetString("faculty"),
					DateOfBirth = reader.GetDateTime("dateofbirth"),
					EnrollmentDate = reader.GetDateTime("enrollmentdate")
				};
				students.Add(student);
			}

			return students;
		}
	}
}
