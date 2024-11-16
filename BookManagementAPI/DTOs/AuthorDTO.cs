using BookManagementAPI.Models;

namespace BookManagementAPI.DTOs
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
