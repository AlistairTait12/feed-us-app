using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.Views;
using System.Collections.ObjectModel;

namespace FeedUs.Presentation.ViewModels;

public partial class RecipeListViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public ObservableCollection<Recipe> Recipes { get; } = new();

    public RecipeListViewModel(IDataAccess dataAccess) => _dataAccess = dataAccess;

    [RelayCommand]
    public async Task LoadRecipesAsync()
    {
        var recipes = await _dataAccess.GetRecipesAsync();
        // Setting the collection to a new instance will NOT update the UI!!!
        // But the following logic will...
        if (Recipes.Count is not 0)
        {
            Recipes.Clear(); 
        }

        foreach (var recipe in recipes)
        {
            Recipes.Add(recipe);
        }
    }

    [RelayCommand]
    public async Task GoToRecipeDetails(Recipe recipe)
    {
        await Shell.Current.GoToAsync(nameof(RecipeDetailsPage), false,
            new Dictionary<string, object>
            {
                { "Recipe", recipe }
            });
    }
}
