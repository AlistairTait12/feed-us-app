using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.Models;

namespace FeedUs.Presentation.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    Recipe recipe;

    [RelayCommand]
    public async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}
