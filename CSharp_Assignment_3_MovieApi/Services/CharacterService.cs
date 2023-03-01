using CSharp_Assignment_3_MovieApi.DatabaseContext;
using CSharp_Assignment_3_MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly MovieDbContext _dbContext;

        public CharacterService(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _dbContext.Characters.Include(x => x.Movies).ToListAsync();
        }

        public async Task<Character> GetCharacterById(int id)
        {
            
                var Character = await _dbContext.Characters.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            
                return Character;
            
                
            
        }

        public async Task<List<Character>> GetCharactersByIds(List<int> ids)
        {
            return await _dbContext.Characters
                .Include(x => x.Movies)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<Character> PostCharacter(Character Character)
        {
            await _dbContext.Characters.AddAsync(Character);
            await _dbContext.SaveChangesAsync();
            return Character;
        }

        public async Task<Character> DeleteCharacter(int id)
        {
            var Character = await _dbContext.Characters.FindAsync(id);
            if (Character == null)
            {
                throw new Exception($"Character with Id: {id} not found.");
            }
            _dbContext.Characters.Remove(Character);
            await _dbContext.SaveChangesAsync();

            return Character;
        }

        public async Task<Character> PatchCharacter(Character Character)
        {
            //find Character entity
            var updatedCharacter = await _dbContext.Characters.FindAsync(Character.Id);
            if (updatedCharacter == null)
            {
                throw new Exception($"Character with Id: {Character.Id} not found.");
            }

            //patch Character entity
            updatedCharacter.FirstName = Character.FirstName;
            updatedCharacter.LastName = Character.LastName;
            updatedCharacter.Gender = Character.Gender;
            updatedCharacter.Alias = Character.Alias;
            updatedCharacter.Picture = Character.Picture;
            updatedCharacter.Movies = Character.Movies;
            await _dbContext.SaveChangesAsync();
            return updatedCharacter;
        }
    }
}
