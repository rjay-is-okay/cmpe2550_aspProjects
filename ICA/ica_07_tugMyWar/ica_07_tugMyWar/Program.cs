using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ica_07_tugMyWar
{
    public class Program
    {
        static int currentTurn = 0;
        static string play1 = "";
        static string play2 = "";

        static int location = 32;
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

            // string connectionString = "Server=data.cnt.sast.ca,24680;" + "Database=rwatson31_tugOwar;" + "User Id=rwatson31;" + "Password=cmpe_200557814;" + "Encrypt=False;";

            string gameOver = "neither";

            app.MapGet("/", () => "Hello World!");


            app.MapPost("/startNewGame", (Info given) =>
            {
                Console.Write("Inside Start new game");
                play1 = CleanInputs(given.postPlay1);
                play2 = CleanInputs(given.postPlay2);

                if(play1 != string.Empty && play2 != string.Empty)
                {
                    currentTurn = 0; 
                    location = 32;
                    return new
                    {
                        Start = true
                    };
                } else
                {
                    return new
                    {
                        Start = false
                    };
                }
            });

            app.MapPost("/registerMove", (Info sub) => {
                Random rnd = new Random();


                location += rnd.Next(-6, 7);
                currentTurn++;

                gameOver =  CheckWin();
                //everything else you want to do
                //int timeToProcess = rnd.Next(5, 31);
                //double[] marks = { 2.5, 4.7 };

                return new
                {

                    Location = location,
                    GameState = gameOver

                    //TimeToProcess = timeToProcess
                };

            });

            app.MapPost("/quitGame", (Info sub) =>
            {
                play1 = "";
                play2 = "";
                currentTurn = 0;
                location = 33;

                return new
                {
                    Player1 = play1,
                    Player2 = play2
                };
            });

            app.MapPost("/resume", () =>
            {

                return new
                {
                    Player1 = play1,
                    Player2 = play2,
                    Location = location,
                    GameState = gameOver
                };

            });
            app.Run();


        }

        public static string CheckWin()
        {
            if(location < 18)
            {
                return "player1 WON";
            } else if(location > 48)
            {
                return "player2 WON";
            } else if(currentTurn > 15){
                return "tied";
            } else
            {
                return "neither";
            }
        }

        record Info(string postPlay1, string postPlay2);

    }
}