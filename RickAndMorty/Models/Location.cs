namespace RickAndMorty.Models
{
    public class Location : Result
    {
        public string type { get; set; }
        public string dimension { get; set; }
        public string[] residents { get; set; }
    }
}
