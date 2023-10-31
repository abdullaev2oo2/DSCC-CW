using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IBookRepository
    {
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        Book GetBookById(int id);
        Task<IEnumerable<Book>> GetBooks();
    }
}
