using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Assignment_3_MovieApi.Models
{
    public class FranchiseDto
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Movies { get; set; }
    }
}
