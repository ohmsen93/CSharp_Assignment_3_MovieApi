using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> GetFranchiseById(int id);
        Task<Franchise> PostFranchise(Franchise franchise);
        Task<Franchise> PatchFranchise(Franchise franchise);
        Task<Franchise> PatchFranchiseMovies(Franchise franchise);

        Task<Franchise> DeleteFranchise(int id);
    }
}
