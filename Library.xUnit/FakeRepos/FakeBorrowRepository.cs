using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.xUnit.FakeRepos
{
    public class FakeBorrowRepository : IBorrowRepository
    {
        private readonly List<Borrow> _borrows = new List<Borrow>();

        public void DeleteBorrow(int id)
        {
            if (_borrows.Count <= id || _borrows[id] == null)
                throw new ArgumentOutOfRangeException();

            _borrows.RemoveAt(id);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Borrow> GetAllBorrows()
        {
            return _borrows;
        }

        public Borrow? GetBorrow(int id)
        {
            return _borrows.Where(b => b.Id == id).FirstOrDefault();
        }

        public void PutBorrow(Borrow borrowInfo)
        {
            _borrows.Add(borrowInfo);
        }
    }
}
