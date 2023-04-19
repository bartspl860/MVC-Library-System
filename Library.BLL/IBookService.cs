using Library.Model;

namespace Library.BLL
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooks(string title);
        Book FindBook(int id);
        void AddBook(Book book);
    }
}
