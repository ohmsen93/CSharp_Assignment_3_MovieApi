namespace CSharp_Assignment_3_MovieApi.Models
{
    public class FranchiseEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        public int FranchiseId { get; set; }
        public string Franchise { get; set; }
    }
}
