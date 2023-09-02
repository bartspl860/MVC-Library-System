using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public UserRepository(DbLibraryContext dbLibraryContext)
        {
            this._context = dbLibraryContext;
        }

        public void CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User? user = GetUser(id);
            if(user != null)
            {
                _context.Users.Remove(user);
            }
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
        }

        public User? GetUser(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
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
    }
}
