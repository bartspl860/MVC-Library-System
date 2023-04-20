using Library.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Test.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBookService _bookService;

        public TestController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("Count")]
        public IActionResult CountBooks()
        {
            return Ok(_bookService.CountBooks());
        }
    }
}
