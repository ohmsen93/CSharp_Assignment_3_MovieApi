using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);

        Task<IEnumerable<Movie>> GetMoviesByIds(List<int> ids);
        Task<Movie> PostMovie(Movie movie);
        Task<Movie> PatchMovie(Movie movie);

        Task<Movie> DeleteMovie(int id);
    }
}
