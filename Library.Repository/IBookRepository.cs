using Library.Core.Models;

namespace Library.Repository
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetAsync(string filter = null);
        Task<Book> GetAsync(int id);
        Task UpdateAsync(Book book);
    }
}