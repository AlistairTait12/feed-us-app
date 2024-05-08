using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class RecipeDetailsPage : ContentPage
{
    public RecipeDetailsPage(RecipeDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}