using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;
        public BookController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #region Get methods
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks([FromQuery] string? authorFirstName = null, [FromQuery] string? genre = null, [FromQuery] DateTime? publicationDate = null, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var filter = new BookFilter()
                {
                    AuthorFirstName = authorFirstName,
                    Genre = genre,
                    PublicationDate = publicationDate
                };
                return Ok(_mapper.Map<IEnumerable<BookDTO>>(await _service.GetBooks(filter, skip, take)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById([FromRoute] Guid id)
        {
            try
            {
                return Ok(_mapper.Map<BookDTO>(await _service.GetBookById(id)));
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
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] CreateBookDTO createBookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(createBookDto);
                var createdBook = await _service.CreateBook(book);
                return Created($"books/{createdBook.Id}", _mapper.Map<BookDTO>(createdBook));
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
        public async Task<ActionResult<BookDTO>> UpdateBookById([FromRoute] Guid id, [FromBody] CreateBookDTO createBookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(createBookDto);
                var updatedBook = await _service.UpdateBook(id, book);
                return Created($"books/{updatedBook.Id}", _mapper.Map<BookDTO>(updatedBook));
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
        public async Task<ActionResult> DeleteBookById([FromRoute] Guid id)
        {
            try
            {
                await _service.DeleteBook(id);
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
