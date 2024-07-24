using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class CharactersListPage : ContentPage
{
	public CharactersListPage(CharactersListViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}