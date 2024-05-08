using FeedUs.Presentation.DataAccess;
using FeedUs.Presentation.ViewModels;
using FeedUs.Presentation.Views;
using Microsoft.Extensions.Logging;

namespace FeedUs.Presentation;

public static class MauiProgram
{
    private static readonly string _filePath = Path.Combine(
        FileSystem.AppDataDirectory,
        "sampleRecipes.json");

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<RecipeListPage>();
        builder.Services.AddSingleton<IDataAccess, SimpleJsonDataAccess>(_ =>
        {
            return new SimpleJsonDataAccess(_filePath);
        });
        builder.Services.AddTransient<RecipeListViewModel>();
        builder.Services.AddTransient<RecipeDetailsPage>();
        builder.Services.AddTransient<RecipeDetailsViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
