using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class RecipeListPage : ContentPage
{
    public RecipeListPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as RecipeListViewModel).LoadRecipesAsync();
    }
}