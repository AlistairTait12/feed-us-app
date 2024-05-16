using FeedUs.Presentation.Enums;
using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePage : ContentPage
{
    public CreateRecipePage(CreateRecipeViewModel viewModel)
    {
        var units = new List<string>();
        foreach (var unit in Enum.GetValues<UnitOfMeasure>())
        {
            units.Add(unit.GetDisplayString());
        }
        InitializeComponent();
        BindingContext = viewModel;
        UnitPicker.ItemsSource = units;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
        BindingContext = null;
    }
}
