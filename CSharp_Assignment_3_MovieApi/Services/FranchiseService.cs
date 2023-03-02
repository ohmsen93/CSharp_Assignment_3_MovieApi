using CSharp_Assignment_3_MovieApi.DatabaseContext;
using CSharp_Assignment_3_MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MovieDbContext _dbContext;

        public FranchiseService(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all franchises with their associated movies.
        /// </summary>
        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _dbContext.Franchises.Include(x => x.Movies).ToListAsync();
        }

        /// <summary>
        /// Get a franchise by its ID with associated movies.
        /// </summary>
        /// <param name="id">The ID of the franchise to get.</param>
        /// <returns>The franchise with the specified ID.</returns>
        public async Task<Franchise> GetFranchiseById(int id)
        {

            var franchise = await _dbContext.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);

            return franchise;

        }

        /// <summary>
        /// Get a franchise by its ID with associated movies and characters.
        /// </summary>
        /// <param name="id">The ID of the franchise to get.</param>
        /// <returns>The franchise with the specified ID.</returns>
        public async Task<Franchise> GetAllIdFranchiseCharacters(int id)
        {
            var franchise = await _dbContext.Franchises.Include(x => x.Movies)
            .ThenInclude(m => m.Characters)
            .FirstOrDefaultAsync(x => x.Id == id);
            return franchise;
        }

        /// <summary>
        /// Add a new franchise to the database.
        /// </summary>
        /// <param name="franchise">The franchise to add.</param>
        /// <returns>The added franchise.</returns>
        public async Task<Franchise> PostFranchise(Franchise franchise)
        {
            await _dbContext.Franchises.AddAsync(franchise);
            await _dbContext.SaveChangesAsync();
            return franchise;
        }

        /// <summary>
        /// Delete a franchise from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to delete.</param>
        /// <returns>The deleted franchise.</returns>
        public async Task<Franchise> DeleteFranchise(int id)
        {
            var franchise = await _dbContext.Franchises.FindAsync(id);
            if (franchise == null)
            {
                throw new Exception($"Franchise with Id: {id} not found.");
            }
            _dbContext.Franchises.Remove(franchise);
            await _dbContext.SaveChangesAsync();

            return franchise;
        }

        /// <summary>
        /// Update an existing franchise by its ID in the database.
        /// </summary>
        /// <param name="franchise">The franchise with updated information.</param>
        /// <returns>The updated franchise.</returns>
        public async Task<Franchise> PatchFranchise(Franchise franchise)
        {
            var updatedFranchise = await _dbContext.Franchises.FindAsync(franchise.Id);
            if (updatedFranchise == null)
            {
                throw new Exception($"Franchise with Id: {franchise.Id} not found.");
            }

            updatedFranchise.Name = franchise.Name;
            updatedFranchise.Description = franchise.Description;
            await _dbContext.SaveChangesAsync();
            return updatedFranchise;
        }

        /// <summary>
        /// Update the movies associated with a franchise.
        /// </summary>
        /// <param name="franchiseId">The ID of the franchise to update.</param>
        /// <param name="movieIds">The IDs of the movies to associate with the franchise.</param>
        /// <returns>The updated franchise.</returns>
        public async Task<Franchise> PatchFranchiseMovies(int franchiseId, IEnumerable<int> movieIds)
        {
            var franchise = await _dbContext.Franchises.FindAsync(franchiseId);

            if (franchise == null)
            {
                throw new Exception($"Franchise with Id: {franchiseId} not found.");
            }

            var moviesToUpdate = await _dbContext.Movies.Where(m => movieIds.Contains(m.Id)).ToListAsync();

            foreach (var movie in moviesToUpdate)
            {
                movie.FranchiseId = franchise.Id;
            }

            await _dbContext.SaveChangesAsync();

            return franchise;
        }


    }
}
