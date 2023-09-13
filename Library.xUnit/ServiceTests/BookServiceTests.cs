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

            unitofwork.BooksRepository = new FakeBookRepository();
            unitofwork.BooksRepository.AddBook(book);

            var bookservice = new BookService(unitofwork);

            Assert.Contains<Book>(book, bookservice.GetBooks());
        }
        [Fact]
        public void AddBookTest_ShouldThrow_InvalidBook()
        {
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
}