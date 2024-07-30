using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class LocationsListPage : ContentPage
{
	public LocationsListPage(LocationsListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}