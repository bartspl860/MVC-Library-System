using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    [Table("Readers")]
    public class Reader : Person
    {
        [Required]
        public long LibraryCardNumber { get; set; }
        [Required, Column(TypeName="Date")]
        public DateTime LibraryCardExpirationDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Book> BorrowedBooks { get; set;}
    }
}
