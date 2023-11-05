using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Writer
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DoB { get; set; }

        [Display(Name = "All Number Of Books")]
        public int AllNumberOfBooks { get; set; }
    }
}
