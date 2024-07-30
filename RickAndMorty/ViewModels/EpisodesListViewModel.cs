using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RickAndMorty.Models;
using RickAndMorty.Services;
using RickAndMorty.Views;
using ShopApp.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace RickAndMorty.ViewModels
{
    public partial class EpisodesListViewModel : ViewModelGlobal
    {
        private int Page = 1;

        [ObservableProperty]
        public ObservableCollection<Episode> episodesList = [];

        [ObservableProperty]
        public Episode episodeSelected;

        private readonly ApiService _apiService;

        private readonly INavegacionService _navegacionService;

        public EpisodesListViewModel(ApiService apiService, INavegacionService navegacionService)
        {
            _apiService = apiService;
            _navegacionService = navegacionService;
            LoadDataCommand.Execute(this);
        }

        [RelayCommand]
        public async Task LoadData()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var episodes = await _apiService.GetAllEpisodes(Page);
                Page++;

                foreach (var episode in episodes)
                {
                    EpisodesList.Add(episode);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task EpisodeEventSelected()
        {
            var serializedEpisode = JsonSerializer.Serialize(EpisodeSelected);
            var uri = $"{nameof(EpisodeDetailPage)}?episodeJson={serializedEpisode}";
            await _navegacionService.GoToAsync(uri);
        }
    }
}
