using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL
{    
    public class ReaderRepository : IReaderRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public ReaderRepository(DbLibraryContext dbLibraryContext) {
            this._context = dbLibraryContext;
        }

        public Reader? GetReader(int id)
        {
            return _context.Readers.Where(r=> r.Id == id).FirstOrDefault();
        }

        public void PutReader(Reader reader)
        {
            _context.Readers.Add(reader);
            _context.SaveChanges();
        }

        public void DeleteReader(int id)
        {
            var reader = GetReader(id);
            if(reader != null)
            {
                _context.Readers.Remove(reader);
            }
        }

        public IEnumerable<Reader> GetAllReaders() {
            return _context.Readers.ToList();
        }

        public IQueryable<Book> GetBorrowedBooks()
        {
            return _context.Readers.Include(r => r.BorrowedBooks).SelectMany(r => r.BorrowedBooks);
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