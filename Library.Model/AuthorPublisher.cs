using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class BookPublisher
    {
        public Book Book { get; set; }
        public PublishingHouse Publisher { get; set; }
    }
}
