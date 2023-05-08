using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public BookRepository(DbLibraryContext dbLibraryContext)
        {
            this._context = dbLibraryContext;
        }

        public void DeleteBook(int id)
        {
            var book = GetBook(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books
                .Include(books => books.Authors)
                .Include(books => books.PublishingHouse)
                .ToList();
        }

        public Book? GetBook(int id)
        {
            return _context.Books
                .Where(r => r.Id == id)
                .Include(books => books.Authors)
                .Include(books => books.PublishingHouse)
                .FirstOrDefault();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddBook(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
