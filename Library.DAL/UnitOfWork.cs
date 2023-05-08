using Library.Model;

namespace Library.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbLibraryContext _dbLibraryContext;
        private IBookRepository? _booksRepository;
        private IPublishingHouseRepository? _publishingHousesRepository;
        private IAuthorRepository? _authorsRepository;
        private IReaderRepository? _readersRepository;
        private IBorrowRepository? _borrowsRepository;

        public UnitOfWork(DbLibraryContext dbLibraryContext)
        {
            _dbLibraryContext = dbLibraryContext;
        }

        public IBookRepository BooksRepository
        {
            get => this._booksRepository;
            set => this._booksRepository = value;
        }

        public IPublishingHouseRepository PublishingHousesRepository
        {
            get => this._publishingHousesRepository;
            set => this._publishingHousesRepository = value;
        }

        public IAuthorRepository AuthorsRepository
        {
            get => this._authorsRepository; 
            set => this._authorsRepository = value;
        }

        public IReaderRepository ReadersRepository
        {
            get => this._readersRepository; 
            set => this._readersRepository = value;
        }

        public IBorrowRepository BorrowsRepository
        {
            get => _borrowsRepository;
            set => this._borrowsRepository = value;
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
