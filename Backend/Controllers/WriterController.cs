using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : Controller
    {
        private readonly IUnitOfWork uow;
        public WriterController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        // GET: api/Writer
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var writer = await uow.WriterRepository.GetWriters();
            return new OkObjectResult(writer);
        }
        // GET: api/Writer/5
        [HttpGet("{id}", Name = "GetC")]
        public IActionResult Get(int id)
        {
            var writer = uow.WriterRepository.GetWriterById(id);
            if (writer == null) return BadRequest("Not available ID written");

            return new OkObjectResult(writer);
        }
        // POST: api/Writer
        [HttpPost]
        public async Task<IActionResult> Add(Writer writer)
        {
            if (writer == null) return BadRequest("Writer cannot be null");

            uow.WriterRepository.InsertWriter(writer);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        // PUT: api/Writer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Writer writer)
        {
            if (writer != null)
            {
                uow.WriterRepository.UpdateWriter(writer);
                await uow.SaveAsync();
                if (id != writer.Id || writer == null) return BadRequest("Update Not Allowed");

                return StatusCode(200);
            }
            return new NoContentResult();
        }
        // DELETE: api/Writer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var writer = uow.WriterRepository.GetWriterById(id);
            if (writer == null) return BadRequest("Delete Not Allowed, Reason: Not available ID written");

            uow.WriterRepository.DeleteWriter(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
