namespace RickAndMorty.Models
{
    public class Character : Result
    {
        public string status { get; set; }
        public string species { get; set; }
        public string type { get; set; }
        public string gender { get; set; }
        public Origin origin { get; set; }
        public LastLocation location { get; set; }
        public string image { get; set; }
        public string[] episode { get; set; }
    }

    public class Origin
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class LastLocation
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
