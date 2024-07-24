namespace RickAndMorty.Models
{
    public class Episode : Result
    {
        public string air_date { get; set; }
        public string episode { get; set; }
        public string[] characters { get; set; }
    }
}
