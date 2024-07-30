using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace RickAndMorty.ViewModels
{
    public partial class LocationDetailViewModel : ViewModelGlobal, IQueryAttributable
    {
        [ObservableProperty]
        public Models.Location location;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("locationJson", out var locationJson))
            {
                var decodedJson = Uri.UnescapeDataString(locationJson.ToString());
                Location = JsonSerializer.Deserialize<Models.Location>(decodedJson);
            }
        }
    }
}
