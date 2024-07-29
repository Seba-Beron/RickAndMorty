using Microsoft.Extensions.Configuration;
using RickAndMorty.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RickAndMorty.Services
{
    public class ApiService : IApiService
    {
        private HttpClient client;
        private Settings settings;

        public ApiService(HttpClient client, IConfiguration configuration)
        {
            this.client = client;
            settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
        }

        public async Task<List<Character>> GetAllCharacters(int Page = 1)
        {
            var returnResponse = new List<Character>();

            if(Page > 42) return returnResponse; // la api solo tiene 42 paginas

            var response = await client.GetAsync(settings.UrlBase + "character" + "?page=" + Page);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var body = await response.Content.ReadAsStringAsync();

                JsonNode nodes = JsonNode.Parse(body);
                string results = nodes["results"].ToString();

                returnResponse = JsonSerializer.Deserialize<List<Character>>(results);
            }

            return returnResponse;
        }
    }
}
