using Library.Model;

namespace Library.DAL
{
    public class UnitOfWork : IDisposable
    {
        private DbLibraryContext _dbLibraryContext = new DbLibraryContext();
        private Repository<Book>? booksRepository;
        private Repository<PublishingHouse>? publishingHouseRepository;

        public Repository<Book> BooksRepository { 
            get {
                if (booksRepository == null)
                    booksRepository = new Repository<Book>(_dbLibraryContext);
                return booksRepository;
            } 
        }

        public Repository<PublishingHouse> PublishingHouseRepository
        {
            get
            {
                if (publishingHouseRepository == null)
                    publishingHouseRepository = new Repository<PublishingHouse>(_dbLibraryContext);
                return publishingHouseRepository;
            }
        }

        public void Save()
        {
            _dbLibraryContext.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
              _dbLibraryContext.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
