using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BookManagementAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;
        public AuthorService(DataContext context)
        {
            _context = context;
        }
        #region Assign one to one by ids
        public async Task AssignBookToAuthor(Guid bookId, Guid authorId, CancellationToken cancellationToken = default)
        {
            var author = await _context.Authors.FindAsync([authorId], cancellationToken);
            if (author == null)
                throw new ArgumentException("Author not found");
            var book = await _context.Books.FindAsync([bookId], cancellationToken);
            if (book == null)
                throw new ArgumentException("Book not found");
            book.Author = author;
            book.AuthorId = authorId;
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        #region Create one
        public async Task<Author> CreateAuthor(Author author, CancellationToken cancellationToken = default)
        {
            var entityEntry = _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity;
        }
        #endregion
        #region Delete one by id
        public async Task DeleteAuthor(Guid id, CancellationToken cancellationToken = default)
        {
            var author = await _context.Authors.FindAsync([id], cancellationToken);
            if(author == null)
                throw new ArgumentException("Author not found");
            _context.Authors.Remove(author);
            var relatedBooks = await _context.Books.Where(b => b.AuthorId == id).ToListAsync(cancellationToken);
            if (relatedBooks != null)
            {
                foreach (var item in relatedBooks)
                {
                    item.Author = null;
                    item.AuthorId = null;
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
        #region Get one by id
        public async Task<Author> GetAuthorById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Authors.FindAsync([id], cancellationToken) ?? throw new ArgumentException("Author not found");
        }
        #endregion
        #region Get all
        public async Task<IEnumerable<Author>> GetAuthors(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Author>()
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
        #endregion
        #region Update one
        public async Task<Author> UpdateAuthor(Guid id, Author author, CancellationToken cancellationToken = default)
        {
            if (!_context.Authors.Any(a => a.Id == id))
                throw new ArgumentException("Author not found");
            author.Id = id;
            var entityEntry = _context.Authors.Update(author);
            await _context.SaveChangesAsync(cancellationToken);
            return entityEntry.Entity;
        }
        #endregion
    }
}
