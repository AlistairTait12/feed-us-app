using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Enums;
using FeedUs.Presentation.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FeedUs.Presentation.ViewModels;

public partial class CreateRecipeViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public CreateRecipeViewModel(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        Ingredients.CollectionChanged += OnIngredientsChanged;
        Steps.CollectionChanged += OnStepsChanged;
    }

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string description;

    public ObservableCollection<Ingredient> Ingredients { get; } = new();

    [ObservableProperty]
    string currentIngredientName;

    [ObservableProperty]
    string currentIngredientAmount;

    [ObservableProperty]
    string currentIngredientUnit;

    public ObservableCollection<string> Steps { get; } = new();

    [ObservableProperty]
    string currentStep;

    [ObservableProperty]
    bool addStepButtonEnabled;

    [ObservableProperty]
    bool addIngredientButtonEnabled;

    [ObservableProperty]
    bool createButtonEnabled;

    [RelayCommand]
    public void AddIngredient()
    {
        var amount = double.Parse(CurrentIngredientAmount);

        var ingredient = new Ingredient
        {
            Name = CurrentIngredientName,
            Quantity = amount,
            Unit = Enum.Parse<UnitOfMeasure>(CurrentIngredientUnit)
        };

        Ingredients.Add(ingredient);
        CurrentIngredientName = string.Empty;
        CurrentIngredientAmount = string.Empty;
        CurrentIngredientUnit = string.Empty;
    }

    [RelayCommand]
    public void RemoveIngredient(Ingredient ingredient) => Ingredients.Remove(ingredient);

    [RelayCommand]
    public void AddStep()
    {
        Steps.Add(CurrentStep);
        CurrentStep = string.Empty;
    }

    [RelayCommand]
    public void RemoveStep(string step) => Steps.Remove(step);

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

    // Button enablement logic
    partial void OnCurrentStepChanged(string value) => UpdateAddStepButtonEnabled();

    [RelayCommand]
    private void UpdateAddStepButtonEnabled() =>
        AddStepButtonEnabled = !string.IsNullOrWhiteSpace(CurrentStep);

    partial void OnCurrentIngredientNameChanged(string value) => UpdateAddIngredientButtonEnabled();

    partial void OnCurrentIngredientAmountChanged(string value) => UpdateAddIngredientButtonEnabled();

    partial void OnCurrentIngredientUnitChanged(string value) => UpdateAddIngredientButtonEnabled();

    [RelayCommand]
    private void UpdateAddIngredientButtonEnabled()
    {
        AddIngredientButtonEnabled = !string.IsNullOrWhiteSpace(CurrentIngredientName)
        && !string.IsNullOrWhiteSpace(CurrentIngredientUnit)
        && !string.IsNullOrEmpty(CurrentIngredientAmount);
    }

    partial void OnTitleChanged(string value) => UpdateCreateButtonEnabled();

    private void OnIngredientsChanged(object sender, NotifyCollectionChangedEventArgs e) => UpdateCreateButtonEnabled();

    private void OnStepsChanged(object sender, NotifyCollectionChangedEventArgs e) => UpdateCreateButtonEnabled();

    [RelayCommand]
    private void UpdateCreateButtonEnabled()
    {
        CreateButtonEnabled = !string.IsNullOrWhiteSpace(Title)
        && Steps.Count > 0
        && Ingredients.Count > 0;
    }

    public void OnViewDisappearing()
    {
        Ingredients.CollectionChanged -= OnIngredientsChanged;
        Steps.CollectionChanged -= OnStepsChanged;
    }
}
