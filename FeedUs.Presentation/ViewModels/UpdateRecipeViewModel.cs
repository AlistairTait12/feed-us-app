using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using FeedUs.Presentation.Wrappers;

namespace FeedUs.Presentation.ViewModels;

[QueryProperty("Recipe", "Recipe")]
public partial class UpdateRecipeViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;
    private readonly INavigationWrapper _navigationWrapper;

    public UpdateRecipeViewModel(IDataAccess dataAccess, INavigationWrapper navigationWrapper)
    {
        _dataAccess = dataAccess;
        _navigationWrapper = navigationWrapper;
    }

    [ObservableProperty]
    Recipe recipe;

    [RelayCommand]
    public async Task UpdateRecipe()
    {
        await _dataAccess.UpdateRecipeAsync(Recipe);
        var updatedVersion = await _dataAccess.GetRecipe(Recipe.Id);
        var parameters = new Dictionary<string, object>
        {
            { "Recipe", updatedVersion }
        };
        await _navigationWrapper.GoToAsync("..", parameters);
        await DisplayUpdatedConfirmationToast();
    }

    private async Task DisplayUpdatedConfirmationToast() =>
        await ViewModelStaticMethods.DisplayToast($"Recipe {Recipe.Title} updated");
}
