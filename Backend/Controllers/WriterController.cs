using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : Controller
    {
        private readonly DataContext _context;

        public WriterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetWriters()
        {
            var writers = await _context.Writers.ToListAsync();

            return new OkObjectResult(writers);
        }
    }
}
