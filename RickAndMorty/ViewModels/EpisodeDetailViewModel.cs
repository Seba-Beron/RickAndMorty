using CommunityToolkit.Mvvm.ComponentModel;
using RickAndMorty.Models;
using System.Text.Json;

namespace RickAndMorty.ViewModels
{
    public partial class EpisodeDetailViewModel : ViewModelGlobal, IQueryAttributable
    {
        [ObservableProperty]
        public Episode episode;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("episodeJson", out var episodeJson))
            {
                var decodedJson = Uri.UnescapeDataString(episodeJson.ToString());
                Episode = JsonSerializer.Deserialize<Episode>(decodedJson);
            }
        }
    }
}
