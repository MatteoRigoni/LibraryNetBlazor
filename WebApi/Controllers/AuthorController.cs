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
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly LibraryContext _libraryContext;

        public AuthorController(ILogger<AuthorController> logger, LibraryContext libraryContext)
        {
            _logger = logger;
            _libraryContext = libraryContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return Ok(_libraryContext.Authors.Include(a => a.Books));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _libraryContext.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBooks(int id, [FromQuery] BookQueryFilter bookQueryFilter)
        {
            IQueryable<Book> books = _libraryContext.Books;
            books = books.Where(b => b.AuthorId == id);

            if (bookQueryFilter != null)
            {
                if (bookQueryFilter.Id.HasValue)
                    books = books.Where(b => b.BookId == bookQueryFilter.Id);
                if (!String.IsNullOrEmpty(bookQueryFilter.Title))
                    books = books.Where(b => b.Title.Contains(bookQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(await books.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Author>> InsertAuthor([FromBody] Author author)
        {
            // default child
            author.Books = new List<Book>();
            author.Books.Add(new Book() { Title = "Presentation", Price = 0, PublishDate = DateTime.Now });

            _libraryContext.Authors.Add(author);

            await _libraryContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> InsertAuthor(int id, [FromBody] Author author)
        {
            if (id != author.AuthorId)
                return BadRequest("Invalid id");

            _libraryContext.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _libraryContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (_libraryContext.Authors.Find(id) == null)
                    return NotFound();

                throw;
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var authorDb = await _libraryContext.Authors.FindAsync(id);
            if (authorDb == null) return NotFound();

            _libraryContext.Authors.Remove(authorDb);
            await _libraryContext.SaveChangesAsync(); // check if childs is deleted

            return NoContent();
        }
    }
}
