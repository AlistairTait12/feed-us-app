using FeedUs.Presentation.Views;

namespace FeedUs.Presentation;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // This needs to be async void and not async Task so that it can be a clickable event
    private async void OnRecipesPageButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RecipeListPage));
    }
}

