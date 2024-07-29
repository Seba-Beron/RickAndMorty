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
    public partial class CharactersListViewModel : ViewModelGlobal
    {
        private int Page = 1;

        [ObservableProperty]
        public ObservableCollection<Character> charactersList = [];

        [ObservableProperty]
        public Character characterSelected;

        //public Command GetDataCommand { get; }

        private readonly ApiService _apiService;

        private readonly INavegacionService _navegacionService;

        public CharactersListViewModel(ApiService apiService, INavegacionService navegacionService)
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
                await Task.Delay(5000);
                var characters = await _apiService.GetAllCharacters(Page);
                Page++;

                foreach (var character in characters)
                {
                    CharactersList.Add(character);
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
        async Task CharacterEventSelected()
        {
            var serializedCharacter = JsonSerializer.Serialize(CharacterSelected);
            var encodedCharacter = Uri.EscapeDataString(serializedCharacter);
            var uri = $"{nameof(CharacterDetailPage)}?characterJson={serializedCharacter}";
            await _navegacionService.GoToAsync(uri);
        }
    }
}
