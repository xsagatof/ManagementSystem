using ManagementSystem.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		//Student database methods
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

		//Course database methods
		public List<Course> LoadCourses()
		{
			var courses = new List<Course>();

			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM courses";
			using var command = new NpgsqlCommand(sql, connection);
			using var reader = command.ExecuteReader();

			while (reader.Read())
			{
				var course = new Course()
				{
					CourseID = reader.GetInt16("courseid"),
					Name = reader.GetString("name"),
					Credits = reader.GetDouble("credits")
				};
				courses.Add(course);
			}
			return courses;
		}
		public void SaveNewCourse(Course course)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"INSERT into courses (courseid, name, credits)
						VALUES (@courseid, @name, @credits)
							RETURNING courseid";

			using var command = new NpgsqlCommand( sql, connection);
			command.Parameters.AddWithValue("@courseid", course.CourseID);
			command.Parameters.AddWithValue("@name", course.Name);
			command.Parameters.AddWithValue("@credits", course.Credits);

			var newId = command.ExecuteScalar();
			if (newId != null)
			{
				course.CourseID = Convert.ToInt32(newId);
			}
		}
		public Course GetCourseById(int id)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM courses";
			using var command = new NpgsqlCommand(sql, connection);
			using var reader = command.ExecuteReader();

			if (reader.Read())
			{
				return new Course
				{
					CourseID = reader.GetInt32("courseid"),
					Name = reader.GetString("name"),
					Credits = reader.GetDouble("credits")
				};
			}

			return null;
		}
		public void DeleteCourse(int id)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"DELETE FROM courses WHERE courseid = @courseid";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@courseid", id);
			command.ExecuteNonQuery();
		}
		public List<Course> SearchCourses(string text)
		{
			var courses = new List<Course>();

			var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"SELECT * FROM courses
						WHERE name ILIKE @text
						ORDER BY courseid";

			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@text", $"%{text}%");
			using var reader = command.ExecuteReader();

			while(reader.Read())
			{
				var course = new Course()
				{
					CourseID = reader.GetInt16("courseid"),
					Name = reader.GetString("name"),
					Credits = reader.GetDouble("credits")
				};
				courses.Add(course);
			}

			return courses;
		}


		//Enrollment database methods
		public void AddEnrollment(Enrollment enrollment)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = @"
						INSERT INTO enrollments (studentid, courseid, grade)
						VALUES (@studentId, @courseId, @grade)
						RETURNING enrollmentid";

			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@studentid", enrollment.StudentId);
			command.Parameters.AddWithValue("@courseid", enrollment.CourseId);
			command.Parameters.AddWithValue("@grade", string.IsNullOrEmpty(enrollment.Grade) ? (object)DBNull.Value : enrollment.Grade);

			var newId = command.ExecuteScalar();
			if (newId != null)
			{
				enrollment.EnrollmentId = Convert.ToInt32(newId);
			}
		}

		public List<Enrollment> GetEnrollments()
		{
			var enrollments = new List<Enrollment>();

			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM enrollments ORDER BY enrollmentid";
			using var command = new NpgsqlCommand(sql, connection);
			using var reader = command.ExecuteReader();

			while (reader.Read())
			{
				var enrollment = new Enrollment
				{
					EnrollmentId = reader.GetInt32("enrollmentid"),
					StudentId = reader.GetInt32("studentid"),
					CourseId = reader.GetInt32("courseid"),
					Grade = reader.IsDBNull("grade") ? null : reader.GetString("grade")
				};
				enrollments.Add(enrollment);
			}

			return enrollments;
		}

		public bool UpdateEnrollmentGrade(int enrollmentId, string grade)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "UPDATE enrollments SET grade = @grade WHERE enrollmentid = @id";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@id", enrollmentId);
			command.Parameters.AddWithValue("@grade", grade);

			return command.ExecuteNonQuery() > 0;
		}

		public bool DeleteEnrollment(int enrollmentId)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "DELETE FROM enrollments WHERE enrollmentid = @id";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@id", enrollmentId);

			return command.ExecuteNonQuery() > 0;
		}

		public List<Enrollment> GetStudentEnrollments(int studentId)
		{
			var enrollments = new List<Enrollment>();

			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM enrollments WHERE studentid = @studentid ORDER BY studentid";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@studentid", studentId);
			using var reader = command.ExecuteReader();

			while (reader.Read())
			{
				var enrollment = new Enrollment
				{
					EnrollmentId = reader.GetInt32("enrollmentid"),
					StudentId = reader.GetInt32("studentid"),
					CourseId = reader.GetInt32("courseid"),
					Grade = reader.IsDBNull("grade") ? null : reader.GetString("grade")
				};
				enrollments.Add(enrollment);
			}

			return enrollments;
		}

		public Enrollment? GetEnrollmentById(int enrollmentId)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT * FROM enrollments WHERE enrollmentid = @id";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@id", enrollmentId);
			using var reader = command.ExecuteReader();

			if (reader.Read())
			{
				return new Enrollment
				{
					EnrollmentId = reader.GetInt32("enrollmentid"),
					StudentId = reader.GetInt32("studentid"),
					CourseId = reader.GetInt32("courseid"),
					Grade = reader.IsDBNull("grade") ? null : reader.GetString("grade")
				};
			}

			return null;
		}

		public bool IsStudentEnrolledInCourse(int studentId, int courseId)
		{
			using var connection = new NpgsqlConnection(_connectionString);
			connection.Open();

			var sql = "SELECT COUNT(*) FROM enrollments WHERE studentid = @studentid AND courseid = @courseid";
			using var command = new NpgsqlCommand(sql, connection);
			command.Parameters.AddWithValue("@studentid", studentId);
			command.Parameters.AddWithValue("@courseid", courseId);

			var count = Convert.ToInt32(command.ExecuteScalar());
			return count > 0;
		}

	}
}
