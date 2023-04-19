using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.BLL;
using Library.DAL;
using Microsoft.Identity.Client;

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
        public IActionResult GetBooks([FromQuery(Name ="Title")] string? title)
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
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existingBook = _bookService.FindBook(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title;
            existingBook.Authors = book.Authors;
            existingBook.PublishingHouse = book.PublishingHouse;
            existingBook.PublishingHouseId = book.PublishingHouseId;

            if (!_bookService.UpdateBook(existingBook)){
                return NotFound();
            }

            return Ok();
        }      
        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            _bookService.AddBook(book);

            return CreatedAtAction("AddBook", new { id = book.Id }, book);
        }
        /*
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            var book = _unitOfWork.BooksRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.BooksRepository.DeleteBook(id);

            return NoContent();
        }
        */
    }
}
