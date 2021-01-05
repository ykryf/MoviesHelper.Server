using MoviesHelper.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesHelper.Core
{
    public interface IMovieDbClient
    {
        Task<Movie> GetMovieByImdbIdAsync(string imdbId);
        Task<Movie> GetMovieByTitleAsync(string title);
        Task<ICollection<Movie>> SearchAsync(string searchTag, int? year = null);
    }
}