using Library.Core.Models;

namespace Library.Repository
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAsync(Author author);
        Task DeleteAsync(int id);
        Task<IEnumerable<Author>> GetAsync();
        Task<IEnumerable<Author>> GetAsync(int id);
        Task<IEnumerable<Book>> GetAuthorBooksAsync(int id, string filter = null);
        Task UpdateAsync(Author author);
    }
}