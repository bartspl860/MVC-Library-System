using Library.Model;

namespace Library.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbLibraryContext _dbLibraryContext;
        private IBookRepository? booksRepository;
        private IPublishingHouseRepository? publishingHousesRepository;
        private IAuthorRepository? authorsRepository;
        private IReaderRepository? readersRepository;
        private IBorrowRepository? borrowsRepository;

        public UnitOfWork(DbLibraryContext dbLibraryContext)
        {
            _dbLibraryContext = dbLibraryContext;
        }

        public IBookRepository BooksRepository
        {
            get {
                if (booksRepository == null)
                    booksRepository = new BookRepository(_dbLibraryContext);
                return this.booksRepository;
            } 
        }

        public IPublishingHouseRepository PublishingHousesRepository
        {
            get
            {
                if (publishingHousesRepository == null)
                    publishingHousesRepository = new PublishingHouseRepository(_dbLibraryContext);
                return publishingHousesRepository;
            }
        }

        public IAuthorRepository AuthorsRepository
        {
            get
            {
                if (authorsRepository == null)
                    authorsRepository = new AuthorRepository(_dbLibraryContext);
                return authorsRepository;
            }
        }

        public IReaderRepository ReadersRepository
        {
            get
            {
                if (readersRepository == null)
                    readersRepository = new ReaderRepository(_dbLibraryContext);
                return readersRepository;
            }
        }

        public IBorrowRepository BorrowsRepository
        {
            get
            {
                if (borrowsRepository == null)
                    borrowsRepository = new BorrowRepository(_dbLibraryContext);
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
