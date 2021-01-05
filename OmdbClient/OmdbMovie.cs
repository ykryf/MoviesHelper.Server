using MoviesHelper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OmdbClient
{
    public class OmdbMovie : OmdbBaseMovie
    {
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public List<OmdbMovieRating> Ratings { get; set; }
        public string Metascore { get; set; }
        [JsonPropertyName("imdbRating")]
        public string ImdbRating { get; set; }
        [JsonPropertyName("imdbVotes")]
        public string ImdbVotes { get; set; }
        [JsonPropertyName("DVD")]
        public string Dvd { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }


        public Movie ConvertToMovie()
        {
            return OmdbMapper.ConvertToMovie(this);
        }
    }

    public class OmdbMovieRating
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }

}
