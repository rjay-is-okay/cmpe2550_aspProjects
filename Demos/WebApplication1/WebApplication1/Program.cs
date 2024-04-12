using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => {

                using (var db = new Rwatson31NorthWindContext())
                {
                    var result =    from c in db.Categories
                                    select new { c.CategoryId, c.CategoryName, c.Description};
                    return result.ToList();

                }
            });

            app.Run();
        }
    }
}