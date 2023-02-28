using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Services
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<Character> PostCharacter(Character Character);
        Task<Character> PatchCharacter(Character Character);

        Task<Character> DeleteCharacter(int id);
    }
}
