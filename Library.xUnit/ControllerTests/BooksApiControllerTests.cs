using Library.BLL;
using Library.Model;
using Library.MVC.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Library.xUnit.ControllerTests
{
    public class BooksApiControllerTests
    {
        [Fact]
        public void GetBooks()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.GetBooks())
                .Returns(new List<Book> { new Book() { Title = "Test1" }, new Book() { Title = "Test2" } });

            var controller = new BooksController(mockService.Object);

            var result = controller.GetBooks();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<Book>>(objectResult.Value);
            Assert.Equal(2, items.Count());
        }
        [Fact]
        public void GetBooksFilter()
        {
            var title = "Hi";
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.GetBooksFilter(title))
                .Returns(new List<Book> { new Book() { Title = "Hi Mom!" } });

            var controller = new BooksController(mockService.Object);

            var result = controller.GetBooks(title);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<Book>>(objectResult.Value);
            Assert.Single(items);
        }
        [Fact]
        public void GetBookCount()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.CountBooks())
                .Returns(3);

            var controller = new BooksController(mockService.Object);

            var result = controller.CountBooks();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var count = Assert.IsAssignableFrom<int>(objectResult.Value);

            Assert.Equal(3, count);
        }

        [Fact]
        public void PutBook()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.FindBook(1))
                .Returns(new Book());         

            var controller = new BooksController(mockService.Object);

            var result = controller.PutBook(1, new Book());

            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public void PutBook_NotFound()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.FindBook(1))
                .Returns((Book?)null);

            var controller = new BooksController(mockService.Object);

            var result = controller.PutBook(1, new Book());

            Assert.IsType<NotFoundResult>(result);
        }                
    }
}
