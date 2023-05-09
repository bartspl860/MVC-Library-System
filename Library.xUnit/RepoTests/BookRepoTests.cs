using Microsoft.EntityFrameworkCore;
using Library.Model;
using Microsoft.EntityFrameworkCore.InMemory;
using Library.DAL;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Library.xUnit.DummyRepos;

namespace Library.xUnit.RepoTests
{    
    public class BookRepoTests
    {
        private DbLibraryContext GetDbLibraryContext(string dbName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            // Create db context options specifying in memory database
            var options = new DbContextOptionsBuilder<DbLibraryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .UseInternalServiceProvider(serviceProvider)
            .Options;

            //Use this to instantiate the db context
            return new DbLibraryContext(options);
        }

        [Fact]
        public void AddBookTest()
        {
            var testDb = GetDbLibraryContext("BookDb");

            var bookrepo = new BookRepository(testDb);

            bookrepo.AddBook(new Book() { Title = "Test" });

            Assert.Single(testDb.Books);
        }

        [Fact]
        public void DummyBookRepoTest()
        {
            Action dummyActCreate = () => new DummyBookRepository(null);

            Assert.Throws<ArgumentNullException>(dummyActCreate);
        }

    }
}
