namespace demo_attempt2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //webApplication is special class provided by Microsoft used to configure the HTTP request/pipeline and routes
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers(); //added because without this your cors services will fail upon attempted use
            //missing addCors() internal Exception

            var app = builder.Build(); //this creates the webapp

            //need to fix the cors problem encountered with post AJAX call
            //will allow web service to be called from any website
            app.UseCors(x=> x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin=>true));

            app.UseDeveloperExceptionPage(); //display error messages for developers. Remove it once everything works

            //Simple lambda statements are used for demo purposes only
            app.MapGet("/", () => "Hello World!");

            app.MapGet("/special" , ()=>"When you ask for special, I will provide it to you");

            app.MapGet("/rocketShip", () => "8============D------______"); //ignore this I might just be a funny guy

            app.MapGet("/registerGet", (string getFirst,string getColor,string getAge)=>$"First name: {getFirst},His Favourite Colour: {getColor}, How old he is: {getAge}");

            //one would think that passing for post would be the same as the get , one would be wrong
            //uncomment this for use with html
            //app.MapPost("/registerPost", (Info sub) => $"First name: {sub.postFirst},His Favourite Colour: {sub.postColor}, How old he is: {sub.postAge}");


            //uncomment this for use with json dta
            app.MapPost("/registerPost", (Info sub) => {
                //process the inputs
                //everything else you want to do
                int timeToProcess = 45;
                double[] marks = {2.5,4.7 };

                return new
                {
                    FirstName = sub.postFirst,
                    Color = sub.postColor,
                    age = sub.postAge,
                    timeToProcess = timeToProcess,
                    Marks = marks
                };
            
            });
            //above doesn't work
            //we require an ajax call

            app.Run();
        }
        record Info (string postFirst, string postColor, string postAge);
    }
}