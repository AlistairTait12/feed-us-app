using FeedUs.Presentation.Views;

namespace FeedUs.Presentation;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(RecipeListPage), typeof(RecipeListPage));
        Routing.RegisterRoute(nameof(RecipeDetailsPage), typeof(RecipeDetailsPage));
        Routing.RegisterRoute(nameof(CreateRecipePage), typeof(CreateRecipePage));
    }
}
