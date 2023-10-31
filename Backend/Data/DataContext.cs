using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DataContext : DbContext { 
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Writer> Writers => Set<Writer>();
        public DbSet<Book> Books => Set<Book>();
    }
}
