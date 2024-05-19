using CommunityToolkit.Maui;
using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Views;
using FeedUs.Presentation.Wrappers;
using Microsoft.Extensions.Logging;

namespace FeedUs.Presentation;

public static class MauiProgram
{
    private static readonly string _filePath = Path.Combine(
        FileSystem.AppDataDirectory,
        "recipe_storage.json");

    public static MauiApp CreateMauiApp()
    {
        // Create the storage file if not existing
        if (!File.Exists(_filePath))
        {
            var fileContent = "[]";
            File.WriteAllText(_filePath, fileContent);
        }

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<INavigationWrapper,  NavigationWrapper>();
        builder.Services.AddSingleton<IDataAccess, SimpleJsonDataAccess>(
            _ => new SimpleJsonDataAccess(_filePath));
        builder.Services.AddSingleton<RecipeListPage>();
        builder.Services.AddTransient<RecipeListViewModel>();
        builder.Services.AddSingleton<RecipeDetailsPage>();
        builder.Services.AddTransient<RecipeDetailsViewModel>();
        builder.Services.AddSingleton<CreateRecipePage>();
        builder.Services.AddTransient<CreateRecipeViewModel>();
        builder.Services.AddSingleton<UpdateRecipePage>();
        builder.Services.AddTransient<UpdateRecipeViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
