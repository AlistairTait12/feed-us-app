using FeedUs.Presentation.Enums;
using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePage : ContentPage
{
    public CreateRecipePage(CreateRecipeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        UnitPicker.ItemsSource = Enum.GetNames(typeof(UnitOfMeasure));
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
    }
}
