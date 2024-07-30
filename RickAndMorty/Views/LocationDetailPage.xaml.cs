using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class LocationDetailPage : ContentPage
{
	public LocationDetailPage(LocationDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}