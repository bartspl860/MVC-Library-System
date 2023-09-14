using Library.BLL;
using Library.Model;
using Library.MVC.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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
        public void GetBook_Exists()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.FindBook(It.IsAny<int>()))
                .Returns(new Book() { Title = "Test1" });

            var controller = new BooksController(mockService.Object);

            var viewResult = controller.GetBook(1);

            var objectResult = Assert.IsType<OkObjectResult>(viewResult);
            var foundBook = Assert.IsType<Book>(objectResult.Value);
            
            Assert.NotNull(foundBook);
        }
        [Fact]
        public void GetBook_NotExists()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.FindBook(It.IsAny<int>()));

            var controller = new BooksController(mockService.Object);

            var viewResult = controller.GetBook(1);

            var objectResult = Assert.IsType<NotFoundResult>(viewResult);
        }
        [Fact]
        public void PostBook_SingleBook()
        {
            Book book = new Book
            {
                Title = "Title"
            };

            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.AddBook(book));

            var controller = new BooksController(mockService.Object);

            var apiResult = controller.PostBook(book);

            Assert.IsType<CreatedAtActionResult>(apiResult);
        }      
        [Fact]
        public void PostBook_BadRequest()
        {
            Book book = new Book();

            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.AddBook(It.IsAny<Book>()))
                .Throws<Exception>();

            var controller = new BooksController(mockService.Object);

            Assert.Throws<Exception>(() => controller.PostBook(book));
        }

        [Fact]
        public void DeleteBook_Found()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.DeleteBook(2));

            mockService
                .Setup(service => service.GetBooks())
                .Returns(new List<Book> 
                {
                    new Book 
                    {
                    Id = 1,
                    Title = "Title1",
                    Authors = { },
                    PublishingHouse = { }
                    },
                    new Book
                    {
                    Id = 2,
                    Title = "Title2",
                    Authors = { },
                    PublishingHouse = { }
                    }
                });

            var controller = new BooksController(mockService.Object);
            var resultAsync = controller.DeleteBook(1);
            var result = Assert.IsType<OkResult>(resultAsync);
            Assert.Equal(result.StatusCode, 200);
        }
        [Fact]
        public async void DeleteBook_NotFound()
        {
            var mockService = new Mock<IBookService>();
            mockService
                .Setup(service => service.DeleteBook(3));

            mockService
                .Setup(service => service.GetBooks())
                .Returns(new List<Book>
                {
                    new Book
                    {
                    Id = 1,
                    Title = "Title1",
                    Authors = { },
                    PublishingHouse = { }
                    },
                    new Book
                    {
                    Id = 2,
                    Title = "Title2",
                    Authors = { },
                    PublishingHouse = { }
                    }
                });

            var controller = new BooksController(mockService.Object);

            var aResult = controller.DeleteBook(55);

            var result = Assert.IsType<NotFoundResult>(aResult);
            Assert.Equal(404, result.StatusCode);
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
