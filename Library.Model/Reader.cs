using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    [Table("Readers")]
    public class Reader : Person
    {
        [Required]
        public long LibraryCardNumber { get; set; }
        [Required, Column(TypeName="Date")]
        public DateTime LibraryCardExpirationDate { get; set; }
        public ICollection<Book> BorrowedBooks { get; set;}
    }
}
