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

        /// <summary>
        /// Gets all movies with associated characters and franchise.
        /// </summary>
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a movie by id with associated characters and franchise.
        /// </summary>
        /// <param name="id">The id of the movie to retrieve.</param>
        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        /// <summary>
        /// Gets multiple movies by id with associated characters and franchise.
        /// </summary>
        /// <param name="ids">The ids of the movies to retrieve.</param>
        public async Task<IEnumerable<Movie>> GetMoviesByIds(List<int> ids)
        {
            return await _dbContext.Movies
                .Include(x => x.Characters)
                .Include(x => x.Franchise)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="movie">The movie object to create.</param>
        public async Task<Movie> PostMovie(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        /// <summary>
        /// Updates a movie by id.
        /// </summary>
        /// <param name="movie">The updated movie object.</param>
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

        /// <summary>
        /// Updates the characters associated with a movie.
        /// </summary>
        /// <param name="id">The id of the movie to update.</param>
        /// <param name="characterIds">The ids of the characters to associate with the movie.</param>
        public async Task<Movie> PatchMovieCharacters(int id, List<int> characterIds)
        {
            var movie = await _dbContext.Movies
                .Include(m => m.Characters)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var characters = new List<Character>();

            foreach (var characterId in characterIds)
            {
                Character charObj = await _dbContext.Characters.FindAsync(characterId);
                characters.Add(charObj);
            }
            movie.Characters = characters;
            await _dbContext.SaveChangesAsync();

            return movie;
        }

        /// <summary>
        /// Deletes a movie by id.
        /// </summary>
        /// <param name="id">The id of the movie to delete.</param>
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
