using Library.Model;

namespace Library.DAL
{    
    public class ReaderRepository
    {
        private readonly DbLibraryContext _context;
        public ReaderRepository(DbLibraryContext dbLibraryContext) {
            this._context = dbLibraryContext;
        }

        public Reader? GetReader(int id)
        {
            return _context.Readers.Where(r=> r.Id == id).FirstOrDefault();
        }

        public void PutReader(Reader reader)
        {
            _context.Readers.Add(reader);
            _context.SaveChanges();
        }

        public void DeleteReader(int id)
        {
            var reader = GetReader(id);
            if(reader != null)
            {
                _context.Readers.Remove(reader);
            }
        }

        public IEnumerable<Reader> GetAllReaders() {
            return _context.Readers.ToList();
        }
    }
}