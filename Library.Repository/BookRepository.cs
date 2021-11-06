using Library.Core.Models;
using Library.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly string _versionApi;

        public BookRepository(IWebApiExecuter webApiExecuter, string versionApi = "1")
        {
            _webApiExecuter = webApiExecuter;
            _versionApi = versionApi;
        }

        public async Task<IEnumerable<Book>> GetAsync(string filter = null)
        {
            string uri = $"api/v{_versionApi}/book";

            if (!string.IsNullOrEmpty(filter))
                uri += $"?title={filter.Trim()}";

            return await _webApiExecuter.InvokeGet<IEnumerable<Book>>(uri);
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Book>($"api/v{_versionApi}/book/{id}");
        }

        public async Task<Book> CreateAsync(Book book)
        {
            return await _webApiExecuter.InvokePost<Book>($"api/v{_versionApi}/book", book);
        }

        public async Task UpdateAsync(Book book)
        {
            await _webApiExecuter.InvokePut<Book>($"api/v{_versionApi}/book/{book.BookId}", book);
        }

        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete<Book>($"api/v{_versionApi}/book/{id}");
        }
    }
}
