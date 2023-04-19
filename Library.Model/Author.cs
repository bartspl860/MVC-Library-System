using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Model
{
    [Table("Authors")]
    public class Author : Person
    {
        [JsonIgnore]
        public ICollection<Book>? WrittenBooks { get; set; }
    }
}
