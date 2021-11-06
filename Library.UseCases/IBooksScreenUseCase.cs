using Library.Core.Models;

namespace Library.UseCases
{
    public interface IBooksScreenUseCase
    {
        Task<IEnumerable<Book>> ViewBooksByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> ViewBooksFilteredAsync(int authorId, string filter);
        Task UpdateBook(Book book);
        Task<Book> ViewBookById(int bookId);
        Task<Book> AddBook(Book book);
        Task RemoveBook(int bookId);
    }
}