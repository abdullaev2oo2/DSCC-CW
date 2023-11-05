namespace Backend.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Pages { get; set; }
        public Writer? Writer { get; set; }
        public int WriterId { get; set; }
    }
}
