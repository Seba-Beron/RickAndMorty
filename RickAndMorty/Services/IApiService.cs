using Microsoft.Extensions.Configuration;
using RickAndMorty.Models;

namespace RickAndMorty.Services
{
    public interface IApiService
    {
        public Task<List<Character>> GetAllCharacter();
    }
}
