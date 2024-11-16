using BookManagementAPI.Models;
using BookManagementAPI;
using System.Threading;
namespace BookManagementAPI.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks(BookFilter filter, int skip, int take, CancellationToken cancellationToken = default);
        Task<Book> GetBookById(Guid id, CancellationToken cancellationToken = default);
        Task<Book> CreateBook(Book book, CancellationToken cancellationToken = default);
        Task<Book> UpdateBook(Guid id, Book book, CancellationToken cancellationToken = default);
        Task DeleteBook(Guid id, CancellationToken cancellationToken = default);
    }
}
