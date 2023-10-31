using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dbContext;
        public BookRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int booktId)
        {
            var book = _dbContext.Books.Find(booktId);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                Save();
            }
        }
        public Book GetBookById(int booktId)
        {
            var book = _dbContext.Books.Find(booktId);
            if (book != null)
            {
                _dbContext.Entry(book).Reference(s => s.Writer).Load();
            }
            return book;
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _dbContext.Books.Include(s => s.Writer).ToListAsync();
        }
        public void InsertBook(Book book)
        {
            book.Writer = _dbContext.Writers.Find(book.WriterId);
            _dbContext.Add(book);
            Save();
        }
        public void UpdateBook(Book book)
        {
            _dbContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
