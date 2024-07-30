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
    public partial class LocationsListViewModel : ViewModelGlobal
    {
        private int Page = 1;

        [ObservableProperty]
        public ObservableCollection<Models.Location> locationsList = [];

        [ObservableProperty]
        public Models.Location locationSelected;

        private readonly ApiService _apiService;

        private readonly INavegacionService _navegacionService;

        public LocationsListViewModel(ApiService apiService, INavegacionService navegacionService)
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
                var locations = await _apiService.GetAllLocations(Page);
                Page++;

                foreach (var location in locations)
                {
                    LocationsList.Add(location);
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
        async Task LocationEventSelected()
        {
            var serializedlocation = JsonSerializer.Serialize(LocationSelected);
            var uri = $"{nameof(LocationDetailPage)}?locationJson={serializedlocation}";
            await _navegacionService.GoToAsync(uri);
        }
    }
}
