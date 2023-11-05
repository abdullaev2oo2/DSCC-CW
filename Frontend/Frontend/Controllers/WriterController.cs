using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend.Controllers
{
    public class WriterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public WriterController(IConfiguration configuration)
        {
            _configuration = configuration;
            clnt = new HttpClient();
            clnt.BaseAddress = new Uri(_configuration["BaseUrl"]);
        }

        public void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Writer
        public async Task<ActionResult> Index()
        {
            //// Creating the list of new Product list
            List<Writer> bookInfo = new List<Writer>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Writer");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                bookInfo = JsonConvert.DeserializeObject<List<Writer>>(responseMessage);
            }
            return View(bookInfo);
        }

        // GET: Writer/Details/5
        public ActionResult Details(int id)
        {
            //Creating a Get Request to get single Writer
            Writer writerDetails = new Writer();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Writer/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                writerDetails = JsonConvert.DeserializeObject<Writer>(detailsInfo);
            }
            return View(writerDetails);
        }

        // GET: Writer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Writer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Writer writer)
        {
            if (ModelState.IsValid)
            {
                // serializing writer object into json format to send
                /*string jsonObject = "{"+writer."}"*/
                ;
                string createBookInfo = JsonConvert.SerializeObject(writer);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createBookInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/writer/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(writer);
        }

        // GET: Writer/Edit/5
        public ActionResult Edit(int id)
        {
            //Creating a Get Request to get single Writer
            Writer bookDetails = new Writer();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Writer/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                bookDetails = JsonConvert.DeserializeObject<Writer>(detailsInfo);
            }
            return View(bookDetails);
        }

        // POST: Writer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Writer writer)
        {
            if (ModelState.IsValid)
            {
                // serializing write object into json format to send
                /*string jsonObject = "{"+product."}"*/
                string putBookInfo = JsonConvert.SerializeObject(writer);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(putBookInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + "api/writer/" + id, stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: Writer/Delete/5
        public ActionResult Delete(int id)
        {
            //Creating a Get Request to get single Writer
            Writer writerDetails = new Writer();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Writer/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                writerDetails = JsonConvert.DeserializeObject<Writer>(detailsInfo);
            }
            return View(writerDetails);
        }

        // POST: Writer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            HeaderClearing();

            HttpResponseMessage httpResponseMessageDetails = clnt.DeleteAsync(clnt.BaseAddress + "api/writer/" + id).Result;
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
