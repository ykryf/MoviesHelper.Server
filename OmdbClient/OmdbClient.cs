
using MoviesHelper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OmdbClient
{
    public class OmdbClient : IMovieDbClient
    {
        private readonly string baseUrl = "https://www.omdbapi.com/";

        public OmdbClient(string apiKey)
        {
            // Build base url
            baseUrl = "https://www.omdbapi.com?apikey=" + apiKey;
        }

        public async Task<Movie> GetMovieByImdbIdAsync(string imdbId)
        {
            using (var client = new HttpClient())
            {
                string url = baseUrl + $"&i={imdbId}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    var movie = await GetMovieFromResponseAsync(response);
                    return movie?.ConvertToMovie();
                }
                catch (Exception e)
                {
                    // TODO: Log
                    return null;
                }
            }
        }

        public async Task<Movie> GetMovieByTitleAsync(string title)
        {
            using (var client = new HttpClient())
            {
                string url = baseUrl + $"&t={title}";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    var movie = await GetMovieFromResponseAsync(response);
                    return movie?.ConvertToMovie();
                }
                catch (Exception e)
                {
                    // TODO: Log
                    return null;
                }
            }
        }

        public async Task<ICollection<Movie>> SearchAsync(string searchTag, int? year = null)
        {
            using (var client = new HttpClient())
            {
                string url = baseUrl + $"&s={searchTag}";
                if (year != null)
                {
                    url += $"&y={year}";
                }
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    OmdbSearchResults results = await GetSearchResultsFromResponseAsync(response);
                    return results.ConvertToMovies();
                }
                catch (Exception e)
                {
                    // TODO: Log
                    return null;
                }
            }
        }


        private async Task<OmdbMovie> GetMovieFromResponseAsync(HttpResponseMessage response)
        {
            OmdbMovie movie = null;
            string json = await response.Content.ReadAsStringAsync();
            try
            {
                movie = JsonSerializer.Deserialize<OmdbMovie>(json);
            }
            catch (Exception e)
            {

                throw;
            }

            return movie;
        }

        private async Task<OmdbSearchResults> GetSearchResultsFromResponseAsync(HttpResponseMessage response)
        {
            OmdbSearchResults results = null;
            string json = await response.Content.ReadAsStringAsync();
            try
            {
                results = JsonSerializer.Deserialize<OmdbSearchResults>(json);
            }
            catch (Exception e)
            {

                throw;
            }
            return results;
        }
    }
}
