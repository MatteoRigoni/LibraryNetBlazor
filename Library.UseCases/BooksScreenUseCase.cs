using Library.Core.Models;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UseCases
{
    public class BooksScreenUseCase : IBooksScreenUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public BooksScreenUseCase(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<Book>> ViewBooksByAuthorAsync(int authorId)
        {
            return await _authorRepository.GetAuthorBooksAsync(authorId);
        }

        public async Task<IEnumerable<Book>> ViewBooksFilteredAsync(int authorId, string filter)
        {
            return await _authorRepository.GetAuthorBooksAsync(authorId, filter);
        }

        public async Task<Book> ViewBookById(int bookId)
        {
            return await _bookRepository.GetAsync(bookId);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task<Book> AddBook(Book book)
        {
            return await _bookRepository.CreateAsync(book);
        }

        public async Task RemoveBook(int bookId)
        {
            await _bookRepository.DeleteAsync(bookId);
        }
    }
}
