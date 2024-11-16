using AutoMapper;
using BookManagementAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
namespace BookManagementAPI.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #region Get methods
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(await _service.GetAuthors(skip, take)));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById([FromRoute] Guid id)
        {
            try
            {
                return Ok(_mapper.Map<AuthorDTO>(await _service.GetAuthorById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Post methods
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] CreateAuthorDTO createAuthorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthorDto);
                var createdAuthor = await _service.CreateAuthor(author);
                return Created($"authors/{createdAuthor.Id}", _mapper.Map<AuthorDTO>(createdAuthor));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Put methods
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthorById([FromRoute]Guid id, [FromBody] CreateAuthorDTO createAuthorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(createAuthorDto);
                var updatedAuthor = await _service.UpdateAuthor(id, author);
                return Created($"authors/{updatedAuthor.Id}", _mapper.Map<AuthorDTO>(updatedAuthor));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Patch methods
        [Authorize]
        [HttpPatch("book/{bookId}/to/author/{authorId}")]
        public async Task<ActionResult> AssignBookToAuthor([FromRoute] Guid bookId, [FromRoute] Guid authorId)
        {
            try
            {
                await _service.AssignBookToAuthor(bookId, authorId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Delete methods
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorById([FromRoute] Guid id)
        {
            try
            {
                await _service.DeleteAuthor(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
