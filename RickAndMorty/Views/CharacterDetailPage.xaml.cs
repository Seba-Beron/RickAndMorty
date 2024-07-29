using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class CharacterDetailPage : ContentPage
{
	public CharacterDetailPage(CharacterDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}