using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class BorrowRepository : IBorrowRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public BorrowRepository(DbLibraryContext dbLibraryContext)
        {
            this._context = dbLibraryContext;
        }

        public void DeleteBorrow(int id)
        {
            var borrow = GetBorrow(id);
            if (borrow != null)
            {
                _context.Borrows.Remove(borrow);
            }
        }

        public IEnumerable<Borrow> GetAllBorrows()
        {
            return _context.Borrows.ToList();
        }

        public Borrow? GetBorrow(int id)
        {
            return _context.Borrows.Where(r => r.Id == id).FirstOrDefault();
        }

        public void PutBorrow(Borrow borrowInfo)
        {
            _context.Borrows.Add(borrowInfo);
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
