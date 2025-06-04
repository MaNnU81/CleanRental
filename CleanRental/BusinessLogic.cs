using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanRental.model;

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
    }
}
