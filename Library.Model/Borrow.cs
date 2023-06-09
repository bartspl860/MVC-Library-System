﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    [Table("Borrows")]
    public class Borrow
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Reader Reader { get; set; }
        public int ReaderId { get; set; }
        [Required]
        public Book Book { get; set; }
        public int BookId { get; set; }
        [Required]
        public DateTime Date {  get; set; }
    }
}
