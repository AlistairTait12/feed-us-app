using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePage : ContentPage
{
    public CreateRecipePage(CreateRecipeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
    }
}