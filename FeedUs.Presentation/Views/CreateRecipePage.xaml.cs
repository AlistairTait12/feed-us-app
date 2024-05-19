using FeedUs.Presentation.Enums;
using FeedUs.Presentation.ViewModels;

namespace FeedUs.Presentation.Views;

public partial class CreateRecipePage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public CreateRecipePage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var units = new List<string>();
        foreach (var unit in Enum.GetValues<UnitOfMeasure>())
        {
            units.Add(unit.GetDisplayString());
        }
        InitializeComponent();
        UnitPicker.ItemsSource = units;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = _serviceProvider.GetRequiredService<CreateRecipeViewModel>();
        OnDetailsSectionClicked(this, EventArgs.Empty);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as CreateRecipeViewModel)?.OnViewDisappearing();
        BindingContext = null;
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
}