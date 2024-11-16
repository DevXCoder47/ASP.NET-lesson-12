using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        public BookService(DataContext context)
        {
            _context = context;
        }
        #region Create one
        public async Task<Book> CreateBook(Book book, CancellationToken cancellationToken = default)
        {
            book.PublicationDate = DateTime.Now;
            var entityEntry = _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity;
        }
        #endregion
        #region Delete one by id
        public async Task DeleteBook(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _context.Books.FindAsync([id], cancellationToken);
            if (book == null)
                throw new ArgumentException("Book not found");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        #region Get one by id
        public async Task<Book> GetBookById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Books.FindAsync([id], cancellationToken) ?? throw new ArgumentException("Book not found");
        }
        #endregion
        #region Get all
        public async Task<IEnumerable<Book>> GetBooks(BookFilter filter, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();
            if (!string.IsNullOrEmpty(filter.AuthorFirstName))
                query = query.Where(b => b.Author != null && b.Author.FirstName.Contains(filter.AuthorFirstName));
            if(!string.IsNullOrEmpty(filter.Genre))
                query = query.Where(b => b.Genre == filter.Genre);
            if(filter.PublicationDate.HasValue)
                query = query.Where(b => b.PublicationDate == filter.PublicationDate);
            query = query.Skip(skip).Take(take);
            return await query.ToArrayAsync(cancellationToken);
        }
        #endregion
        #region Update one by id
        public async Task<Book> UpdateBook(Guid id, Book book, CancellationToken cancellationToken = default)
        {
            if (!_context.Books.Any(b => b.Id == id))
                throw new ArgumentException("Book not found");
            book.Id = id;
            book.PublicationDate = DateTime.Now;
            var entityEntry = _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity;
        }
        #endregion
    }
}
