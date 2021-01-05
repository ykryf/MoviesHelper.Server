using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHelper.Core
{
    public class Movie
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public int? Runtime { get; set; }
        public string Rated { get; set; }
        public List<string> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Writer> Writers { get; set; }
        public List<Actor> Actors { get; set; }
        public string Plot { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Countries { get; set; }
        public string Awards { get; set; }
        public string PosterUrl { get; set; }
        public List<Rating> Ratings { get; set; }
        public int? Metascore { get; set; }
        public float? ImdbRating { get; set; }
        public int? ImdbVotes { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Dvd { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string WebsiteUrl { get; set; }

    }

}
