namespace ica_07_tugMyWar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            string connectionString = "Server=data.cnt.sast.ca,24680;" + "Database=rwatson31_tugOwar;" + "User Id=rwatson31;" + "Password=cmpe_200557814;" + "Encrypt=False;";


            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}