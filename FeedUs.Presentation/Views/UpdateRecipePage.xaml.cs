using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class UpdateRecipePage : ContentPage
{
    public UpdateRecipePage(UpdateRecipeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}