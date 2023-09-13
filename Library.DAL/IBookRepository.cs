using Library.Model;

namespace Library.DAL
{
    public interface IBookRepository : IDisposable
    {
        Book? GetBook(int id);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        IEnumerable<Book> GetAllBooks();
        void AddBook(Book book);
        bool IsDuplicated(Book book);
    }
}
