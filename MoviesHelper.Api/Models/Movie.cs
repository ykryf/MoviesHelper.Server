using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHelper.Api.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Rated { get; set; }
    }
}
