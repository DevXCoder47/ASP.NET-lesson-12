using BookManagementAPI.Models;

namespace BookManagementAPI.DTOs
{
    public class CreateBookDTO
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}
