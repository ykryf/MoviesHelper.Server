using MoviesHelper.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OmdbClient
{
    public class OmdbMapper
    {
        public static Movie ConvertToMovie(OmdbMovie omdbMovie)
        {
            return new Movie
            {
                Title = omdbMovie.Title,
                Plot = omdbMovie.Plot,
                Awards = omdbMovie.Awards,
                PosterUrl = omdbMovie.Poster,
                ImdbId = omdbMovie.ImdbId,
                Type = omdbMovie.Type,
                Dvd = omdbMovie.Dvd,
                BoxOffice = omdbMovie.BoxOffice,
                Production = omdbMovie.Production,
                WebsiteUrl = omdbMovie.Website,
                Rated = omdbMovie.Rated,
                Year = GetYear(omdbMovie.Year),
                Actors = GetPersons<Actor>(omdbMovie.Actors),
                Writers = GetPersons<Writer>(omdbMovie.Writer),
                Directors = GetPersons<Director>(omdbMovie.Director),
                Genres = GetStrings(omdbMovie.Genre),
                Countries = GetStrings(omdbMovie.Country),
                Languages = GetStrings(omdbMovie.Language),
                Runtime = GetRuntime(omdbMovie.Runtime),
                ImdbRating = GetImdbRating(omdbMovie.ImdbRating),
                ImdbVotes = GetImdbVotes(omdbMovie.ImdbVotes),
                Metascore = GetMetascore(omdbMovie.Metascore),
                ReleasedAt = GetReleasedAt(omdbMovie.Released),
                Ratings = GetRatings(omdbMovie.Ratings),
            };
        }

        public static Movie ConvertToMovie(OmdbSearchResult result)
        {
            return new Movie
            {
                Title = result.Title,
                Year = GetYear(result.Year),
                ImdbId = result.ImdbId,
                Type = result.Type,
                PosterUrl = result.Poster
            };
        }

        private static List<Rating> GetRatings(List<OmdbMovieRating> ratings)
        {
            List<Rating> result = new List<Rating>();
            CultureInfo cultureInfo = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            foreach (OmdbMovieRating rating in ratings)
            {
                Rating resultRating = new Rating { Source = rating.Source };

                if (rating.Value?.Contains('%') == true)
                {
                    float value;
                    if (float.TryParse(rating.Value.Replace("%", ""), NumberStyles.Float, cultureInfo.NumberFormat, out value))
                    {
                        resultRating.PercentageValue = (int)Math.Round(value);
                    }
                }
                else if (rating.Value?.Contains('/') == true)
                {
                    float divident;
                    float divisor;
                    string[] elements = rating.Value.Split('/');
                    if (elements.Length == 2 && float.TryParse(elements[0], NumberStyles.Float, cultureInfo.NumberFormat, out divident) && float.TryParse(elements[1], NumberStyles.Float, cultureInfo.NumberFormat, out divisor))
                    {
                        resultRating.PercentageValue = (int)Math.Round(divident / divisor * 100);
                    }
                }
                if (resultRating.PercentageValue != null)
                {
                    result.Add(resultRating);
                }
            }
            return result;
        }

        private static DateTime? GetReleasedAt(string released)
        {
            string[] dateElements = released?.Split(' ');
            if (dateElements?.Length == 3 && int.TryParse(dateElements[0], out int day) &&
                int.TryParse(dateElements[2], out int year) &&
                Enum.TryParse(typeof(Months), dateElements[1], out object month))
            {
                return new DateTime(year, (int)(Months)month, day);
            }
            return null;
        }

        private static int? GetMetascore(string metascore)
        {
            if (int.TryParse(metascore, out int result))
            {
                return result;
            }
            return null;
        }

        private static int? GetImdbVotes(string imdbVotes)
        {
            if (int.TryParse(imdbVotes?.Replace(",", ""), out int result))
            {
                return result;
            }
            return null;
        }

        private static float? GetImdbRating(string imdbRating)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            if (float.TryParse(imdbRating, NumberStyles.Float, cultureInfo.NumberFormat, out float result))  // TODO: Check for globalization: Does it work with dot in all cases?
            {
                return result;
            }
            return null;
        }

        private static int? GetRuntime(string runtime)
        {
            if (runtime?.Split(' ').Length > 1 && int.TryParse(runtime.Split(' ').First(), out int result))
            {
                return result;
            }
            return null;
        }

        private static List<string> GetStrings(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.ToLower().Trim() == "n/a")
            {
                return new List<string>();
            }
            return input.Split(',').ToList();
        }
        private static int? GetYear(string year)
        {
            if (int.TryParse(year, out int result))
            {
                return result;
            }
            return null;
        }



        private static List<T> GetPersons<T>(string input) where T : Person, new()
        {
            if (string.IsNullOrWhiteSpace(input) || input.ToLower().Trim() == "n/a")
            {
                return new List<T>();
            }
            return input.Split(',').Select(n => new T { FullName = n }).ToList();
        }
    }

    public enum Months
    {
        Jan = 1,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec
    }
}
