namespace Backend.Models
{
    public class Writer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DoB { get; set; }
        public int AllNumberOfBooks { get; set; }
    }
}
