using ManagementSystem.Database;
using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Services
{
	public class CourseService
	{
		private readonly PostgresDataService _postgresDataService;

		public CourseService(PostgresDataService postgresDataService)
		{
			_postgresDataService = postgresDataService;
		}

		public void AddCourse(Course course)
		{
			try
			{
				_postgresDataService.SaveNewCourse(course);
				Console.WriteLine($"Course added successfully with ID: {course.CourseID}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while adding new student: {ex.Message}");
			}
		}
		public List<Course> GetAllCourses()
		{
			try
			{
				return _postgresDataService.LoadCourses();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while retrieving all courses: {ex.Message}");
				return new List<Course>();
			}
		}
		public Course GetCourseById(int id)
		{
			try
			{
				return _postgresDataService.GetCourseById(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while retrieving course by id: {ex.Message}");
				return null;
			}
		}
		public bool DeleteStudent(int id)
		{
			try
			{
				_postgresDataService.DeleteCourse(id);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while deleting course: {ex.Message}");
				return false;
			}
		}
		public List<Course> SearchCourse(string text)
		{
			try
			{
				return _postgresDataService.SearchCourses(text);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error while searching students: {ex.Message}");
				return new List<Course>();
			}
		}
	}
}
