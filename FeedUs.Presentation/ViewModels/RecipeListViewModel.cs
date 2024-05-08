using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.Models;
using System.Collections.ObjectModel;

namespace FeedUs.Presentation.ViewModels;

public partial class RecipeListViewModel : ObservableObject
{
    private readonly IDataAccess _dataAccess;

    public ObservableCollection<Recipe> Recipes { get; set; } = new();

    public RecipeListViewModel(IDataAccess dataAccess) => _dataAccess = dataAccess;

    [RelayCommand]
    public async Task LoadRecipesAsync() => Recipes = new(await _dataAccess.GetRecipesAsync());
}
