namespace LibraryManagement.API.Models
{
    public class BookCreateDto
    {
        public string? Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
