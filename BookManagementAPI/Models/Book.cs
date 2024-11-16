namespace BookManagementAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid? AuthorId { get; set; }
        public Author? Author { get; set; }
        public double Price { get; set; }
    }
}
