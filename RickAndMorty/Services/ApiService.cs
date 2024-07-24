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

        public async Task<List<Character>> GetAllCharacter()
        {
            var response = await client.GetAsync(settings.UrlBase + "character");
            var body = await response.Content.ReadAsStringAsync();

            JsonNode nodes = JsonNode.Parse(body);
            string results = nodes["results"].ToString();

            return JsonSerializer.Deserialize<List<Character>>(results);
        }
    }
}
