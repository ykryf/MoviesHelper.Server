using MoviesHelper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OmdbClient
{
    public class OmdbSearchResults
    {
        [JsonPropertyName("Search")]
        public List<OmdbSearchResult> Results { get; set; }

        public ICollection<Movie> ConvertToMovies()
        {
            List<Movie> movies = new List<Movie>();
            foreach (OmdbSearchResult result in Results)
            {
                movies.Add(OmdbMapper.ConvertToMovie(result));
            }
            return movies;
        }
    }

    public class OmdbSearchResult : OmdbBaseMovie
    {

    }
}
