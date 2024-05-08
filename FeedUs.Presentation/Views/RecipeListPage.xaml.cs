using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class RecipeListPage : ContentPage
{
    public RecipeListPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        (BindingContext as RecipeListViewModel)?.LoadRecipesAsync();
        base.OnAppearing();
    }
}