using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int? Pages { get; set; }
        public Writer? Writer { get; set; }
        public int WriterId { get; set; }
    }
}
