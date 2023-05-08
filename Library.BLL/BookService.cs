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

        public int CountBooks()
        {
            return _unitOfWork.BooksRepository
                .GetAllBooks()
                .Count();
        }

        public object GetBorrowsByTitle(string title)
        {
            var books = this.GetBooksFilter(title);
            var borrowedBooks = _unitOfWork
                .ReadersRepository
                .GetBorrowedBooks()
                .Where(b => b.Title == title)
                .ToList();

            var bookInfo = new
            {
                Title = title,
                BorrowedBooks = borrowedBooks,
                FreeBooks = books.Except(borrowedBooks).ToList()
            };

            return bookInfo;
        }

        public bool UpdateBook(Book book)
        {
            try
            {
                _unitOfWork.BooksRepository
                    .UpdateBook(book);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;            
        }

        public void AddBook(Book book)
        {
            _unitOfWork.BooksRepository
                .AddBook(book);
            _unitOfWork.Save();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _unitOfWork.BooksRepository
                .GetAllBooks();
        }

        public IEnumerable<Book> GetBooksFilter(string title)
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