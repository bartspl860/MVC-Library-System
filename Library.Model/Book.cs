using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Library.Model
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string? Title { get; set; }
        [Required]
        public ICollection<Author>? Authors { get; set; }
        [Required]
        public PublishingHouse? PublishingHouse { get; set; }
        public int PublishingHouseId { get; set; }
    }
}