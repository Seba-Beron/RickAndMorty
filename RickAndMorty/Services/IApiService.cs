using Microsoft.Extensions.Configuration;
using RickAndMorty.Models;

namespace RickAndMorty.Services
{
    public interface IApiService
    {
        Task<List<Character>> GetAllCharacters(int Page = 1);
        Task<List<Models.Location>> GetAllLocations(int Page = 1);
        Task<List<Episode>> GetAllEpisodes(int Page = 1);
    }
}
