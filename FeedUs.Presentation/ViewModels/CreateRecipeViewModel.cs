using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Enums;
using FeedUs.Presentation.Models;
using System.Collections.ObjectModel;

namespace FeedUs.Presentation.ViewModels;

public partial class CreateRecipeViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public CreateRecipeViewModel(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string description;

    public ObservableCollection<Ingredient> Ingredients { get; } = new();

    [ObservableProperty]
    string currentIngredientName;

    [ObservableProperty]
    int currentIngredientAmount;

    [ObservableProperty]
    string currentIngredientUnit;

    public ObservableCollection<string> Steps { get; } = new();

    [ObservableProperty]
    string currentStep;

    [ObservableProperty]
    bool addStepButtonEnabled;

    [RelayCommand]
    public void AddIngredient()
    {
        var ingredient = new Ingredient
        {
            Name = CurrentIngredientName,
            Quantity = CurrentIngredientAmount,
            Unit = Enum.Parse<UnitOfMeasure>(CurrentIngredientUnit)
        };

        Ingredients.Add(ingredient);
    }

    [RelayCommand]
    public void AddStep() => Steps.Add(CurrentStep);

    partial void OnCurrentStepChanged(string value) => UpdateAddStepButtonEnabled();

    [RelayCommand]
    private void UpdateAddStepButtonEnabled() =>
        AddStepButtonEnabled = !string.IsNullOrWhiteSpace(CurrentStep);

    [RelayCommand]
    public async Task CreateRecipeAsync()
    {
        var recipe = new Recipe
        {
            Title = Title,
            Description = Description,
            Ingredients = Ingredients,
            Steps = Steps
        };

        await _dataAccess.AddRecipeAsync(recipe);
        await Shell.Current.GoToAsync("..");
    }
}

// TODO: All this stuff:
// - Should be able to scroll on the create recipe page
// - Should be able to remove ingredients and steps from the page
// - Adding an ingredient or step should clear the input fields
// - Dropdown for unit of measure
// - Quantity should be a number entry and have a numeric keyboard type
