using Microsoft.Extensions.Configuration;
using RickAndMorty.Models;

namespace RickAndMorty.Services
{
    public interface IApiService
    {
        Task<List<Character>> GetAllCharacters(int Page = 1);
    }
}
