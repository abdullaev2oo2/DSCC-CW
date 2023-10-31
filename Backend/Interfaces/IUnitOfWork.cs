using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IWriterRepository WriterRepository { get; }
        IBookRepository BookRepository { get; }
        Task<bool> SaveAsync();
    }
}
