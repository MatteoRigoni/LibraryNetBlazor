using Library.Core.Models;
using Library.Infrastructure;
using Library.WebApi.Filters;
using Library.WebApi.Models.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [CustomTokenAuthFilterAttribute]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly LibraryContext _libraryContext;

        public BookController(ILogger<BookController> logger, LibraryContext libraryContext)
        {
            _logger = logger;
            _libraryContext = libraryContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] BookQueryFilter bookQueryFilter)
        {
            IQueryable<Book> books = _libraryContext.Books;

            if (bookQueryFilter != null)
            {
                if (bookQueryFilter.Id.HasValue)
                    books = books.Where(b => b.BookId == bookQueryFilter.Id);
                if (!String.IsNullOrEmpty(bookQueryFilter.Title))
                    books = books.Where(b => b.Title.Contains(bookQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(await books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _libraryContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> InsertBook([FromBody] Book book)
        {
            var author = await _libraryContext.Authors.FindAsync(book.AuthorId);
            if (author == null)
                return NotFound();

            _libraryContext.Books.Add(book);
            await _libraryContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.AuthorId)
                return BadRequest("Invalid id");

            _libraryContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _libraryContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if ((await _libraryContext.Authors.FindAsync(book.AuthorId)) == null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteBook(int id)
        {
            var bookDb = await _libraryContext.Books.FindAsync(id);
            if (bookDb == null) return NotFound();

            _libraryContext.Books.Remove(bookDb);
            await _libraryContext.SaveChangesAsync(); 

            return NoContent();
        }
    }
}