using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Assignment_3_MovieApi.Models.Dto
{
    public class CharacterEditDto
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
 
        public string Alias { get; set; }

        public string Gender { get; set; }
     
        public string Picture { get; set; }

    }
}
