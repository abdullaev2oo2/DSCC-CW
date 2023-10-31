using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IWriterRepository
    {
        void InsertWriter(Writer writer);
        void UpdateWriter(Writer writer);
        void DeleteWriter(int id);
        Writer GetWriterById(int id);
        Task<IEnumerable<Writer>> GetWriters();
    }
}
