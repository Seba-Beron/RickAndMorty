using RickAndMorty.ViewModels;

namespace RickAndMorty.Views;

public partial class EpisodeDetailPage : ContentPage
{
	public EpisodeDetailPage(EpisodeDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}