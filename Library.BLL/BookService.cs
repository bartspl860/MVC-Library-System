using Library.DAL;
using Library.Model;
using System.Linq;

namespace Library.BLL
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public Book? FindBook(int id)
        {
            return _unitOfWork.BooksRepository
                .GetBook(id);
        }

        public void AddBook(Book book)
        {
            _unitOfWork.BooksRepository.AddBook(book);
            _unitOfWork.Save();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _unitOfWork.BooksRepository
                .GetAllBooks();
        }

        public IEnumerable<Book> GetBooks(string title)
        {
            return _unitOfWork.BooksRepository
                .GetAllBooks()
                .Where(b => b.Title
                .ToUpper()
                .Contains(title
                .ToUpper())
                );
        }
    }
}