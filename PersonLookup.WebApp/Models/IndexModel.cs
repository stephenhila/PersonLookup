namespace PersonLookup.WebApp.Models
{
    public class IndexModel
    {
        public List<Person> People { get; set; } = new List<Person>();

        public Person NewPerson { get; set; } = new Person();
    }
}
