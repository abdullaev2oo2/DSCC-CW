﻿namespace Backend.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Pages { get; set; }
        public Writer? Writer { get; set; }
    }
}
