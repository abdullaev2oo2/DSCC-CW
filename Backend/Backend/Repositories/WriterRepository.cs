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
    public class WriterRepository : IWriterRepository
    {

        private readonly DataContext _dbContext;
        public WriterRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteWriter(int writerId)
        {
            var writer = _dbContext.Writers.Find(writerId);
            if (writer != null)
            {
                _dbContext.Writers.Remove(writer);
                Save();
            }
        }
        public Writer GetWriterById(int writerId)
        {
            var prod = _dbContext.Writers.Find(writerId);
            if (prod != null)
            {
                _dbContext.Entry(prod);
            }
            return prod;
        }
        public async Task<IEnumerable<Writer>> GetWriters()
        {
            return await _dbContext.Writers.ToListAsync();
        }
        public void InsertWriter(Writer writer)
        {
            _dbContext.Add(writer);
            Save();
        }
        public void UpdateWriter(Writer writer)
        {
            _dbContext.Entry(writer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }


    }
}
