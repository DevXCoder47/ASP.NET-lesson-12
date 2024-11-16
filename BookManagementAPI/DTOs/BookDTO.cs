using BookManagementAPI.Models;

namespace BookManagementAPI.DTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid? AuthorId { get; set; }
        public double Price { get; set; }
    }
}
