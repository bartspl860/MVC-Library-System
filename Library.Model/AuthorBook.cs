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
        public ICollection<Author>? Author { get; set; }
        public ICollection<Book>? Book { get; set; }
    }
}
