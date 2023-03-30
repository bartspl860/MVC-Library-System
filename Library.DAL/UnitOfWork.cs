using Library.Model;

namespace Library.DAL
{
    public class UnitOfWork : IDisposable
    {
        private DbLibraryContext _dbLibraryContext = new DbLibraryContext();
        private Repository<Book>? booksRepository;
        private Repository<PublishingHouse>? publishingHousesRepository;
        private Repository<Author>? authorsRepository;
        private Repository<Reader>? readersRepository;
        private Repository<Borrow>? borrowsRepository;

        public Repository<Book> BooksRepository { 
            get {
                if (booksRepository == null)
                    booksRepository = new Repository<Book>(_dbLibraryContext);
                return booksRepository;
            } 
        }

        public Repository<PublishingHouse> PublishingHousesRepository
        {
            get
            {
                if (publishingHousesRepository == null)
                    publishingHousesRepository = new Repository<PublishingHouse>(_dbLibraryContext);
                return publishingHousesRepository;
            }
        }

        public Repository<Author> AuthorsRepository
        {
            get
            {
                if (authorsRepository == null)
                    authorsRepository = new Repository<Author>(_dbLibraryContext);
                return authorsRepository;
            }
        }

        public Repository<Reader> ReadersRepository
        {
            get
            {
                if (readersRepository == null)
                    readersRepository = new Repository<Reader>(_dbLibraryContext);
                return readersRepository;
            }
        }

        public Repository<Borrow> BorrowsRepository
        {
            get
            {
                if (borrowsRepository == null)
                    borrowsRepository = new Repository<Borrow>(_dbLibraryContext);
                return borrowsRepository;
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
