using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Frontend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Frontend.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
            clnt = new HttpClient();
            clnt.BaseAddress = new Uri(_configuration["BaseUrl"]);
        }

        public async Task<List<Writer>> GetAllWriters()
        {
            List<Writer> writers = new List<Writer>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Writer");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                writers = JsonConvert.DeserializeObject<List<Writer>>(responseMessage);
            }
            return writers;
        }

        public void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Book
        public async Task<ActionResult> Index()
        {

            //// Creating the list of new Product list
            List<Book> bookInfo = new List<Book>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Book");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                bookInfo = JsonConvert.DeserializeObject<List<Book>>(responseMessage);
            }
            return View(bookInfo);

        }

        // GET: Book/5
        public ActionResult Details(int id)
        {
            //Creating a Get Request to get single Book
            Book bookDetails = new Book();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Book/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                bookDetails = JsonConvert.DeserializeObject<Book>(detailsInfo);
            }
            return View(bookDetails);
        }

        // GET: Book/Create
        public async Task<ActionResult> Create()
        {
            List<Writer> writerList = await GetAllWriters();
            ViewBag.WriterList = new SelectList(writerList, "Id", "Name");
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            book.Writer = new Writer { Id = book.WriterId };
            if (ModelState.IsValid)
            {
                // serializing product object into json format to send
                /*string jsonObject = "{"+product."}"*/
                ;
                string createBookInfo = JsonConvert.SerializeObject(book);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createBookInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Book/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(book);

        }

        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //Creating a Get Request to get single Book
            Book bookDetails = new Book();

            HeaderClearing();

            List<Writer> writerList = await GetAllWriters();
            ViewBag.WriterList = new SelectList(writerList, "Id", "Name");

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Book/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                bookDetails = JsonConvert.DeserializeObject<Book>(detailsInfo);
            }
            return View(bookDetails);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            book.Writer = new Writer { Id = book.WriterId };
            book.Id = id;
            if (ModelState.IsValid)
            {
                // serializing product object into json format to send
                /*string jsonObject = "{"+product."}"*/
                string putBookInfo = JsonConvert.SerializeObject(book);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(putBookInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + "api/Book/" + id, stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            //Creating a Get Request to get single Book
            Book bookDetails = new Book();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Book/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                bookDetails = JsonConvert.DeserializeObject<Book>(detailsInfo);
            }
            return View(bookDetails);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            HeaderClearing();
            
            HttpResponseMessage httpResponseMessageDetails = clnt.DeleteAsync(clnt.BaseAddress + "api/Book/" + id).Result;
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }else
            {
                return View();
            }
        }
    }
}
