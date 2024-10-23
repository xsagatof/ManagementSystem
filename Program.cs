using ManagementSystem.Employees;
using System.Data.SqlClient;

namespace ManagementSystem
{
    public class Program
	{
		static void Main(string[] args)
		{
			SqlConnection sqlConnection;
			string connectionString =
				@"Data Source=.;Initial Catalog=MyDb;Integrated Security=True";

			try
			{
				sqlConnection = new SqlConnection(connectionString);
				sqlConnection.Open();
				Console.WriteLine("Connection established successfully");

				Person person = new Person();
				Console.WriteLine("Enter name of new person: ");
				person.name = Console.ReadLine();

				Console.WriteLine("Enter age of new person: ");
				person.age = Convert.ToInt16(Console.ReadLine());

				string insertQuery = "INSERT INTO DETAILS(user_name,user_age) VALUES('" + person.name + "'," + person.age + ")";
				SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
				insertCommand.ExecuteNonQuery();
				Console.WriteLine("Data is successfully inserted into table!");

				sqlConnection.Close();
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1.Message);
			}

		}
	}
}
