using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IBookRepository : IDisposable
    {
        public Book? GetBook(int id);
        public void UpdateBook(Book book);
        public void DeleteBook(int id);
        public IEnumerable<Book> GetAllBooks();
    }
}
