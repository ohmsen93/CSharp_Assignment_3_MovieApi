using CSharp_Assignment_3_MovieApi.DatabaseContext;
using CSharp_Assignment_3_MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _dbContext;

        public MovieService(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByIds(List<int> ids)
        {
            return await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<Movie> PostMovie(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<Movie> PatchMovie(Movie movie)
        {
            //find Movie entity
            var updatedMovie = await _dbContext.Movies.FindAsync(movie.Id);
            if (updatedMovie == null)
            {
                throw new Exception($"Movie with Id: {movie.Id} not found.");
            }

            //patch Movie entity
            updatedMovie.Title = movie.Title;
            updatedMovie.Genre = movie.Genre;
            updatedMovie.ReleaseYear = movie.ReleaseYear;
            updatedMovie.Director = movie.Director;
            updatedMovie.Picture = movie.Picture;
            updatedMovie.Trailer = movie.Trailer;
            updatedMovie.FranchiseId = movie.FranchiseId;
            updatedMovie.Characters = movie.Characters;
            await _dbContext.SaveChangesAsync();
            return updatedMovie;
        }
        public async Task<Movie> DeleteMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new Exception($"Movie with Id: {id} not found.");
            }
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return movie;
        }
    }
}
