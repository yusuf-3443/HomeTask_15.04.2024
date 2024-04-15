namespace Domain.DTOs.BookDTO;

public class AddBookDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublishedYear { get; set; }
    public string Genre { get; set; }
}