using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class AuthorRepository : IAuthorRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public AuthorRepository(DbLibraryContext dbLibraryContext)
        {
            this._context = dbLibraryContext;
        }

        public bool IsDuplicated(Author author)
        {
            var authorInDb = _context.Authors
                .Where(a=>a.Name == author.Name && a.Surname == author.Surname)
                .ToList();
            return authorInDb.Any();
        }

        public void DeleteAuthor(int id)
        {
            var author = GetAuthor(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors
                .Include(author => author.WrittenBooks)
                .ToList();
        }

        public Author? GetAuthor(int id)
        {
            return _context.Authors.Where(r => r.Id == id).FirstOrDefault();
        }

        public void AddAuthor(Author author)
        {
            if (!IsDuplicated(author))
            {
                _context.Authors.Add(author);
            }
            
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void UpdateAuthor(Author author)
        {
            _context.Update(author);
        }
    }
}
