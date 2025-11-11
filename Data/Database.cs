using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Database
{
	public class Database
	{
		private static string _connnectionString = "Server=127.0.0.1,Port=3306;Database=management_sys_net;Uid=root;Pwd=barca";

		public static SqlConnection GetConnection()
		{
			var connection = new SqlConnection(_connnectionString);
			connection.Open();
			return connection;
		}
	}
}
