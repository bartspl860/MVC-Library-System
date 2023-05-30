using Library.Model;

namespace Library.BLL
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooksFilter(string title);
        IEnumerable<Book> GetBooks();
        object GetBorrowsByTitle(string title);
        Book? FindBook(int id);
        void AddBook(Book book);
        bool UpdateBook(Book book);
        int CountBooks();
    }
}
