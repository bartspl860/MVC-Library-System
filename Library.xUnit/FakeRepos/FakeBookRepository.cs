using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.DAL;
using Library.Model;

namespace Library.xUnit.FakeRepos
{
    public class FakeBookRepository : IBookRepository
    {
        private readonly List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void DeleteBook(int id)
        {
            _books.RemoveAt(id);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetBook(int id)
        {
            return _books.Where(b => b.Id == id).FirstOrDefault();
        }

        public bool IsDuplicated(Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            var book_to_update = _books.Where(b => b.Id == book.Id).FirstOrDefault();
            if (book_to_update == null)
                return;

            book_to_update.Authors = book.Authors;
            book_to_update.PublishingHouse = book.PublishingHouse;
            book_to_update.PublishingHouseId = book.PublishingHouseId;
            book_to_update.Title = book.Title;
        }
    }
}
