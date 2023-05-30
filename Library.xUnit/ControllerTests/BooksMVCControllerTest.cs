using Library.BLL;
using Library.Model;
using Library.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.xUnit.ControllerTests
{
    public class BooksMVCControllerTest
    {
        [Fact]
        public void IndexViewTest()
        {
            Mock<IBookService> bookServiceMock = new Mock<IBookService>();
            bookServiceMock
                .Setup(x => x.GetBooks())
                .Returns(new List<Book> { new Book() { Title = "Test1" }, new Book() { Title = "Test2" } });

            var homeController = new HomeController(bookServiceMock.Object);

            var controllerResponse = homeController.Index();
            Assert.IsAssignableFrom<ActionResult>(controllerResponse);

            ActionResult? booksViewResult = controllerResponse;
            Assert.NotNull(booksViewResult);

            var booksBag = ((ViewResult)booksViewResult).ViewData["Books"];
            Assert.IsType<List<Book>>(booksBag);

            int numberOfBooks = ((List<Book>)booksBag).Count();
            Assert.Equal(2, numberOfBooks);
        }

        [Fact]
        public void CountBooksTest()
        {
            Mock<IBookService> bookServiceMock = new Mock<IBookService>();
            bookServiceMock
                .Setup(x => x.CountBooks())
                .Returns(3);

            var homeController = new HomeController(bookServiceMock.Object);

            var controllerResponse = homeController.NumberOfBooks();
            Assert.IsAssignableFrom<ViewResult>(controllerResponse);

            ViewResult? countViewResult = controllerResponse as ViewResult;
            Assert.NotNull(countViewResult);

            var booksCount = countViewResult.ViewData["Count"];
            Assert.Equal(3, booksCount);
        }
    }
}
