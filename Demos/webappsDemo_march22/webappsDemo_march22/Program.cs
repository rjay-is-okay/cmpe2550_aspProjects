using System.Data.SqlClient;

namespace webappsDemo_march22
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            /*
             * Define a connection string that includes information about your database server, db name, authentication
             * 
             */
            string connectionString = "Server=data.cnt.sast.ca,24680;" + "Database=demo_db2550_Northwind;" + "User Id=demoUser;" + "Password=temP2020#;" + "Encrypt=False;";
            


            app.MapGet("/", () => "Hello World!");

            app.MapGet("/RetrieveData", () =>
            {
                Console.WriteLine("Inside Retrievedata handler");

                /*Use sqlconnection class to open a connection to the DB
             * Create and open the connection in a using block. 
             * This ensures that all resources will be closed and disposed when the code Exists
             */
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //open the connection in try/ catch blocks

                    try
                    {
                        con.Open();
                        Console.WriteLine("Connection is open");

                        string query = "select * from Employees";

                        //you can use sqlcommand class to execute sql queries
                        using (SqlCommand com = new SqlCommand(query,con))
                        {
                            //create sqldatareader object to store your retreived data
                            using (SqlDataReader ret = com.ExecuteReader())
                            {
                                while (ret.Read())
                                {
                                    //accesss data using ret["ColumnName"] or ret.GetXXX()
                                    Console.WriteLine($"{ret["EmployeeID"]} | {ret.GetString(1)}");
                                    //now can return the data in json format or in html format
                                }
                            }
                                try
                                {

                                }
                                catch (SqlException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        //return a json or string back to client the way used in ICA06
                    }
                }
            });
            

                app.Run();
        }
    }
}