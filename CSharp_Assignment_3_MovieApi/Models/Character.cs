using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharp_Assignment_3_MovieApi.Models;

public class Character
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Alias { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Gender { get; set; }

    [Required]
    [StringLength(100)]
    public string Picture { get; set; }

    public ICollection<Movie> Movies { get; set; }

}