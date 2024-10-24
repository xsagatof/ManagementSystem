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

				//Create -> C

				/*Person person = new Person();
				Console.WriteLine("Enter name of new person: ");
				person.name = Console.ReadLine();

				Console.WriteLine("Enter age of new person: ");
				person.age = Convert.ToInt16(Console.ReadLine());

				string insertQuery = "INSERT INTO DETAILS(user_name,user_age) VALUES('" + person.name + "'," + person.age + ")";
				SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
				insertCommand.ExecuteNonQuery();
				Console.WriteLine("Data is successfully inserted into table!");
				*/

				// Retrieve -> R
				string displayQuery = "SELECT * FROM Details";
				SqlCommand displayCommmand = new SqlCommand(displayQuery, sqlConnection);
				SqlDataReader dataReader = displayCommmand.ExecuteReader();
				while (dataReader.Read())
				{
					Console.WriteLine("ID: " + dataReader.GetValue(0).ToString());
					Console.WriteLine("Name: " + dataReader.GetValue(1).ToString());
					Console.WriteLine("Age: " + dataReader.GetValue(2).ToString());
				}
				dataReader.Close();

				//Update -> U
				int u_id;
				int u_age;
				Console.WriteLine("Enter the user ID that you would like to update");
				u_id = int.Parse(Console.ReadLine());
				Console.WriteLine("Enter the age of the user that you will update");
				u_age = int.Parse(Console.ReadLine());
				string updateQuery = "UPDATE Details SET user_age = " + u_age + "WHERE user_id = " + u_id + "";
				SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
				updateCommand.ExecuteNonQuery();
				Console.WriteLine("Data updated successfully");
				sqlConnection.Close();
			}
			catch (Exception e1)
			{
				Console.WriteLine(e1.Message);
			}

		}
	}
}
