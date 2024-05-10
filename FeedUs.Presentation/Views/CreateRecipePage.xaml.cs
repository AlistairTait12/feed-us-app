using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePage : ContentPage
{
    public CreateRecipePage(CreateRecipeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}