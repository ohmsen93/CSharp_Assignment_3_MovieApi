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


        /// <summary>
        /// Get all characters with associated movies.
        /// </summary>
        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _dbContext.Characters.Include(x => x.Movies).ToListAsync();
        }

        /// <summary>
        /// Get a character by Id with associated movies.
        /// </summary>
        /// <param name="id">The Id of the character to retrieve.</param>
        /// <returns>The retrieved character.</returns>
        public async Task<Character> GetCharacterById(int id)
        {
            
                var Character = await _dbContext.Characters.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == id);
            
                return Character;
            
                
            
        }

        /// <summary>
        /// Get characters by a list of Ids with associated movies.
        /// </summary>
        /// <param name="ids">The list of character Ids to retrieve.</param>
        /// <returns>The retrieved characters.</returns>
        public async Task<List<Character>> GetCharactersByIds(List<int> ids)
        {
            return await _dbContext.Characters
                .Include(x => x.Movies)
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        /// <summary>
        /// Add a new character to the database.
        /// </summary>
        /// <param name="character">The character to add.</param>
        /// <returns>The added character.</returns>
        public async Task<Character> PostCharacter(Character Character)
        {
            await _dbContext.Characters.AddAsync(Character);
            await _dbContext.SaveChangesAsync();
            return Character;
        }

        /// <summary>
        /// Delete a character by Id from the database.
        /// </summary>
        /// <param name="id">The Id of the character to delete.</param>
        /// <returns>The deleted character.</returns>
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

        /// <summary>
        /// Update an existing character by Id in the database.
        /// </summary>
        /// <param name="character">The updated character.</param>
        /// <returns>The updated character.</returns>
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
