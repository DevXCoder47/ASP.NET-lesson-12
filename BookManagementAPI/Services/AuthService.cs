using BookManagementAPI.DTOs;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
namespace BookManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;
        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #region Login 
        public async Task<LoginResponse> LoginUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
                throw new ArgumentException("Wrong email or password");
            return new LoginResponse() { User = user, Token = GenerateToken() };
        }
        #endregion
        #region Register
        public async Task<RegisterResponse> RegisterUser(User user)
        {
            if (!Regex.IsMatch(user.Email, @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,6}$"))
                throw new ArgumentException("Email isn't valid");
            if(_context.Users.Any(u =>  u.Email == user.Email))
                throw new ArgumentException("Email is already used");
            if (!Regex.IsMatch(user.Password, @"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$"))
                throw new ArgumentException("Password isn't valid");
            var newUserEntry = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new RegisterResponse() { Id = newUserEntry.Entity.Id, Email = newUserEntry.Entity.Email };
        }
        #endregion
        #region Generate token
        private string GenerateToken()
        {
            var claims = new List<Claim>
            {
            new(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration.GetSection("AppSettings:ExpireTime").Value!)),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion
    }
}
