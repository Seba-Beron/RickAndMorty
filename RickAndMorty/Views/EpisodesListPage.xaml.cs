using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class EpisodesListPage : ContentPage
{
	public EpisodesListPage(EpisodesListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}