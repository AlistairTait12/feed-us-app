using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.Views;
using FeedUs.Presentation.Wrappers;
using System.Collections.ObjectModel;

namespace FeedUs.Presentation.ViewModels;

public partial class RecipeListViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;
    private readonly INavigationWrapper _navigationWrapper;

    public ObservableCollection<Recipe> Recipes { get; } = new();

    public RecipeListViewModel(IDataAccess dataAccess, INavigationWrapper navigationWrapper)
    {
        _dataAccess = dataAccess;
        _navigationWrapper = navigationWrapper;
    }

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
        await _navigationWrapper.GoToAsync(nameof(RecipeDetailsPage),
            new Dictionary<string, object>
            {
                { "Recipe", recipe }
            });
    }
}
