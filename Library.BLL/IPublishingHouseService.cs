using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL
{
    public interface IPublishingHouseService
    {
        PublishingHouse? FindPublisher(int id);
    }
}
