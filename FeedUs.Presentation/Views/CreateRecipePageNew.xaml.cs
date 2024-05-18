using FeedUs.Presentation.Enums;
using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePageNew : ContentPage
{
    public CreateRecipePageNew(CreateRecipeViewModel viewModel)
    {
        var units = new List<string>();
        foreach (var unit in Enum.GetValues<UnitOfMeasure>())
        {
            units.Add(unit.GetDisplayString());
        }
        InitializeComponent();
        BindingContext = viewModel;
        UnitPicker.ItemsSource = units;
        OnDetailsSectionClicked(this, EventArgs.Empty);
    }

    public void OnDetailsSectionClicked(object sender, EventArgs e)
    {
        StepsSection.IsVisible = false;
        IngredientsSection.IsVisible = false;
        DetailsSection.IsVisible = true;
    }

    public void OnIngredientsSectionClicked(object sender, EventArgs e)
    {
        DetailsSection.IsVisible = false;
        StepsSection.IsVisible = false;
        IngredientsSection.IsVisible = true;
    }

    public void OnStepsSectionClicked(object sender, EventArgs e)
    {
        DetailsSection.IsVisible = false;
        IngredientsSection.IsVisible = false;
        StepsSection.IsVisible = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
        BindingContext = null;
    }
}