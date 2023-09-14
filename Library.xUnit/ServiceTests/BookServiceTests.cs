using Library.BLL;
using Library.DAL;
using Library.Model;
using Library.xUnit.FakeRepos;
using Moq;

namespace Library.xUnit.RepoTests
{
    public class BookServiceTests
    {
        [Fact]
        public void AddBookTest_ShouldPass_ValidBook()
        {
            var unitofwork = new UnitOfWork();

            var book = new Book()
            {
                Id = 1,
                Title = "Test"
            };
            
            var bookrepomock = new Mock<IBookRepository>();

            bookrepomock.Setup(repo => repo.AddBook(book));
            bookrepomock.Setup(repo => repo.GetBook(1)).Returns(book);

            unitofwork.BooksRepository = bookrepomock.Object;

            var bookservice = new BookService(unitofwork);

            bookservice.AddBook(book);
            
            Assert.Equal(book, bookservice.FindBook(1));
        }
        [Fact]
        public void AddBookTest_ShouldThrow_InvalidBook()
        {
            var unitofwork = new UnitOfWork();

            var book = new Book()
            {
                Id = 1,
                Title = "Test"
            };

            var bookrepomock = new Mock<IBookRepository>();

            bookrepomock.Setup(repo => repo.AddBook(book)).Throws(new Exception());

            unitofwork.BooksRepository = bookrepomock.Object;

            var bookservice = new BookService(unitofwork);            

            Assert.Throws<Exception>(() => bookservice.AddBook(book));
        }

        [Fact]
        public void DeleteBookTest_ShouldPass()
        {
            var unitofwork = new UnitOfWork();

            var bookmockrepo = new Mock<IBookRepository>();

            bookmockrepo.Setup(repo => repo.DeleteBook(3));
            bookmockrepo.Setup(repo => repo.GetBook(3)).Returns((Book)null);

            unitofwork.BooksRepository = bookmockrepo.Object;

            var bookService = new BookService(unitofwork);

            bookService.DeleteBook(3);

            Assert.Null(bookService.FindBook(3));
        }

        [Fact]
        public void DeleteBookTest_ShouldFail()
        {
            var unitofwork = new UnitOfWork();

            var bookmockrepo = new Mock<IBookRepository>();

            bookmockrepo.Setup(repo => repo.DeleteBook(3)).Throws(new Exception());

            unitofwork.BooksRepository = bookmockrepo.Object;

            var bookService = new BookService(unitofwork);

            Assert.Throws<Exception>(() => bookService.DeleteBook(3));
        }

        [Fact]
        public void FindBookTest_ShouldPass_Exists()
        {
            var unitofwork = new UnitOfWork();

            var book = new Book()
            {
                Id = 1,
                Title = "Test"
            };

            unitofwork.BooksRepository = new FakeBookRepository();
            unitofwork.BooksRepository.AddBook(book);

            var bookservice = new BookService(unitofwork);

            var found_book = bookservice.FindBook(1);

            Assert.Equal(found_book, book);
        }
        [Fact]
        public void FindBookTest_ShouldPass_NotExists()
        {
        }

        [Fact]
        public void CountBooksTest_ShouldPass_NotEmpty()
        {
            var unitofwork = new UnitOfWork();

            var book = new Book()
            {
                Id = 1,
                Title = "Test"
            };
            var book2 = new Book()
            {
                Id = 2,
                Title = "Test2"
            };

            var mockbookrepo = new Mock<IBookRepository>();

            mockbookrepo.Setup(x => x.GetAllBooks()).Returns(new List<Book>() { book, book2 });

            unitofwork.BooksRepository = mockbookrepo.Object;

            var bookservice = new BookService(unitofwork);

            Assert.Equal(2, bookservice.CountBooks());
        }
        [Fact]
        public void CountBooksTest_ShouldPass_Empty()
        {
            var unitofwork = new UnitOfWork();

            var mockbookrepo = new Mock<IBookRepository>();

            mockbookrepo.Setup(x => x.GetAllBooks()).Returns(new List<Book>() { });

            unitofwork.BooksRepository = mockbookrepo.Object;

            var bookservice = new BookService(unitofwork);

            Assert.Equal(0, bookservice.CountBooks());
        }
        [Fact]
        public void UpdateBook_ShouldPass()
        {            
            var book = new Book
            {
                Id = 2,
                Title = "Test"              
            };

            var mockbookrepo = new Mock<IBookRepository>();
            mockbookrepo.Setup(repo => repo.UpdateBook(book));

            var unitofwork = new UnitOfWork();

            unitofwork.BooksRepository = mockbookrepo.Object;

            var bookservice = new BookService(unitofwork);

            var result = bookservice.UpdateBook(book);

            Assert.True(result);
        }

        [Fact]
        public void UpdateBook_ShouldBeFalse()
        {
            var book = new Book
            {
                Id = 2,
                Title = "Test"
            };

            var mockbookrepo = new Mock<IBookRepository>();
            mockbookrepo.Setup(repo => repo.UpdateBook(book)).Throws(new Exception());

            var unitofwork = new UnitOfWork();

            unitofwork.BooksRepository = mockbookrepo.Object;

            var bookservice = new BookService(unitofwork);

            var result = bookservice.UpdateBook(book);

            Assert.False(result);
        }
    }    
}