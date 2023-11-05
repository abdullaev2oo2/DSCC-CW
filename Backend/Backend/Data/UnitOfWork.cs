using Backend.Interfaces;
using Backend.Repositories;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;
        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public IWriterRepository WriterRepository => new WriterRepository(dc);
        public IBookRepository BookRepository => new BookRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
