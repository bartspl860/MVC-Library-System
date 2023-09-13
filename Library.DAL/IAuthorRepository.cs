using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IAuthorRepository : IDisposable
    {
        public Author? GetAuthor(int id);
        public void AddAuthor(Author author);
        public void DeleteAuthor(int id);
        public IEnumerable<Author> GetAllAuthors();
        public void UpdateAuthor(Author author);
        public bool IsDuplicated(Author author);
    }
}
