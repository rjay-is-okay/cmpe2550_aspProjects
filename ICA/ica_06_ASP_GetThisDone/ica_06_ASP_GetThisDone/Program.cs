namespace ica_06_ASP_GetThisDone
{
    public class Program
    {
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
                //process the inputs
                //everything else you want to do
                int timeToProcess = 45;
                double[] marks = { 2.5, 4.7 };

                return new
                {
                    Name = sub.postName,
                    ItenOrdered = sub.postItem,
                    Quantity = sub.postQuantity,
                    PaymentType = sub.postPayType,
                    Location = sub.postLocation,
                    TimeToProcess = timeToProcess
                    //Marks = marks
                };

            });
            //above doesn't work
            //we require an ajax call

            app.Run();
        }
        record Info(string postName, string postItem, string postQuantity, string postPayType, string postLocation);
    }
}