using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.Views;
using FeedUs.Presentation.Wrappers;

namespace FeedUs.Presentation.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailsViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;
    private readonly INavigationWrapper _navigationWrapper;

    public RecipeDetailsViewModel(IDataAccess dataAccess, INavigationWrapper navigationWrapper)
    {
        _dataAccess = dataAccess;
        _navigationWrapper = navigationWrapper;
    }

    [ObservableProperty]
    Recipe recipe;

    [RelayCommand]
    public async Task GoBackAsync() => await _navigationWrapper.GoToAsync("..");

    [RelayCommand]
    public async Task DeleteRecipeAsync()
    {
        var userDidConfirm = await _navigationWrapper.DisplayAlert(
            "Confirmation",
            "Are you sure you want to delete the recipe?",
            "Yes",
            "No");

        if (userDidConfirm)
        {
            await _dataAccess.DeleteRecipeAsync(Recipe.Id);
            await _navigationWrapper.GoToAsync("..");
            await DisplayDeletedConfirmationToast();
        }
    }

    [RelayCommand]
    public async Task GoToUpdateRecipe()
    {
        var parameters = new Dictionary<string, object>
        {
            { "Recipe", Recipe }
        };

        await _navigationWrapper.GoToAsync(nameof(UpdateRecipePage), parameters);
    }

    private async Task DisplayDeletedConfirmationToast() =>
        await ViewModelStaticMethods.DisplayToast($"Recipe {Recipe.Title} deleted");
}
