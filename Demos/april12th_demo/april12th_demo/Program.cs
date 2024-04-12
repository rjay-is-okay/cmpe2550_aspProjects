using System.ComponentModel;

namespace april12th_demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //string connectionString = "Server=data.cnt.sast.ca,24680;" + "Database=rwatson31_Northwind;" + "User Id=rwatson31;" + "Password=cmpe_200557814;" + "Encrypt=False;";


            app.MapGet("/", () => "Hello World!");

            app.MapGet("/RetData");

            app.MapPost("/InsertCategory", () =>
            {
                //return "Freom Insert Category"
                using (var db = new DemoDb2550NorthwindContext() => {
                    Category c = new Category();

                    c.CategoryName = "Test Category";
                    c.Description = "Test Desc";

                    try
                    {
                        db.Categories.Add(c);
                    }
                    catch (Exception){
                        return "Error while inserting data";
                    }
                    
            }

            });
            app.Run();
        }
    }
}