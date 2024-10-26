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
			sqlConnection = new SqlConnection(connectionString);
			sqlConnection.Open();
			Console.WriteLine("Connection established successfully");
			string askUser;
			try
			{
				Console.WriteLine("Admin - A || Employee - E\n Type your role: ");
				char role = char.Parse(Console.ReadLine());
				switch (role)
				{
					case 'A':
						Console.WriteLine("Enter password: ");
						string password = Console.ReadLine();
						if (password == "*****")
						{
							do
							{
								Console.WriteLine("Select command to run: \n1.Create\n2.Read\n3.Update\n4.Delete");
								int choice = int.Parse(Console.ReadLine());
								switch (choice)
								{
									case 1:
										//Create -> C
										User user = new User();
										Console.WriteLine("Enter name of new user: ");
										user.name = Console.ReadLine();
										Console.WriteLine("Enter role of new user: ");
										user.role = Console.ReadLine();
										Console.WriteLine("Enter salary of new user: ");
										user.salary = Console.ReadLine();
										string insertQuery =
											"INSERT INTO INFO(user_name,user_role,user_salary) VALUES('" +
											user.name + "','" + user.role + "'," + user.salary + ")";
										SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
										insertCommand.ExecuteNonQuery();
										Console.WriteLine("Data is successfully inserted into table!");
										break;

									case 2:
										// Read -> R
										string displayQuery = "SELECT * FROM Info";
										SqlCommand displayCommmand = new SqlCommand(displayQuery, sqlConnection);
										SqlDataReader dataReader = displayCommmand.ExecuteReader();
										while (dataReader.Read())
										{
											Console.WriteLine("ID: " + dataReader.GetValue(0).ToString());
											Console.WriteLine("Name: " + dataReader.GetValue(1).ToString());
											Console.WriteLine("Role: " + dataReader.GetValue(2).ToString());
											Console.WriteLine("Salary: " + dataReader.GetValue(3).ToString());
										}

										dataReader.Close();
										break;

									case 3:
										//Update -> U
										int u_id;
										string u_role;
										Console.WriteLine("Enter the user ID that you would like to update");
										u_id = int.Parse(Console.ReadLine());
										Console.WriteLine("Enter the name of the user that you will update");
										u_role = Console.ReadLine();
										string updateQuery =
											"UPDATE Info SET user_role = " + u_role + "WHERE user_id = " + u_id + "";
										SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
										updateCommand.ExecuteNonQuery();
										Console.WriteLine("Data updated successfully");
										break;

									case 4:
										//Delete -> D
										int d_id;
										Console.WriteLine("Enter ID of the record that is to be deleted");
										d_id = int.Parse(Console.ReadLine());
										string deleteQuery = "DELETE FROM Info WHERE user_id = " + d_id;
										SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
										deleteCommand.ExecuteNonQuery();
										Console.WriteLine("Deleted  successfully");
										break;
									default:
										Console.WriteLine("Incorrect Input");
										break;
								}

								Console.WriteLine("Do you want to continue?");
								askUser = Console.ReadLine();
							} while (askUser != "No");
						}
						else
						{
							Console.WriteLine("Password is wrong");
						}

						break;
					case 'E':
						Console.WriteLine("Select command to run: \n1.Read");
						int choice1 = int.Parse(Console.ReadLine());
						switch (choice1)
						{
							case 1:
								// Read -> R
								string displayQuery = "SELECT * FROM Info";
								SqlCommand displayCommmand = new SqlCommand(displayQuery, sqlConnection);
								SqlDataReader dataReader = displayCommmand.ExecuteReader();
								while (dataReader.Read())
								{
									Console.WriteLine("ID: " + dataReader.GetValue(0).ToString());
									Console.WriteLine("Name: " + dataReader.GetValue(1).ToString());
									Console.WriteLine("Role: " + dataReader.GetValue(2).ToString());
									Console.WriteLine("Role: " + dataReader.GetValue(3).ToString());
								}

								dataReader.Close();
								break;
						}

						break;
				}

			}
			catch (Exception e1)
			{
				Console.WriteLine(e1.Message);
			}
			finally
			{
				sqlConnection.Close();
			}

		}
	}
}
