using CleanRental.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanRental
{
    internal class BusinessLogic
    {

        public CleanRentalContext Context { get; set; }
        public BusinessLogic(CleanRentalContext context)
        {
            Context = context;
        }   


        internal List<Film> GetAllMovies()
        {
            var movies = Context.Films.ToList();

            return movies;
        }

        internal List<Actor> GetAllActors()
        {
           var actors = Context.Actors.ToList();
            return actors;
        }

        internal List<Category> GetAllCategories()
        {
            var movies = Context.Categories.ToList();

            return movies;
        }
        internal List<Film> GetMoviesByActorId(int actorId)
        {
            var movies = Context.Films
                .Where(f => f.FilmActors.Any(fa => fa.ActorId == actorId))
                .ToList();
            return movies;
        }

        internal List<Film> GetAllComedyMovies()
        {
           var comedyMovies = Context.Films
                  .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy"))
                  .Include(f => f.FilmActors)
                  .ThenInclude(fa => fa.Actor)
                  .ToList();
            return comedyMovies;
        }

        internal List<(string Country, int StoreNumber)> GetStoreByCountry()
        {
            return Context.Stores
                .Include(s => s.Address)
                    .ThenInclude(a => a.City)
                    .ThenInclude(c => c.Country)
                .AsEnumerable()
                .GroupBy(s => s.Address.City.Country.Country1)
                .Select(g => (Country: g.Key, StoreNumber: g.Count()))
                .OrderByDescending(x => x.StoreNumber)
                .ToList();
        }

        internal List<(int FilmId, string Title, int RentalCount)> GetMoviesRentalNumber()
        {
            return Context.Films
                .Include(f => f.Inventories)
                    .ThenInclude(i => i.Rentals)
                .AsEnumerable()
                .Select(f => (
                    FilmId: f.FilmId,
                    Title: f.Title,
                    RentalCount: f.Inventories.Sum(i => i.Rentals.Count)
                ))
                .OrderByDescending(x => x.RentalCount)
                .ToList();
        }

        internal List<(int ActorId, string FirstName, string LastName , int RentalCount)> GetActorsOrderedByRentalNumber()
        {
            var actorsByRental = Context.Payments
                .Include(p => p.Rental)
                    .ThenInclude(r => r.Inventory)
                        .ThenInclude(i => i.Film)
                            .ThenInclude(f => f.FilmActors)
                                .ThenInclude(fa => fa.Actor)
                .AsEnumerable()
                .SelectMany(p => p.Rental.Inventory.Film.FilmActors.Select(fa => fa.Actor))
                .GroupBy(a => a.ActorId)
                .Select(g => (
                    ActorId: g.Key,
                    FirstName: g.First().FirstName,
                    LastName: g.First().LastName,
                    RentalCount: g.Count()))
                .OrderByDescending(x => x.RentalCount)
                .ToList();
            return actorsByRental;
        }

        internal List<Film> GetMoviesByCategoryId(object categoryId)
        {
            var movies = Context.Films

                .Where(f => f.FilmCategories.Any(fc => fc.Category.CategoryId == (int)categoryId))
                .ToList();
            return movies;
        }




        internal List<Tuple<Film, decimal>> GetMoviesByRentalIncome()
        {
            var movieOrderByRentaleIncome = Context.Films
                .Include(f => f.Inventories)
                    .ThenInclude(i => i.Rentals)
                    .ThenInclude(r => r.Payment)
                    .Select(f => new Tuple<Film, decimal>(
                        f,
                        f.Inventories.Sum(i => i.Rentals.Sum(r => r.Payment.Amount))
                    )).OrderByDescending(x => x.Item2).ToList();
            


        }
    }
}
