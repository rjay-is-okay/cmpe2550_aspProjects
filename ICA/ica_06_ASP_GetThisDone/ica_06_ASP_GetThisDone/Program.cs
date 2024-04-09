using System.Text.RegularExpressions;

namespace ica_06_ASP_GetThisDone
{
    public class Program
    {
        // To sanitize your inputs
        public static string CleanInputs(string input)
        {
            //input  = Regex.Replace(input.Trim(), "<.*?>|&.*?;", string.Empty);
            return Regex.Replace(input.Trim(), "<.*?>|&.*?;", string.Empty).Trim();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // without this your CORS services will fail upon attempted

            var app = builder.Build();

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true));

            app.UseDeveloperExceptionPage();  // Display error messages for developers. Remove it once everything is working fine


            app.MapGet("/", () => "Hello World!");

            //uncomment this for use with json dta
            app.MapPost("/registerPost", (Info sub) => {
                Random rnd = new Random();
                //process the inputs
                //everything else you want to do
                int timeToProcess = rnd.Next(5,31);
                double[] marks = { 2.5, 4.7 };
                
                return new
                {

                    Name = CleanInputs(sub.postName),
                    Location = "Pick up Location: "+ CleanInputs(sub.postLocation),
                    ItemOrdered = "Item Ordered: " + CleanInputs(sub.postItem),
                    Quantity = "Number of Items: " +CleanInputs(sub.postQuantity),
                    PaymentType = "Method of Payment: "+CleanInputs(sub.postPayType),
                    Time = timeToProcess

                    //TimeToProcess = timeToProcess
                };

            });
            //above doesn't work
            //we require an ajax call

            app.Run();
        }
        record Info(string postName, string postItem, string postQuantity, string postPayType, string postLocation);
    }
}