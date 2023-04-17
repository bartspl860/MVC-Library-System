using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IBorrowRepository : IDisposable
    {
        public Borrow? GetBorrow(int id);
        public void PutBorrow(Borrow borrowInfo);
        public void DeleteBorrow(int id);
        public IEnumerable<Borrow> GetAllBorrows();
    }
}
