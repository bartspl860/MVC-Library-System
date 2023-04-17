using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IReaderRepository : IDisposable
    {
        public Reader? GetReader(int id);
        public void PutReader(Reader reader);
        public void DeleteReader(int id);
        public IEnumerable<Reader> GetAllReaders();
    }
}
