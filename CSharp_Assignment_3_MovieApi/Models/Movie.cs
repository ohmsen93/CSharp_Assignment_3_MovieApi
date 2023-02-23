using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CSharp_Assignment_3_MovieApi.Models;

public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Genre { get; set; }

    [Required]
    public DateTime ReleaseYear { get; set; }

    [Required]
    [StringLength(50)]
    public string Director { get; set; }

    [Required]
    [StringLength(100)]
    public string Picture { get; set; }

    [Required]
    [StringLength(100)]
    public string Trailer { get; set; }

    public int FranchiseId { get; set; }

    [ForeignKey("FranchiseId")]
    public Franchise Franchise { get; set; }

    public ICollection<Character> Characters { get; set; }


}