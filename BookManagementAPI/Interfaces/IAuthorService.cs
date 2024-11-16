using BookManagementAPI.Models;

namespace BookManagementAPI.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors(int skip, int take, CancellationToken cancellationToken = default);
        Task<Author> GetAuthorById(Guid id, CancellationToken cancellationToken = default);
        Task<Author> CreateAuthor(Author author, CancellationToken cancellationToken = default);
        Task<Author> UpdateAuthor(Guid id, Author author, CancellationToken cancellationToken = default);
        Task AssignBookToAuthor(Guid bookId, Guid authorId, CancellationToken cancellationToken = default);
        Task DeleteAuthor(Guid id, CancellationToken cancellationToken = default);
    }
}
