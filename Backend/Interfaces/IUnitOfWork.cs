using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IUnitOfWork
    {
        IWriterRepository WriterRepository { get; }
        IBookRepository BookRepository { get; }
        Task<bool> SaveAsync();
    }
}
