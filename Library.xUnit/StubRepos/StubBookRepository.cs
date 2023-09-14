using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.xUnit.StubRepos
{
    public class StubBookRepository : IBookRepository
    {
        private List<Book> stubBooksList = new List<Book>();
        private Book stubBook = new Book();

        public void AddBook(Book book)
        {
            return;
        }

        public void DeleteBook(int id)
        {
            return;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return stubBooksList;
        }

        public Book? GetBook(int id)
        {
            return stubBook;
        }

        public bool IsDuplicated(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            return;
        }
    }
}
