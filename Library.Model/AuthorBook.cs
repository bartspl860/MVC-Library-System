using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class AuthorBook
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Author>? Author { get; set; }
        public ICollection<Book>? Book { get; set; }
    }
}
