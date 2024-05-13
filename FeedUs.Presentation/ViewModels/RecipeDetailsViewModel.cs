using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;

namespace FeedUs.Presentation.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailsViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public RecipeDetailsViewModel(IDataAccess dataAccess) => _dataAccess = dataAccess;

    [ObservableProperty]
    Recipe recipe;

    [RelayCommand]
    public async Task GoBackAsync() => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    public async Task DeleteRecipeAsync()
    {
        bool answer = await Shell.Current.DisplayAlert(
            "Confirmation",
            "Are you sure you want to delete the recipe?",
            "Yes",
            "No");

        if (answer)
        {
            await _dataAccess.DeleteRecipeAsync(recipe.Id);
            await Shell.Current.GoToAsync("..");
        }
    }
}
