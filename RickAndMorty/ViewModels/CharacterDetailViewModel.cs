using CommunityToolkit.Mvvm.ComponentModel;
using RickAndMorty.Models;
using System.Text.Json;

namespace RickAndMorty.ViewModels
{
    public partial class CharacterDetailViewModel : ViewModelGlobal, IQueryAttributable
    {
        [ObservableProperty]
        public Character character;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("characterJson", out var characterJson))
            {
                var decodedJson = Uri.UnescapeDataString(characterJson.ToString());
                Character = JsonSerializer.Deserialize<Character>(decodedJson);
            }
        }
    }
}
