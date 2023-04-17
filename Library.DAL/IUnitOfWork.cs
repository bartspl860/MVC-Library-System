using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IUnitOfWork : IDisposable
    {        
        IBookRepository BooksRepository { get; }
        IPublishingHouseRepository PublishingHousesRepository { get; }
        IAuthorRepository AuthorsRepository { get; }
        IReaderRepository ReadersRepository { get; }
        IBorrowRepository BorrowsRepository { get; }
        void Save();
    }
}
