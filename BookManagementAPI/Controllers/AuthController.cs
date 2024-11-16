using AutoMapper;
using BookManagementAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
namespace BookManagementAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;
        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #region Post methods
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] UserDTO userDto)
        {
            try
            {
                return Ok(await _service.RegisterUser(_mapper.Map<User>(userDto)));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserDTO userDto)
        {
            try
            {
                return Ok(await _service.LoginUser(userDto.Email, userDto.Password));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
