

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;


namespace ica_08_asp_ado
{
    public class Program
    {
        // To sanitize your inputs
        public static string CleanInputs(string input)
        {
            return Regex.Replace(input.Trim(), "<.*?>|&.*?;", string.Empty);
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // without this your CORS services will fail upon attempted

            var app = builder.Build();
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true));
            app.UseDeveloperExceptionPage();  // Display error messages for developers. Remove it once everything is working fine


            /*
             * Define a connection strint that includes information about your 
             * database server, DB name, authentication
             * */
            string connectionString = "Server=data.cnt.sast.ca,24680;" +
                                       "Database=rwatson31_ClassTrak;" +
                                       "User Id=rwatson31;" +
                                       "Password=cmpe_200557814;" +
                                       "Encrypt=False";

            app.MapGet("/", () => "ASP ADO Demo 01");

            app.MapGet("/RetrieveData", () => {
                Console.WriteLine(" Inside Retrievedata handler");
                /* Use SqlConnetion Class to open a connetion to the DB
                 * Create and open the connection in a using block. This
                 * ensures that all reseources will be closed and disposed 
                 * when the code Exits
                 * 
                 */
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open the connetion in try/ catch block

                    try
                    {
                        
                        con.Open();
                        Console.WriteLine("Connetion is open");

                        // Query without parameters UNCOMMENT the following to test it
                        //string query = "select * from Employees";

                        // With Parameters Uncomment the following to test it with parameters

                        string query = "select * from Students s where s.first_name like 'e%' or s.first_name like 'f%' order by s.first_name asc";


                        // You can use SqlCommand class to execute SQL queries

                        //          query  , connectionObject
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            //adding parameters to the command after cleaning it

                            //command.Parameters.AddWithValue("@empId", CleanInputs("4")); // Hard coding the value to 4, but you can use your value received from client

                            // Create SqlDataReader object to store your retrieved data

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                List<string[]> fullset = new List<string[]>();
                                while (reader.Read())
                                {
                                                       
                                    // Access data using reader["ColumnName"] or reader.GetXXX() methods
                                    Console.WriteLine($"{reader["first_name"]} |  {reader.GetString(1)} |  {reader.GetString(2)} | {reader["school_id"]}");
                                    // Return the data in JSON Format back to user
                                    string[] data = { reader["student_id"].ToString(), reader["first_name"].ToString(), reader["last_name"].ToString(), reader["school_id"].ToString() };
                                    fullset.Add(data);
                                }
                                string jsonString = JsonSerializer.Serialize(fullset);
                                return jsonString;
                            }

                        }

                        // TEST IT WITH STORED PROCEDURE 
                        /* STEP 1: Create a stored procedure in the DB
                         * Step 2: Call your stored procedure from your code
                         */

                        /* Stored procedure definition: Create it in your DB
                         create or alter procedure SelectEmployee
                            (
	                            @EmployeeID int
                            )
                            as
	                            select * from Employees
	                            where EmployeeID= @EmployeeID
                            go
                         */

                        //query = "SelectEmployee";

                        ////          query  , connectionObject
                        //using (SqlCommand command = new SqlCommand(query, con))
                        //{
                        //    // Change the commnad Type
                        //    command.CommandType = System.Data.CommandType.StoredProcedure;

                        //    //adding parameters to the command after cleaning it

                        //    command.Parameters.AddWithValue("@EmployeeID", CleanInputs("4")); // Hard coding the value to 4, but you can use your value received from client

                        //    // Create SqlDataReader object to store your retrieved data

                        //    using (SqlDataReader reader = command.ExecuteReader())
                        //    {
                        //        Console.WriteLine("Testing it with SP");

                        //        while (reader.Read())
                        //        {
                        //            // Access data using reader["ColumnName"] or reader.GetXXX() methods
                        //            Console.WriteLine($"{reader["EmployeeID"]} |  {reader.GetString(1)} | |  {reader.GetString(2)}");
                        //            // Return the data in JSON Format back to user
                        //        }
                        //    }

                        //}


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        // Return a JSON or string back to Client the way you did in ICA06 07
                        return JsonSerializer.Serialize("Failure");
                    }
                    return JsonSerializer.Serialize("After catch");
                }

            });

            // Non-Retrieval commands

            app.MapDelete("/DeleteEmployee", () =>
            {
                Console.WriteLine(" Inside Delete Employee handler");
                /* Use SqlConnetion Class to open a connetion to the DB
                 * Create and open the connection in a using block. This
                 * ensures that all reseources will be closed and disposed 
                 * when the code Exits
                 * 
                 */
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open the connetion in try/ catch block

                    try
                    {
                        con.Open();
                        Console.WriteLine("Connetion is open");
                        //return "Returning a testing message";

                        string query = "Delete from Employees where EmployeeId= 35 ";

                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            // Change the command type to Stored procedure if you are running it with SP
                            //command.CommandType = System.Data.CommandType.StoredProcedure;

                            // Add parameters to command if you want to pass parameters

                            // To run non retrieval queries use ExecuteNonQuery method
                            // It will return number of rows affected back
                            // Then you can use that number to give appropriate messages to user
                            int rowAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"Number of rows affected = {rowAffected}");

                            return $"Number of rows affected = {rowAffected}";
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error");
                        return "Returning a testing message from catch";
                    }

                }
            });

            app.Run();
        }
    }
}