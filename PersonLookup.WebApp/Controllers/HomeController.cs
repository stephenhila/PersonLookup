using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonLookup.WebApp.Models;
using System.Diagnostics;
using System.Text;

namespace PersonLookup.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClientHandler _clientHandler = new HttpClientHandler();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<IActionResult> Index()
        {
            List<Person> people;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7149/api/person"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<List<Person>>(apiResponse);
                }
            }

            return View(new IndexModel { People = people });
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IndexModel> Add(IndexModel model)
        {
            Person personFromResponse = null;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model.NewPerson), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7149/api/person", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    personFromResponse = JsonConvert.DeserializeObject<Person>(apiResponse);

                    model.People.Add(personFromResponse);
                }
            }

            return model;
        }

        public async Task<IActionResult> PeopleList()
        {
            List<Person> people;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7149/api/person"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<List<Person>>(apiResponse);
                }
            }

            return PartialView(people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}