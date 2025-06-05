using CleanRental.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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
                Console.WriteLine("12. Show all movies with Actors");

                Console.WriteLine("13. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Here are all the movies:");
                        DisplayAllMovies();
                        break;
                    case "2":
                        Console.WriteLine("Here are all the comedy movies:");
                        DisplayAllComedyMovies();
                        break;
                    case "3":
                        Console.WriteLine("Here are all the comic actors:");
                        DisplayAllComedyActors();
                        break;
                    case "4":
                        Console.WriteLine("Here are the number of store:");
                        DisplayStoreByCountry();
                        break;
                    case "5":
                        Console.WriteLine("Movie rental number:");
                        DisplayMoviesRentalNumber();
                        break;
                    case "6":
                        Console.WriteLine("Actor by rental number:");
                        DisplayActorsOrderedByRentalNumber();
                        break;
                    case "7":
                        Console.WriteLine("Movie order By rental income:");
                       DisplayActorsByRentalIncome();
                        break;
                    case "8":
                        Console.WriteLine("Movie by genre:");
                        DisplayMoviesByGenre();
                        break;
                    case "9":
                        DisplayAllMoviesByActor();
                        break;
                    case "10":
                        Console.WriteLine("Show all actors:");
                        DisplayAllActors();
                        break;
                    case "11":
                        Console.WriteLine("Show all categories:");
                        DisplayAllCategories();
                        break;
                    case "12":
                        Console.WriteLine("Show all movies with actor:");
                        DisplayAllMoviesWithActor();
                        break;

                    case "13":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

            }

        }

        private void DisplayAllMoviesWithActor()
        {
            throw new NotImplementedException();
        }

        private void DisplayMoviesByGenre()
        {
            DisplayAllCategories();
            Console.Write("Enter genre ID to see their movies: ");
            var choice = Console.ReadLine();
            var categoryId = int.TryParse(choice, out var id) ? id : -1;
            var movies = Logic.GetMoviesByCategoryId(categoryId);
            foreach (var movie in movies)
            {
                Console.WriteLine($"id Category: {categoryId} - Film: {movie.FilmId} - {movie.Title}");
            }

        }

        private void DisplayActorsByRentalIncome()
        {
            var moviesByRentalIncome = Logic.GetMoviesByRentalIncome();
            foreach (var movie in moviesByRentalIncome)
            {
                Console.WriteLine($"Film: {movie.Title} - Rental Income: {movie.RentalIncome:C}");
            }
        }

        private void DisplayAllCategories()
        {
            var categories = Logic.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryId} ** Category: {category.Name}");
            }
        }
        private void DisplayAllComedyMovies()
        {
            var comedyMovies = Logic.GetAllComedyMovies();
            foreach (var movie in comedyMovies)
            {
                Console.WriteLine($"Title: {movie.Title}");
            }
        }

        private void DisplayAllComedyActors()
        {
            //var comedyMovies = Logic.GetAllComedyMovies();
            //var actors = new HashSet<string>();
            //foreach (var movie in comedyMovies)
            //{
            //    foreach (var actor in movie.FilmActors)
            //    {
            //        actors.Add($"{actor.Actor.FirstName} {actor.Actor.LastName}");
            //    }
            //}
            //foreach (var actor in actors)
            //{
            //    Console.WriteLine($"Actor: {actor}");
            //}
            var comedyMovies = Logic.GetAllComedyMovies();
            var actors = comedyMovies
    .SelectMany(m => m.FilmActors)
    .Select(fa => $"{fa.Actor.FirstName} {fa.Actor.LastName}")
    .Distinct()
    .OrderBy(name => name); // Ordina alfabeticamente

            foreach (var actor in actors)
            {
                Console.WriteLine($"Actor: {actor}");
            }
        }
        private void DisplayStoreByCountry()
        {
            var stores = Logic.GetStoreByCountry();
            foreach (var store in stores)
            {
                Console.WriteLine($"Country: {store.Country} - Store Number: {store.StoreNumber}");
               
            }

        }

        private void DisplayMoviesRentalNumber()
        {
            var movies = Logic.GetMoviesRentalNumber();
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Title} *** {movie.RentalCount} noleggi");

            }

        }


        private void DisplayActorsOrderedByRentalNumber()
        {
            var actors = Logic.GetActorsOrderedByRentalNumber();
            foreach (var actor in actors)
            {
                Console.WriteLine($"Actor: {actor.FirstName} {actor.LastName} - Rental Count: {actor.RentalCount}");
            }
        }

        private void DisplayAllActors()
        {
            var actors = Logic.GetAllActors();
            foreach (var actor in actors)
            {
                Console.WriteLine($"ActorID: {actor.ActorId} ** {actor.FirstName} {actor.LastName} **");
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


        private void DisplayAllMoviesByActor()
        {
            DisplayAllActors();
            Console.Write("Enter Actor ID to see their movies: ");
            var choice = Console.ReadLine();
            var actorId = int.TryParse(choice, out var id) ? id : -1;
            var movies = Logic.GetMoviesByActorId(actorId);
            foreach (var movie in movies)
            {
                Console.WriteLine($"id attore: {actorId} - Film: {movie.FilmId} - {movie.Title}");
            }
        }

    }
}
