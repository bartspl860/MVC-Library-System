using Library.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        public HomeController(IBookService bookService) 
        {
            this._bookService = bookService;
        }

        // GET: HomeController
        public ViewResult Index()
        {
            ViewBag.Books = _bookService.GetBooks();
            return View();
        }

        public ViewResult NumberOfBooks()
        {
            ViewBag.Count = _bookService.CountBooks();
            return View();
        }
    }
}
