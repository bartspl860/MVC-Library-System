using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class PublishingHouseRepository : IPublishingHouseRepository, IDisposable
    {
        private readonly DbLibraryContext _context;

        private bool _disposed = false;

        public PublishingHouseRepository(DbLibraryContext dbLibraryContext)
        {
            this._context = dbLibraryContext;
        }

        public void DeletePublishingHouse(int id)
        {
            var publishingHouse = GetPublishingHouse(id);
            if (publishingHouse != null)
            {
                _context.PublishingHouses.Remove(publishingHouse);
            }
        }

        public IEnumerable<PublishingHouse> GetAllPublishers()
        {
            return _context.PublishingHouses.ToList();
        }

        public PublishingHouse? GetPublishingHouse(int id)
        {
            return _context.PublishingHouses.Where(r => r.Id == id).FirstOrDefault();
        }

        public void PutPublishingHouse(PublishingHouse publisher)
        {
            _context.PublishingHouses.Add(publisher);
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
