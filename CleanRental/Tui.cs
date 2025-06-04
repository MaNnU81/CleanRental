using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanRental
{
    internal class Tui
    {
        public BusinessLogic Logic { get; set; } 


        public Tui(BusinessLogic logic) 
        
        {
            Logic = logic;
        }

        public void Run()
        {

            while (true)
            {
                Console.WriteLine("Welcome to CleanRental!");   
                Console.WriteLine("1. Show all movies");
                Console.WriteLine("2. Show all commedy films");
                Console.WriteLine("3. Show all commedy actors");
                Console.WriteLine("4. Show store number by country");
                Console.WriteLine("5. Show movies rental number");
                Console.WriteLine("6. show actors ordered by rental number");
                Console.WriteLine("7. Show movies ordered by rental income");
                //============================================================
                Console.WriteLine("8. Show all movies by genre");
                Console.WriteLine("9. Show all movies by actor");   //mostrare tutti gli attori e poi chiedere il numero id dell'attore per trovare tutti i suoi film
                Console.WriteLine("10. Show all actors");
                Console.WriteLine("11. Show all categories");
                Console.WriteLine("12. Show all moviev with Actors");

                Console.WriteLine(". Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        Console.WriteLine("Here are all the movies:");
                        DisplayAllMovies();
                        break;
                    case "2":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

            }

        }

        private void DisplayAllMovies()
        {
            var movies = Logic.GetAllMovies();
            foreach (var movie in movies)
            {
                Console.WriteLine($"Title: {movie.Title}");
            }
        }
    }
}
