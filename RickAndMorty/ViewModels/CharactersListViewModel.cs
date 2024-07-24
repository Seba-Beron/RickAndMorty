using CommunityToolkit.Mvvm.ComponentModel;
using RickAndMorty.Models;
using RickAndMorty.Services;
using System.Collections.ObjectModel;

namespace RickAndMorty.ViewModels
{
    public partial class CharactersListViewModel : ViewModelGlobal
    {
        [ObservableProperty]
        public ObservableCollection<Character> charactersList;

        [ObservableProperty]
        public Character characterSelected;

        public Command GetDataCommand { get; }

        private readonly ApiService _apiService;

        public CharactersListViewModel(ApiService apiService)
        {
            _apiService = apiService;
            GetDataCommand = new Command(async () => await LoadDataAsync());
            GetDataCommand.Execute(this);
        }


        public async Task LoadDataAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var characters = await _apiService.GetAllCharacter();
                CharactersList = new ObservableCollection<Character>(characters);
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
    }
}
