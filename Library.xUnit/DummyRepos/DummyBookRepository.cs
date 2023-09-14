using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.xUnit.DummyRepos
{
    public class DummyBookRepository : IBookRepository
    {
        private readonly DbLibraryContext _context;
        public DummyBookRepository(DbLibraryContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public void AddBook(Book book)
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public void DeleteBook(int id)
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public void Dispose()
        {
            if (_context == null) throw new InvalidOperationException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            if (_context == null) throw new InvalidOperationException();
            throw new NotImplementedException();
        }

        public Book? GetBook(int id)
        {
            if (_context == null) throw new InvalidOperationException();
            throw new NotImplementedException();
        }

        public bool IsDuplicated(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            if (_context == null) throw new InvalidOperationException();
        }
    }
}
