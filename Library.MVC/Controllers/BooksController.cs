using Microsoft.AspNetCore.Mvc;
using Library.Model;
using Library.BLL;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Library.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult GetBooks([FromQuery(Name ="Title")] string? title = null)
        {
            IEnumerable<Book> books;
            if(title == null)
                books = _bookService.GetBooks();
            else
                books = _bookService.GetBooksFilter(title);          

            return Ok(books);
        }

        [HttpGet("Count")]
        public IActionResult CountBooks()
        {
            return Ok(_bookService.CountBooks());
        }

        [HttpGet("Borrowed/{title}")]
        public IActionResult GetBorrowedBooksByTitle(string title)
        {
            return Ok(_bookService.GetBorrowsByTitle(title));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.FindBook(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
        
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), /*Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
        public IActionResult PutBook(int id, Book book)
        {
            var existingBook = _bookService.FindBook(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Authors = book.Authors;
            existingBook.PublishingHouse = book.PublishingHouse;
            existingBook.PublishingHouseId = book.PublishingHouseId;

            _bookService.UpdateBook(existingBook);

            return Ok();
        }      
        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, /*Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
        public ActionResult<Book> PostBook(Book book)
        {
            _bookService.AddBook(book);

            return CreatedAtAction("AddBook", new { id = book.Id }, book);
        }
        
        // DELETE: api/Books/5
        [HttpDelete("{id}"), /*Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = _bookService.GetBooks().Where(b=>b.Id == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            _bookService.DeleteBook(book.Id);

            return Ok();
        }
        
    }
}
