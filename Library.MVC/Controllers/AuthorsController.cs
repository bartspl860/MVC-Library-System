using Library.BLL;
using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Library.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        [HttpGet("{id}/Books")]
        public IActionResult GetAuthorBooks(int id) {

            var author = _authorService.FindAuthor(id).WrittenBooks;

            if(author == null)
            {
                NotFound(id);
            }
          
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAuthors([FromQuery(Name = "Key")] string? key = null)
        {
            IEnumerable<Author> authors;

            if(key == null)
            {
                authors = _authorService.GetAuthors();

                var data = new List<object>();

                foreach(var author in authors)
                {
                    data.Add(new { author, author.WrittenBooks });
                }


                return Ok(data);
            }
            {
                key = key.ToUpper();
                authors =
                    _authorService.GetAuthors()
                    .Where(a =>
                        a.Name.ToUpper().Contains(key) ||
                        a.Surname.ToUpper().Contains(key)
                    );

                var data = new List<object>();

                foreach (var author in authors)
                {
                    data.Add(new { author, author.WrittenBooks });
                }

                return Ok(data);
            }            
        }

        [HttpPost]
        public void AddAuthor(Author author)
        {
            _authorService.AddAuthor(author);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            if(_authorService.FindAuthor(id) == null)
                return NotFound();

            _authorService.DeleteAuthor(id);

            return Ok();
        }

        [HttpPut]
        public void UpdateAuthor(Author author)
        {
            _authorService.UpdateAuthor(author);
        }
    }
}
