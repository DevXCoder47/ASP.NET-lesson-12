using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
namespace BookManagementAPI.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterUser(User user);
        Task<LoginResponse> LoginUser(string email, string password);
    }
}
