using Library.Model;

namespace Library.BLL
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooksFilter(string title);
        Book FindBook(int id);
        void AddBook(Book book);
        bool UpdateBook(Book book);
        int CountBooks();
    }
}
