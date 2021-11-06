using Library.Core.Models;
using Library.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly string _versionApi;

        public AuthorRepository(IWebApiExecuter webApiExecuter, string versionApi = "1")
        {
            _webApiExecuter = webApiExecuter;
            _versionApi = versionApi;
        }

        public async Task<IEnumerable<Author>> GetAsync()
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Author>>($"api/v{_versionApi}/author");
        }

        public async Task<IEnumerable<Author>> GetAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Author>>($"api/v{_versionApi}/author/{id}");
        }

        public async Task<IEnumerable<Book>> GetAuthorBooksAsync(int id, string filter = null)
        {
            string uri = $"api/v{_versionApi}/author/{id}/books";

            if (!string.IsNullOrEmpty(filter))
                uri += $"?title={filter.Trim()}";

            return await _webApiExecuter.InvokeGet<IEnumerable<Book>>(uri);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            return await _webApiExecuter.InvokePost<Author>($"api/v{_versionApi}/author", author);
        }

        public async Task UpdateAsync(Author author)
        {
            await _webApiExecuter.InvokePut<Author>($"api/v{_versionApi}/author/{author.AuthorId}", author);
        }

        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete<Author>($"api/v{_versionApi}/author/{id}");
        }
    }
}
