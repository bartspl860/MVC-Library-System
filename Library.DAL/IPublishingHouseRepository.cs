using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IPublishingHouseRepository : IDisposable
    {
        public PublishingHouse? GetPublishingHouse(int id);
        public void PutPublishingHouse(PublishingHouse publisher);
        public void DeletePublishingHouse(int id);
        public IEnumerable<PublishingHouse> GetAllPublishers();
    }
}
