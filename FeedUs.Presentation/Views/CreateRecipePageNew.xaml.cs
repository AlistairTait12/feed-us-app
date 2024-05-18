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
        StepsButton.BackgroundColor = Color.Parse("Grey");
        IngredientsSection.IsVisible = false;
        IngredientButton.BackgroundColor = Color.Parse("Grey");
        DetailsSection.IsVisible = true;
        DetailsButton.BackgroundColor = Color.Parse("Green");
    }

    public void OnIngredientsSectionClicked(object sender, EventArgs e)
    {
        DetailsSection.IsVisible = false;
        DetailsButton.BackgroundColor = Color.Parse("Grey");
        StepsSection.IsVisible = false;
        StepsButton.BackgroundColor = Color.Parse("Grey");
        IngredientsSection.IsVisible = true;
        IngredientButton.BackgroundColor = Color.Parse("Green");
    }

    public void OnStepsSectionClicked(object sender, EventArgs e)
    {
        DetailsSection.IsVisible = false;
        DetailsButton.BackgroundColor = Color.Parse("Grey");
        IngredientsSection.IsVisible = false;
        IngredientButton.BackgroundColor = Color.Parse("Grey");
        StepsSection.IsVisible = true;
        StepsButton.BackgroundColor = Color.Parse("Green");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
        BindingContext = null;
    }
}