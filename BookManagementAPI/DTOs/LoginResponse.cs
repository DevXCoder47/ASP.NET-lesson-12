using BookManagementAPI.Models;

namespace BookManagementAPI.DTOs
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
