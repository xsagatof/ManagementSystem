using ManagementSystem.Employees;

namespace ManagementSystem
{
    public class Program
	{
		static void Main(string[] args)
		{
			Employee Full1 = new FullTime("Ford", 2010, -90);
			try
			{
				Full1.setBaseSalary(800);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Full1.displayEmployeeDetails();
			Full1.calculateSalary();
		}
	}
}
