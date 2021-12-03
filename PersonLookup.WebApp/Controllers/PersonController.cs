using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonLookup.WebApp.Models;

namespace PersonLookup.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        public static List<Person> HardCodedPeople = new List<Person>
        {
            new Person {Id = "1", Name = "John Doe"},
            new Person {Id = "2", Name = "Stephen"},
            new Person {Id = "3", Name = "Jenifer"},
        };

        [HttpGet]
        public List<Person> Get()
        {
            return HardCodedPeople;
        }

        [HttpPost(Name = "AddPerson")]
        public Person Add(Person person)
        {
            HardCodedPeople.Add(person);
            return person;
        }
    }
}
