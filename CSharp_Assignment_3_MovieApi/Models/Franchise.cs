using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp_Assignment_3_MovieApi.Models;

public class Franchise
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }


    [StringLength(255)]
    public string Description { get; set; }

    public ICollection<Movie> Movies { get; set; }

}