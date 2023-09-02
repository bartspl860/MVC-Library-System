using Library.Model;

namespace Library.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBookRepository? _booksRepository;
        private IPublishingHouseRepository? _publishingHousesRepository;
        private IAuthorRepository? _authorsRepository;
        private IReaderRepository? _readersRepository;
        private IBorrowRepository? _borrowsRepository;
        private IUserRepository? _usersRepository;

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

        public IUserRepository UsersRepository
        {
            get => this._usersRepository;
            set => this._usersRepository = value;
        }
    }
}
