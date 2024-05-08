using CommunityToolkit.Mvvm.ComponentModel;
using FeedUs.Presentation.Models;

namespace FeedUs.Presentation.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    Recipe recipe;
}
