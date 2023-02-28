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

        

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _dbContext.Franchises.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Franchise> GetFranchiseById(int id)
        {
            
                var franchise = await _dbContext.Franchises.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            
                return franchise;
            
                
            
        }

        public async Task<Franchise> PostFranchise(Franchise franchise)
        {
            await _dbContext.Franchises.AddAsync(franchise);
            await _dbContext.SaveChangesAsync();
            return franchise;
        }
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

        public async Task<Franchise> PatchFranchise(Franchise franchise)
        {
            //find Franchise entity
            var updatedFranchise = await _dbContext.Franchises.FindAsync(franchise.Id);
            if (updatedFranchise == null)
            {
                throw new Exception($"Franchise with Id: {franchise.Id} not found.");
            }

            //patch franchise entity
            updatedFranchise.Name = franchise.Name;
            updatedFranchise.Description = franchise.Description;
            await _dbContext.SaveChangesAsync();
            return updatedFranchise;
        }

        public async Task<Franchise> PatchFranchiseMovies(Franchise franchise)
        {
            //find Franchise entity
            var updatedFranchise = await _dbContext.Franchises.FindAsync(franchise.Id);
            if (updatedFranchise == null)
            {
                throw new Exception($"Franchise with Id: {franchise.Id} not found.");
            }

            //patch franchise entity

            updatedFranchise.Movies = franchise.Movies;
            await _dbContext.SaveChangesAsync();
            return updatedFranchise;
        }
    }
}
