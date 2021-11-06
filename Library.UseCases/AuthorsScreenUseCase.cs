using Library.Core.Models;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UseCases
{
    public class AuthorsScreenUseCase : IAuthorsScreenUseCase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsScreenUseCase(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IEnumerable<Author>> ViewAuthorsAsync()
        {
            return await _authorRepository.GetAsync();
        }
    }
}
