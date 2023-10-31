using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Interfaces;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IUnitOfWork uow;
        public BookController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await uow.BookRepository.GetBooks();


            return new OkObjectResult(books);
        }
        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetB")]
        public IActionResult Get(int id)
        {
            var book = uow.BookRepository.GetBookById(id);
            if (book == null) return BadRequest("Not available ID written");

            return new OkObjectResult(book);
        }
        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            if (book == null) return BadRequest("Book cannot be null");

            uow.BookRepository.InsertBook(book);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (book != null)
            {
                uow.BookRepository.UpdateBook(book);
                await uow.SaveAsync();
                if (id != book.Id || book == null) return BadRequest("Update Not Allowed");

                return StatusCode(200);
            }
            return new NoContentResult();
        }
        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = uow.BookRepository.GetBookById(id);
            if (book == null) return BadRequest("Delete Not Allowed, Reason: Not available ID written");

            uow.BookRepository.DeleteBook(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
