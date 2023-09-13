using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MinLength(60)]
        [MaxLength(60), JsonIgnore] /* BCrypt fixed hash length */
        public string HashedPassword { get; set; }
        [Required, JsonIgnore]
        public string PasswordSalt { get; set; }
    }
}
