using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RickAndMorty.Models;
using RickAndMorty.Services;
using RickAndMorty.Views;
using ShopApp.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace RickAndMorty.ViewModels
{
    public partial class CharactersListViewModel : ViewModelGlobal
    {
        private readonly ApiService _apiService;
        private readonly INavegacionService _navegacionService;

        private int Page = 1;

        [ObservableProperty]
        private ObservableCollection<Character> charactersList = [];

        [ObservableProperty]
        private Character characterSelected;

        [ObservableProperty]
        private ObservableCollection<string> statusOptions = ["Alive", "Dead", "unknown"];

        [ObservableProperty]
        private ObservableCollection<string> genderOptions = ["Female", "Male", "Genderless", "unknown"];

        [ObservableProperty]
        private string selectedStatus;
        partial void OnSelectedStatusChanged(string value) => FilterChanged();

        [ObservableProperty]
        private string selectedGender;
        partial void OnSelectedGenderChanged(string value) => FilterChanged();

        [ObservableProperty]
        private string searchText;
        partial void OnSearchTextChanged(string value) => FilterChanged();
        

        public CharactersListViewModel(ApiService apiService, INavegacionService navegacionService)
        {
            _apiService = apiService;
            _navegacionService = navegacionService;
            LoadDataCommand.Execute(this);
        }

        private void FilterChanged()
        {
            CharactersList.Clear();
            Page = 1;
            LoadDataCommand.Execute(this);
        }

        [RelayCommand]
        public async Task LoadData()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var characters = await _apiService.GetAllCharacters(Page, SearchText, selectedStatus, selectedGender);
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
            var uri = $"{nameof(CharacterDetailPage)}?characterJson={serializedCharacter}";
            await _navegacionService.GoToAsync(uri);
        }
    }
}
